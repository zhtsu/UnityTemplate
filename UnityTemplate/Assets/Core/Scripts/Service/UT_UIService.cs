using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public enum UT_EUILayer
{
    None,
    Bottom,
    Main,
    Top,
    Popup,
}

public class UT_UIService : UT_Service, UT_IUIService
{
    public override string ServiceName => "UI Service";

    private UT_SO_UIConfig _UIConfig;
    private UT_UIRoot _UIRoot;

    private Dictionary<string, UT_UIView> _CacheUIDict = new Dictionary<string, UT_UIView>();
    private Stack<UT_UIView> _ActiveUIStack = new Stack<UT_UIView>();

    public UT_UIService(UT_SO_UIConfig UIConfig)
    {
        _UIConfig = UIConfig;
    }

    public override async UniTask Initialize()
    {
        _CacheUIDict.Clear();
        _ActiveUIStack.Clear();

        if (_UIConfig == null)
        {
            Debug.LogError("UI config is null!");
            return;
        }

        var Results = await UnityEngine.Object.InstantiateAsync(_UIConfig.UIRootPrefab).ToUniTask();
        if (Results.Length > 0)
        {
            _UIRoot = Results[0].GetComponent<UT_UIRoot>();
            if (_UIRoot == null)
            {
                Debug.LogError("UI Root Component is missing in the prefab!");
            }
        }
    }

    public override void Destroy()
    {
        _CacheUIDict.Clear();
        _ActiveUIStack.Clear();
    }

    public void OpenUI(string UITypeKey, UT_UIParams Params = null)
    {
        if (_UIRoot == null)
            return;

        UT_SO_UIDescriptor UIDesc = GetUIDesc(UITypeKey);
        if (UIDesc == null)
            return;

        if (_CacheUIDict.TryGetValue(UITypeKey, out UT_UIView CachedUI))
        {
            OpenUI_Internal(CachedUI, Params, UIDesc);
            return;
        }

        Addressables.LoadAssetAsync<GameObject>(UIDesc.PrefabAddress).Completed +=
        (Handle) =>
        {
            if (Handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                GameObject Prefab = Handle.Result;
                GameObject UIObj = GameObject.Instantiate(Prefab);
                UT_UIView UIComp = UIObj.GetComponent<UT_UIView>();
                if (UIComp != null)
                {
                    _CacheUIDict.Add(UITypeKey, UIComp);
                    OpenUI_Internal(UIComp, Params, UIDesc);
                }
                else
                {
                    Debug.LogError($"UI Component is missing in prefab for UI type {UITypeKey}!");
                    GameObject.Destroy(UIObj);
                }
            }
            else
            {
                Debug.LogError($"Failed to load UI prefab for type {UITypeKey} from address {UIDesc.PrefabAddress}!");
            }
        };
    }

    private void OpenUI_Internal(UT_UIView UI, UT_UIParams Params, UT_SO_UIDescriptor UIDesc)
    {
        UI.Initialize(Params);
        UI.OnOpen();

        RectTransform Rect = UI.GetComponent<RectTransform>();
        SetFullStretch(Rect);

        Transform LayerTransform = _UIRoot.GetLayerObject(UIDesc.Layer).transform;
        UI.transform.SetParent(LayerTransform, false);
        UI.enabled = true;

        _ActiveUIStack.Push(UI);
    }

    public void CloseUI(string UITypeKey)
    {
        if (_ActiveUIStack.Count == 0)
            return;

        UT_UIView TopUI = _ActiveUIStack.Peek();

        if (TopUI.TypeKey != UITypeKey)
            return;

        TopUI.OnClose();
        TopUI.transform.SetParent(null);
        TopUI.enabled = false;

        _ActiveUIStack.Pop();
    }

    public void CloseAllUI()
    {
        while (_ActiveUIStack.Count > 0)
        {
            UT_UIView TopUI = _ActiveUIStack.Pop();
            TopUI.OnClose();
            TopUI.transform.SetParent(null);
            TopUI.enabled = false;
        }
    }

    private UT_SO_UIDescriptor GetUIDesc(string UITypeKey)
    {
        if (_UIConfig == null)
            return null;

        foreach (var Desc in _UIConfig.UITypeDescriptorList)
        {
            if (Desc.TypeKey == UITypeKey)
                return Desc;
        }

        Debug.LogError($"UI descriptor for type {UITypeKey} not found in config!");
        return null;
    }

    public static void SetFullStretch(RectTransform TargetRectTransform)
    {
        TargetRectTransform.anchorMin = Vector2.zero;
        TargetRectTransform.anchorMax = Vector2.one;

        TargetRectTransform.offsetMin = Vector2.zero;
        TargetRectTransform.offsetMax = Vector2.zero;

        TargetRectTransform.pivot = new Vector2(0.5f, 0.5f);
    }
}
