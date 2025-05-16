using UnityEngine;

public class FB_ManagerHub
{
    public FB_EventManager EventManager { get; }
    public FB_TileManager TileManager { get; }
    public FB_XLuaManager XLuaManager { get; }
    public FB_ModManager ModManager { get; }
    public FB_DataManager DataManager { get; }

    private static FB_ManagerHub _Instance;

    static public FB_ManagerHub Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new FB_ManagerHub();
            }

            return _Instance;
        }
    }

    private FB_ManagerHub()
    {
        EventManager = new FB_EventManager();
        TileManager = new FB_TileManager();
        XLuaManager = new FB_XLuaManager();
        ModManager = new FB_ModManager();
        DataManager = new FB_DataManager();
    }

    public void Initialize()
    {
        InitManager<FB_EventManager>(EventManager);
        InitManager<FB_ModManager>(ModManager);
        InitManager<FB_TileManager>(TileManager);
        InitManager<FB_XLuaManager>(XLuaManager);
        InitManager<FB_DataManager>(DataManager);
    }

    public void Destroy()
    {
        DestroyManager<FB_XLuaManager>(XLuaManager);
        DestroyManager<FB_TileManager>(TileManager);
        DestroyManager<FB_ModManager>(ModManager);
        DestroyManager<FB_EventManager>(EventManager);
        DestroyManager<FB_DataManager>(DataManager);
    }

    private void InitManager<T>(T Manager)
        where T : class, FB_IManager, new()
    {
        Manager.Initialize();
        Debug.Log(Manager.ManagerName + " initialized!");
    }

    private void DestroyManager<T>(T Manager)
        where T : class, FB_IManager
    {
        Manager.Destroy();
        Debug.Log(Manager.ManagerName + " destroyed!");
    }
}
