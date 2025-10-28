using System.IO;
using UnityEngine;

public class FB_PathManager : FB_IManager
{
    public string ManagerName
    {
        get { return "PathManager"; }
    }

    public static string ModsPath { get; private set; }

    public void Initialize()
    {
        ModsPath = NormalizePath(Path.Combine(Application.dataPath, "Mods"));
        if (!Directory.Exists(ModsPath))
        {
            Directory.CreateDirectory(ModsPath);
        }
    }

    public void Destroy()
    {

    }

    public static string GenerateModFilePath(string FilePath)
    {
        return NormalizePath(Path.Combine(ModsPath, FilePath));
    }

    private static string NormalizePath(string InPath)
    {
        return InPath.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
    }
}
