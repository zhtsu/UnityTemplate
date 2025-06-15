using System;
using UnityEngine;

public class FB_UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject BottomLayer;
    [SerializeField]
    private GameObject MainLayer;
    [SerializeField]
    private GameObject TopLayer;
    [SerializeField]
    private GameObject PopupLayer;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void OnDestroy()
    {
    }
}
