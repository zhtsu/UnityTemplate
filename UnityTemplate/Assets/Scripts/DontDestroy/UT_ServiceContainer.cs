using UnityEngine;

public struct UT_FServiceContainerInitParams
{
    public UT_AudioRoot AudioRoot;
    public UT_UIRoot UIRoot;
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

    public void Initialize(UT_FServiceContainerInitParams InitParams)
    {
        _EventManager = new UT_EventManager();
        _PrefabManager = new UT_PrefabManager(_EventManager, InitParams.PrefabConfig);
        _UIManager = new UT_UIManager(InitParams.UIRoot);

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
        Manager.Initialize();
        Debug.Log(Manager.ManagerName + " initialized!");
    }

    private void DestroyManager<T>(T Manager)
        where T : UT_Manager
    {
        Manager.Destroy();
        Debug.Log(Manager.ManagerName + " destroyed!");
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
