using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_LifeCycleController : MonoBehaviour
{
    [SerializeField]
    private GameObject UIManagerPrefab;

    private void Start()
    {
        DontDestroyOnLoad(this);

        if (UIManagerPrefab)
        {
            Instantiate(UIManagerPrefab);
        }

        FB_ManagerHub.Instance.Initialize();
    }

    private void OnDestroy()
    {
        FB_ManagerHub.Instance.Destroy();
    }
}
