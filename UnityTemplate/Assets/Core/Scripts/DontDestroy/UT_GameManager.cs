using Cysharp.Threading.Tasks;
using UnityEngine;

public class UT_GameManager : MonoBehaviour
{
    [SerializeField] private UT_SO_GameConfig _GameConfig;
    [SerializeField] private UT_SO_AudioConfig _AudioConfig;
    [SerializeField] private UT_SO_PrefabConfig _PrefabConfig;
    [SerializeField] private UT_SO_UIConfig _UIConfig;

    private UT_Boot _Boot = new CG_Boot();
    private UT_ServiceLocator _ServiceLocator;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private async void Start()
    {
        GameObject CameraObj = Instantiate(_GameConfig.MainCameraPrefab);
        Instantiate(_GameConfig.EventSystemPrefab);
        GameObject LoadingScreen = Instantiate(_UIConfig.LoadingScreenPrefab);

        UT_FServiceContainerInitParams Params = new();
        Params.GameConfig = _GameConfig;
        Params.PrefabConfig = _PrefabConfig;
        Params.UIConfig = _UIConfig;
        Params.AudioConfig = _AudioConfig;
        Params.MainCamera = CameraObj.GetComponent<Camera>();

        GameObject ServiceLocatorInst = Instantiate(_GameConfig.ServiceLocatorPrefab);
        if (ServiceLocatorInst != null)
            _ServiceLocator = ServiceLocatorInst.GetComponent<UT_ServiceLocator>();

        if (_ServiceLocator != null)
            await _ServiceLocator.Initialize(Params);

        if (LoadingScreen != null)
            Destroy(LoadingScreen);

        if (_Boot != null)
            _Boot.Initialize(_ServiceLocator);
    }

    private void OnDestroy()
    {
        if (_ServiceLocator != null)
            _ServiceLocator.Destroy();
    }
}
