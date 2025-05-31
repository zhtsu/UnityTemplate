using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_LifeCycleController : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);

        FB_ManagerHub.Instance.Initialize();
    }

    private void OnDestroy()
    {
        FB_ManagerHub.Instance.Destroy();
    }
}
