using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UT_EGameState
{
    None,
    Loading,
    InMainMenu,
    Playing,
    Paused,
}

public class UT_GameController : MonoBehaviour
{
    [SerializeField] private UT_SO_GameConfig _GameConfig;
    [SerializeField] private UT_SO_AudioConfig _AudioConfig;
    [SerializeField] private UT_SO_PrefabConfig _PrefabConfig;
    [SerializeField] private UT_SO_UIConfig _UIConfig;

    private UT_ServiceContainer _ServiceContainer;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public static bool IsGameReady()
    {
        return false;
    }

    private void Start()
    {
        if (_GameConfig == null)
        {
            Debug.LogWarning("Invalid GameConfig!");
            return;
        }

        if (_GameConfig.MainCameraPrefab == null)
        {
            Debug.LogWarning("Invalid Camera Prefab!");
            return;
        }

        if (_GameConfig.UIRootPrefab == null)
        {
            Debug.LogWarning("Invalid UIRoot Prefab!");
            return;
        }

        if (_GameConfig.AudioRootPrefab == null)
        {
            Debug.LogWarning("Invalid AudioRoot Prefab!");
            return;
        }

        if (_GameConfig.ServiceContainerPrefab == null)
        {
            Debug.LogWarning("Invalid ServiceContainer Prefab!");
            return;
        }

        Camera MainCamera = Instantiate(_GameConfig.MainCameraPrefab).GetComponent<Camera>();
        UT_UIRoot UIRoot = Instantiate(_GameConfig.UIRootPrefab).GetComponent<UT_UIRoot>();
        UT_AudioRoot AudioRoot = Instantiate(_GameConfig.AudioRootPrefab).GetComponent<UT_AudioRoot>();

        _ServiceContainer = Instantiate(_GameConfig.ServiceContainerPrefab).GetComponent<UT_ServiceContainer>();
        UT_FServiceContainerInitParams Params = new();
        Params.UIRoot = UIRoot;
        Params.AudioRoot = AudioRoot;
        Params.PrefabConfig = _PrefabConfig;
        _ServiceContainer.Initialize(Params);
    }

    private void OnDestroy()
    {
        if (_ServiceContainer != null)
        {
            _ServiceContainer.Destroy();
        }
    }
}
