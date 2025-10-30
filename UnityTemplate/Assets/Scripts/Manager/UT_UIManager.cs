using System;
using System.Collections.Generic;
using UnityEngine;

public class UT_UIManager : UT_Manager
{
    override public string ManagerName => "UI Manager";

    private UT_UIRoot _UIRoot;
    private UT_ISpawnService _SpawnService;

    private List<GameObject> _ActiveUIList = new List<GameObject>();

    public UT_UIManager(UT_UIRoot InUIRoot, UT_ISpawnService InSpawnService)
    {
        _UIRoot = InUIRoot;
        _SpawnService = InSpawnService;
    }

    override public void Initialize()
    {
        _ActiveUIList.Clear();
    }

    override public void Destroy()
    {
        _ActiveUIList.Clear();
    }

    public void OpenScreenUI(UT_EScreenUIType TargetUIType, UT_UIParams Params, UT_EUILayer Layer)
    {
        if (_UIRoot == null)
            return;
        if (_SpawnService == null)
            return;

        GameObject TargetUI = _SpawnService.CreateGameObject(GetUIPrefabAddress(TargetUIType));

        UT_ScreenUI UIComp = TargetUI.GetComponent<UT_ScreenUI>();
        UIComp.SetParams(Params);
        UIComp.OnOpen();

        RectTransform Rect = TargetUI.GetComponent<RectTransform>();
        SetFullStretch(Rect);

        Transform LayerTransform = _UIRoot.GetLayerObject(Layer).transform;
        TargetUI.transform.SetParent(LayerTransform, false);

        _ActiveUIList.Add(TargetUI);
    }

    public void CloseScreenUI(UT_EScreenUIType TargetUIType)
    {
        foreach (GameObject Obj in _ActiveUIList)
        {
            UT_ScreenUI UIComp = Obj.GetComponent<UT_ScreenUI>();
            if (UIComp != null && UIComp.Type == TargetUIType)
            {
                _ActiveUIList.Remove(Obj);
                UIComp.OnClose();
                UIComp.DestroySelf();
                break;
            }
        }
    }

    public void ShowScreenUI(UT_EScreenUIType TargetUIType)
    {
        foreach (GameObject Obj in _ActiveUIList)
        {
            UT_ScreenUI UIComp = Obj.GetComponent<UT_ScreenUI>();
            if (UIComp != null && UIComp.Type == TargetUIType)
            {
                Obj.SetActive(true);
                UIComp.OnShow();
                break;
            }
        }
    }

    public void HideScreenUI(UT_EScreenUIType TargetUIType)
    {
        foreach (GameObject Obj in _ActiveUIList)
        {
            UT_ScreenUI UIComp = Obj.GetComponent<UT_ScreenUI>();
            if (UIComp != null && UIComp.Type == TargetUIType)
            {
                Obj.SetActive(false);
                UIComp.OnHide();
                break;
            }
        }
    }

    public void CloseAllScreenUI()
    {
        foreach (GameObject Obj in _ActiveUIList)
        {
            UT_ScreenUI UIComp = Obj.GetComponent<UT_ScreenUI>();
            UIComp.OnClose();
            UIComp.DestroySelf();
        }

        _ActiveUIList.Clear();
    }

    public bool IsScreenUIActive(UT_EScreenUIType CheckedUIType)
    {
        foreach (GameObject Obj in _ActiveUIList)
        {
            UT_ScreenUI UIComponent = Obj.GetComponent<UT_ScreenUI>();
            if (UIComponent != null && UIComponent.Type == CheckedUIType)
            {
                return true;
            }
        }

        return false;
    }

    private string GetUIPrefabAddress(UT_EScreenUIType ScreenUIType)
    {
        return ScreenUIType.ToString();
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
