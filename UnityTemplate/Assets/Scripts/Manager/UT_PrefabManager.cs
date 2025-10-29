using System;
using UnityEngine.AddressableAssets;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

public class UT_PrefabManager : UT_Manager, UT_IPrefabService
{
    override public string ManagerName => "Prefab Manager";

    private UT_IEventService _EventService;
    private UT_SO_PrefabConfig _PrefabConfig;
    private Dictionary<Hash128, AsyncOperationHandle<GameObject>> _PrefabHandleDict = new Dictionary<Hash128, AsyncOperationHandle<GameObject>>();

    public UT_PrefabManager(UT_IEventService EventServie, UT_SO_PrefabConfig PrefabConfig)
    {
        _EventService = EventServie;
        _PrefabConfig = PrefabConfig;
    }

    override public void Initialize()
    {
        foreach (string Address in _PrefabConfig.PrefabAddressList)
        {
            if (string.IsNullOrEmpty(Address) == false)
            {
                AsyncOperationHandle<GameObject> Handle = Addressables.LoadAssetAsync<GameObject>(Address);
                Handle.Completed += OnPrefabLoaded;
                _PrefabHandleDict.Add(Hash128.Compute(Address), Handle);
            }
        }
    }

    override public void Destroy()
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

    private void OnPrefabLoaded(AsyncOperationHandle<GameObject> Handle)
    {
        bool IsAllCompleted = true;
        foreach (var Iter in _PrefabHandleDict.Values)
        {
            if (Iter.IsDone == false)
            {
                IsAllCompleted = false;
                break;
            }
        }

        if (IsAllCompleted)
        {
            _EventService.SendEvent<M3_Event_PrefabsLoadCompleted>();
        }
    }
}
