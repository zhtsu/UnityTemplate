using UnityEngine;

public class FB_ManagerHub
{
    public FB_EventManager EventManager { get; private set; }
    public FB_TileManager TileManager { get; private set; }
    public FB_XLuaManager XLuaManager { get; private set; }
    public FB_ModManager ModManager { get; private set; }
    public FB_LocaleManager LocaleManager { get; private set; }
    public FB_PathManager PathManager { get; private set; }

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
        XLuaManager = new FB_XLuaManager();
        ModManager = new FB_ModManager();
        LocaleManager = new FB_LocaleManager();
        PathManager = new FB_PathManager();

        // First initialize
        InitManager<FB_PathManager>(PathManager);
        InitManager<FB_EventManager>(EventManager);
        InitManager<FB_XLuaManager>(XLuaManager);

        // Second initialize
        InitManager<FB_LocaleManager>(LocaleManager);
        InitManager<FB_TileManager>(TileManager);

        // Third initialize
        InitManager<FB_ModManager>(ModManager);
    }

    public void Destroy()
    {
        // First destroy
        DestroyManager<FB_LocaleManager>(LocaleManager);
        DestroyManager<FB_TileManager>(TileManager);

        // Second destroy
        DestroyManager<FB_ModManager>(ModManager);
        DestroyManager<FB_PathManager>(PathManager);
        DestroyManager<FB_XLuaManager>(XLuaManager);

        // Third destroy
        DestroyManager<FB_EventManager>(EventManager);
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
