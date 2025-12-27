using System;
using UnityEngine;

public interface UT_IUIService
{
    public void OpenUI(string UITypeKey, UT_UIParams Params);
    public void CloseUI(string UITypeKey);
    public void CloseAllUI();
}
