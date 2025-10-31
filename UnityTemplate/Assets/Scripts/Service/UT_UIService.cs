using System;
using System.Collections.Generic;
using UnityEngine;



public class UT_UIService : MonoBehaviour, UT_IUIService
{
    private UT_UIManager _UIManager;

    public void Initialize(UT_UIManager InUIManager)
    {
        _UIManager = InUIManager;
    }

    public void CloseAllScreenUI()
    {
        throw new NotImplementedException();
    }

    public void CloseScreenUI(UT_EScreenUIType ScreenUIType)
    {
        throw new NotImplementedException();
    }

    public void HideScreenUI(UT_EScreenUIType ScreenUIType)
    {
        throw new NotImplementedException();
    }

    public bool IsScreenUIActive(UT_EScreenUIType ScreenUIType)
    {
        throw new NotImplementedException();
    }

    public void OpenScreenUI(UT_EScreenUIType ScreenUIType, UT_UIParams Params, UT_EUILayer Layer)
    {
        throw new NotImplementedException();
    }

    public void ShowScreenUI(UT_EScreenUIType ScreenUIType)
    {
        throw new NotImplementedException();
    }
}
