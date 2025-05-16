using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GIB_Starter : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);

        GIB_ManagerHub.Instance.Initialize();
    }

    private void OnDestroy()
    {
        GIB_ManagerHub.Instance.Destroy();
    }
}
