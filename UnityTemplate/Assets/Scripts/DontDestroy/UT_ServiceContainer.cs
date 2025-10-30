using UnityEngine;

public struct UT_FServiceContainerInitParams
{
    public UT_SO_GameConfig GameConfig;
    public UT_SO_PrefabConfig PrefabConfig;
}

public class UT_ServiceContainer : MonoBehaviour
{
    private UT_EventManager _EventManager = null;
    private UT_UIManager _UIManager = null;
    private UT_AudioManager _AudioManager = null;
    private UT_PrefabManager _PrefabManager = null;
    private UT_ScriptManager _ScriptManager = null;

    public UT_EventManager EventManager { get { return _EventManager; } }
    public UT_UIManager UIManager { get { return _UIManager; } }
    public UT_AudioManager AudioManager { get { return _AudioManager; } }
    public UT_PrefabManager PrefabManager { get { return _PrefabManager; } }
    public UT_ScriptManager ScriptManager { get { return _ScriptManager; } }

    public void Initialize(UT_FServiceContainerInitParams Params)
    {
        if (!CheckObject<UT_SO_GameConfig>(Params.GameConfig))               return;
        if (!CheckObject<Camera>(Params.GameConfig.MainCameraPrefab))        return;
        if (!CheckObject<UT_UIRoot>(Params.GameConfig.UIRootPrefab))         return;
        if (!CheckObject<UT_AudioRoot>(Params.GameConfig.AudioRootPrefab))   return;
        if (!CheckObject<UT_EventService>(Params.GameConfig.EventService))   return;
        if (!CheckObject<UT_SpawnService>(Params.GameConfig.SpawnService))   return;
        if (!CheckObject<UT_UIService>(Params.GameConfig.UIService))         return;

        UT_UIRoot UIRoot = Instantiate(Params.GameConfig.UIRootPrefab);
        UT_AudioRoot AudioRoot = Instantiate(Params.GameConfig.AudioRootPrefab);

        UT_EventService EventService = Instantiate(Params.GameConfig.EventService, transform);
        UT_SpawnService SpawnService = Instantiate(Params.GameConfig.SpawnService, transform);
        UT_UIService UIService = Instantiate(Params.GameConfig.UIService, transform);

        _EventManager = new UT_EventManager();
        _PrefabManager = new UT_PrefabManager(EventService, Params.PrefabConfig);
        _UIManager = new UT_UIManager(UIRoot, SpawnService);

        InitManager<UT_EventManager>(_EventManager);
        InitManager<UT_PrefabManager>(_PrefabManager);
        InitManager<UT_UIManager>(_UIManager);
    }

    public void Destroy()
    {
        DestroyManager<UT_UIManager>(_UIManager);
        DestroyManager<UT_EventManager>(_EventManager);
        DestroyManager<UT_PrefabManager>(_PrefabManager);
    }

    private void InitManager<T>(T Manager)
        where T : UT_Manager
    {
        if (Manager != null)
        {
            Manager.Initialize();
            Debug.Log(Manager.ManagerName + " initialized!");
        }
        else
        {
            Debug.LogWarning($"Invalid {Manager.ManagerName}!");
        }
    }

    private void DestroyManager<T>(T Manager)
        where T : UT_Manager
    {
        if (Manager != null)
        {
            Manager.Destroy();
            Debug.Log(Manager.ManagerName + " destroyed!");
        }
        else
        {
            Debug.LogWarning($"Invalid {Manager.ManagerName}!");
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private bool CheckObject<T>(object Object)
    {
        if (Object == null || Object is not T)
        {
            Debug.LogWarning($"Failed to create {typeof(T)}!");
            return false;
        }

        return true;
    }
}
