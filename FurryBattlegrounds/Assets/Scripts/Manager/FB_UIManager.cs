using System;
using UnityEngine;

public class FB_UIManager : FB_IManager
{
    public string ManagerName
    {
        get { return "UIManager"; }
    }

    private FB_UIRoot _UIRoot;

    public void Initialize()
    {
        _UIRoot = GameObject.FindObjectOfType<FB_UIRoot>();
    }

    public void Destroy()
    {

    }
}
