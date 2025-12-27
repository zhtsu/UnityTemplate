using Cysharp.Threading.Tasks;
using UnityEngine;

public class UT_AudioService : UT_Service, UT_IAudioService
{
    public override string ServiceName => "Audio Service";

    private UT_SO_AudioConfig _AudioConfig;
    private UT_AudioRoot _AudioRoot;

    public UT_AudioService(UT_SO_AudioConfig AudioConfig)
    {
        _AudioConfig = AudioConfig;
    }

    public override void Destroy()
    {

    }

    public override async UniTask Initialize()
    {
        if (_AudioConfig.AudioRootPrefab == null)
        {
            Debug.LogError("Audio Root Prefab is null!");
            return;
        }

        var Results = await UnityEngine.Object.InstantiateAsync(_AudioConfig.AudioRootPrefab).ToUniTask();
        if (Results.Length > 0)
        {
            _AudioRoot = Results[0].GetComponent<UT_AudioRoot>();
            if (_AudioRoot == null)
            {
                Debug.LogError("Audio Root Component is missing in the prefab!");
            }
        }
    }
}
