using UnityEngine;

public class GIB_ManagerHub
{
    public GIB_EventManager EventManager { get; }
    public GIB_TileManager TileManager { get; }
    public GIB_XLuaManager XLuaManager { get; }
    public GIB_ModManager ModManager { get; }

    private static GIB_ManagerHub _Instance;

    static public GIB_ManagerHub Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new GIB_ManagerHub();
            }

            return _Instance;
        }
    }

    private GIB_ManagerHub()
    {
        EventManager = new GIB_EventManager();
        TileManager = new GIB_TileManager();
        XLuaManager = new GIB_XLuaManager();
        ModManager = new GIB_ModManager();
    }

    public void Initialize()
    {
        InitManager<GIB_EventManager>(EventManager);
        InitManager<GIB_ModManager>(ModManager);
        InitManager<GIB_TileManager>(TileManager);
        InitManager<GIB_XLuaManager>(XLuaManager);
    }

    public void Destroy()
    {
        DestroyManager<GIB_XLuaManager>(XLuaManager);
        DestroyManager<GIB_TileManager>(TileManager);
        DestroyManager<GIB_ModManager>(ModManager);
        DestroyManager<GIB_EventManager>(EventManager);
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
