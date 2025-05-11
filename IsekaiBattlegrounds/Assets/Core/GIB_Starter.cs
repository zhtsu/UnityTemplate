using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GIB_Starter : MonoBehaviour
{
    private void Start()
    {
        GIB_ManagerHub.Instance.Initialize();

        GIB_ManagerHub.Instance.EventManager.Subscribe<GIB_Event_UnitEnterTile>(Test);
        GIB_ManagerHub.Instance.EventManager.SendEvent<GIB_Event_UnitEnterTile>(new GIB_Event_UnitEnterTile());
        GIB_ManagerHub.Instance.EventManager.SendEvent<GIB_Event_UnitEnterTile>(new GIB_Event_UnitEnterTile());
    }

    private void OnDestroy()
    {
        GIB_ManagerHub.Instance.Destroy();
    }

    private void Test(GIB_Event_UnitEnterTile UETEvent)
    {
        Debug.Log(UETEvent.Id);
    }
}
