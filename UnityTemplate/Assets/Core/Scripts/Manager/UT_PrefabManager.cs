using System;
using UnityEngine.AddressableAssets;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UT_PrefabManager : UT_Manager
{
    public override string ManagerName => "Prefab Manager";

    private UT_IEventService _EventService;
    private UT_SO_PrefabConfig _PrefabConfig;
    private Dictionary<Hash128, AsyncOperationHandle<GameObject>> _PrefabHandleDict = new Dictionary<Hash128, AsyncOperationHandle<GameObject>>();

    public UT_PrefabManager(UT_IEventService EventServie, UT_SO_PrefabConfig PrefabConfig)
    {
        _EventService = EventServie;
        _PrefabConfig = PrefabConfig;
    }

    public override async Task Initialize()
    {
        List<Task> LoadTasks = new List<Task>();

        foreach (string Address in _PrefabConfig.PrefabAddressList)
        {
            if (string.IsNullOrEmpty(Address) == false)
            {
                AsyncOperationHandle<GameObject> Handle = Addressables.LoadAssetAsync<GameObject>(Address);
                LoadTasks.Add(Handle.Task);

                _PrefabHandleDict.Add(Hash128.Compute(Address), Handle);
            }
        }

        await Task.WhenAll(LoadTasks);
    }

    public override void Destroy()
    {
        foreach (AsyncOperationHandle<GameObject> Handle in _PrefabHandleDict.Values)
        {
            Addressables.Release(Handle);
        }

        _PrefabHandleDict.Clear();
    }

    public GameObject GetPrefab(string Address)
    {
        if (_PrefabHandleDict.TryGetValue(Hash128.Compute(Address), out AsyncOperationHandle<GameObject> OutHandle))
        {
            return OutHandle.Result;
        }

        return null;
    }
}
