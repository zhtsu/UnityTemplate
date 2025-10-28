using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_LifeCycleController : MonoBehaviour
{
    [SerializeField]
    private GameObject _UIRootPrefab;

    [SerializeField]
    private GameObject _TestGrid;

    private void Start()
    {
        DontDestroyOnLoad(this);

        if (_UIRootPrefab)
        {
            Instantiate(_UIRootPrefab);
        }

        FB_EventManager.Subscribe<FB_Event_PrefabsLoadCompleted>(TestAnythingHere);

        FB_ManagerHub.Instance.Initialize();
    }

    private void OnDestroy()
    {
        FB_ManagerHub.Instance.Destroy();
    }

    private void TestAnythingHere(FB_Event_PrefabsLoadCompleted Event)
    {
        if (_TestGrid)
        {
            FB_Grid Grid = _TestGrid.GetComponent<FB_Grid>();

            Grid.Initialize(10, 10, 2.0f);
            Grid.GenetateTerrain();
        }
    }
}
