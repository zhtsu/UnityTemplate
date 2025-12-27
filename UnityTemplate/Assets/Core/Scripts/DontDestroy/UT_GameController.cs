using Cysharp.Threading.Tasks;
using UnityEngine;

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

    private async void Start()
    {
        Instantiate(_GameConfig.MainCameraPrefab);

        UT_UI_LoadingScreen LoadingScreen = null;
        var Results_LSP = await InstantiateAsync(_UIConfig.LoadingScreenPrefab).ToUniTask();
        if (Results_LSP.Length > 0)
        {
            LoadingScreen = Results_LSP[0].GetComponent<UT_UI_LoadingScreen>();
            if (LoadingScreen == null)
            {
                Debug.LogError("LoadingScreen Component is missing in the prefab!");
            }
        }

        UT_FServiceContainerInitParams Params = new();
        Params.GameConfig = _GameConfig;
        Params.PrefabConfig = _PrefabConfig;
        Params.UIConfig = _UIConfig;
        Params.AudioConfig = _AudioConfig;

        var Results_SCP = await InstantiateAsync(_GameConfig.ServiceContainerPrefab).ToUniTask();
        if (Results_SCP.Length > 0)
        {
            _ServiceContainer = Results_SCP[0].GetComponent<UT_ServiceContainer>();
            if (_ServiceContainer == null)
            {
                Debug.LogError("ServiceContainer Component is missing in the prefab!");
            }
        }

        if (_ServiceContainer != null)
            await _ServiceContainer.Initialize(Params);


        Destroy(LoadingScreen.gameObject);
    }

    private void OnDestroy()
    {
        if (_ServiceContainer != null)
            _ServiceContainer.Destroy();
    }
}
