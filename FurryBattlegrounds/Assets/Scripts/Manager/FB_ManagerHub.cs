using UnityEngine;

public class FB_ManagerHub
{
    public FB_EventManager EventManager { get; private set; }
    public FB_TileManager TileManager { get; private set; }
    public FB_UnitManager UnitManager { get; private set; }
    public FB_XLuaManager XLuaManager { get; private set; }
    public FB_ModManager ModManager { get; private set; }
    public FB_LocaleManager LocaleManager { get; private set; }
    public FB_PathManager PathManager { get; private set; }
    public FB_PrefabManager PrefabManager { get; private set; }

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

    public void Initialize()
    {
        EventManager = new FB_EventManager();
        TileManager = new FB_TileManager();
        UnitManager = new FB_UnitManager();
        XLuaManager = new FB_XLuaManager();
        ModManager = new FB_ModManager();
        LocaleManager = new FB_LocaleManager();
        PathManager = new FB_PathManager();
        PrefabManager = new FB_PrefabManager();

        // First initialize
        InitManager<FB_PrefabManager>(PrefabManager);
        InitManager<FB_PathManager>(PathManager);
        InitManager<FB_EventManager>(EventManager);
        InitManager<FB_XLuaManager>(XLuaManager);

        // Second initialize
        InitManager<FB_LocaleManager>(LocaleManager);
        InitManager<FB_TileManager>(TileManager);
        InitManager<FB_UnitManager>(UnitManager);

        // Third initialize
        InitManager<FB_ModManager>(ModManager);
    }

    public void Destroy()
    {
        // First destroy
        DestroyManager<FB_LocaleManager>(LocaleManager);
        DestroyManager<FB_TileManager>(TileManager);
        DestroyManager<FB_UnitManager>(UnitManager);

        // Second destroy
        DestroyManager<FB_ModManager>(ModManager);
        DestroyManager<FB_PathManager>(PathManager);
        DestroyManager<FB_XLuaManager>(XLuaManager);

        // Third destroy
        DestroyManager<FB_EventManager>(EventManager);
        DestroyManager<FB_PrefabManager>(PrefabManager);
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
