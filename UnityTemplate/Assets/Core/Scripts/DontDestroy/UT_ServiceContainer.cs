using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public struct UT_FServiceContainerInitParams
{
    public UT_SO_GameConfig GameConfig;
    public UT_SO_PrefabConfig PrefabConfig;
    public UT_SO_UIConfig UIConfig;
    public UT_SO_AudioConfig AudioConfig;
}

public class UT_ServiceContainer : MonoBehaviour
{
    private UT_EventService _EventService = null;
    private UT_CommandService _CommandService = null;
    private UT_PrefabService _PrefabService = null;
    private UT_UIService _UIService = null;
    private UT_GameStateService _GameStateService = null;
    private UT_AudioService _AudioService = null;

    private void LateUpdate()
    {
        if (_EventService != null)
            _EventService.Update();
    }

    public async UniTask Initialize(UT_FServiceContainerInitParams Params)
    {
        // Create Services
        _EventService = new UT_EventService();
        _CommandService = new UT_CommandService();
        _PrefabService = new UT_PrefabService(Params.PrefabConfig);
        _UIService = new UT_UIService(Params.UIConfig);
        _GameStateService = new UT_GameStateService();
        _AudioService = new UT_AudioService(Params.AudioConfig);

        // Initialize Services
        _ = _EventService.Initialize();
        _ = _CommandService.Initialize();
        _ = _GameStateService.Initialize();
        await _UIService.Initialize();
        await _PrefabService.Initialize();
        await _AudioService.Initialize();

        // Initialize Managers
    }

    public void Destroy()
    {
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
