using System;
using System.Collections.Generic;
using UnityEngine;



public class UT_SpawnService : MonoBehaviour, UT_ISpawnService
{
    private UT_IPrefabService _PrefabService;

    public UT_SpawnService(UT_IPrefabService InPrefabService)
    {
        _PrefabService = InPrefabService;
    }

    public GameObject CreateGameObject(string PrefabAddress)
    {
        GameObject Prefab = _PrefabService.GetPrefab(PrefabAddress);
        if (Prefab == null)
        {
            Debug.LogWarning($"Invalid prefab: {PrefabAddress}");
            return null;
        }

        return Instantiate(Prefab);
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
