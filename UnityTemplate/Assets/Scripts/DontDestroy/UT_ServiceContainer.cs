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

    private UT_EventService _EventService = null;
    private UT_SpawnService _SpawnService = null;
    private UT_UIService _UIService = null;

    private void LateUpdate()
    {
        if (_EventManager != null)
        {
            _EventManager.Update();
        }
    }

    public void Initialize(UT_FServiceContainerInitParams Params)
    {
        if (!CheckObject<UT_SO_GameConfig>(Params.GameConfig))               return;
        if (!CheckObject<Camera>(Params.GameConfig.MainCameraPrefab))        return;
        if (!CheckObject<UT_UIRoot>(Params.GameConfig.UIRootPrefab))         return;
        if (!CheckObject<UT_AudioRoot>(Params.GameConfig.AudioRootPrefab))   return;
        if (!CheckObject<UT_EventService>(Params.GameConfig.EventService))   return;
        if (!CheckObject<UT_SpawnService>(Params.GameConfig.SpawnService))   return;
        if (!CheckObject<UT_UIService>(Params.GameConfig.UIService))         return;

        UT_AudioRoot AudioRoot = Instantiate(Params.GameConfig.AudioRootPrefab);

        // Event Service
        _EventManager = new UT_EventManager();
        InitManager<UT_EventManager>(_EventManager);
        _EventService = Instantiate(Params.GameConfig.EventService, transform);
        _EventService.Initialize(_EventManager);
        // Prefab Service
        _PrefabManager = new UT_PrefabManager(_EventService, Params.PrefabConfig);
        InitManager<UT_PrefabManager>(_PrefabManager);
        _SpawnService = Instantiate(Params.GameConfig.SpawnService, transform);
        _SpawnService.Initialize(_PrefabManager);
        // UI Service
        UT_UIRoot UIRoot = Instantiate(Params.GameConfig.UIRootPrefab);
        _UIManager = new UT_UIManager(UIRoot, _SpawnService);
        InitManager<UT_UIManager>(_UIManager);
        _UIService = Instantiate(Params.GameConfig.UIService, transform);
        _UIService.Initialize(_UIManager);

        _EventService.Subscribe<UT_Event_PrefabsLoadCompleted>((Event) => {
            Debug.Log("测试事件系统");
        });

        _EventService.Dispatch<UT_Event_PrefabsLoadCompleted>();
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
