using UnityEngine;

public class GIB_ManagerHub : MonoBehaviour
{
    public GIB_TerrainCellManager TerrainCellManager { get; }
    public GIB_XLuaManager XLuaManager { get; }

    public GIB_ManagerHub()
    {
        TerrainCellManager = new GIB_TerrainCellManager();
        XLuaManager = new GIB_XLuaManager();
    }

    private void Awake()
    {
        InitManager<GIB_TerrainCellManager>(TerrainCellManager);
        InitManager<GIB_XLuaManager>(XLuaManager);
    }

    private void OnDestroy()
    {
        DestroyManager<GIB_TerrainCellManager>(TerrainCellManager);
        DestroyManager<GIB_XLuaManager>(XLuaManager);
    }

    private void InitManager<T>(T Manager)
        where T : class, GIB_IManager, new()
    {
        Manager.Initialize();
        Debug.Log(Manager.ManagerName + " initialized!");
    }

    private void DestroyManager<T>(T Manager)
        where T : class, GIB_IManager
    {
        Manager.Destroy();
        Debug.Log(Manager.ManagerName + " destroyed!");
    }
}
