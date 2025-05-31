using System;
using UnityEngine.AddressableAssets;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class FB_PrefabManager : FB_IManager
{
    public string ManagerName
    {
        get { return "PrefabManager"; }
    }

    private const string _SquareTilePrefabAddress = "Assets/Prefabs/SquareTile.prefab";

    private GameObject _SquareTilePrefab;

    public void Initialize()
    {
        Addressables.LoadAssetAsync<GameObject>(_SquareTilePrefabAddress).Completed += OnSquareTilePrefabLoaded;
    }

    public void Destroy()
    {

    }

    void OnSquareTilePrefabLoaded(AsyncOperationHandle<GameObject> Handle)
    {
        if (Handle.Status == AsyncOperationStatus.Succeeded)
        {
            _SquareTilePrefab = Handle.Result;
            Debug.Log("Square tile prefab loaded");
        }
    }
}
