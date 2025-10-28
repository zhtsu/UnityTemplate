using System;
using UnityEngine.AddressableAssets;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

public enum FB_PrefabType
{
    SquareTile = 0,
}

public class FB_PrefabManager : FB_IManager
{
    public string ManagerName
    {
        get { return "PrefabManager"; }
    }

    private string[] _PrefabAddressList = {
        // Tile
        "Assets/Prefabs/SquareTile.prefab"
    };

    private static List<AsyncOperationHandle<GameObject>> _PrefabHandleList = new List<AsyncOperationHandle<GameObject>>();

    public void Initialize()
    {
        foreach (string Address in _PrefabAddressList)
        {
            Addressables.LoadAssetAsync<GameObject>(Address).Completed += OnPrefabLoaded;
        }
    }

    public void Destroy()
    {
        foreach (AsyncOperationHandle<GameObject> Handle in _PrefabHandleList)
        {
            Addressables.Release(Handle);
        }
    }

    public static GameObject GetPrefab(FB_PrefabType PrefabType)
    {
        if (_PrefabHandleList.Count > (int)PrefabType)
        {
            AsyncOperationHandle<GameObject> Handle = _PrefabHandleList[(int)PrefabType];
            return Handle.Result;
        }

        return null;
    }

    private void OnPrefabLoaded(AsyncOperationHandle<GameObject> Handle)
    {
        if (Handle.Status == AsyncOperationStatus.Succeeded)
        {
            _PrefabHandleList.Add(Handle);
            Debug.Log(Handle.DebugName);

            if (_PrefabHandleList.Count == _PrefabAddressList.Length)
            {
                FB_EventManager.SendEvent<FB_Event_PrefabsLoadCompleted>();
            }
        }
    }
}
