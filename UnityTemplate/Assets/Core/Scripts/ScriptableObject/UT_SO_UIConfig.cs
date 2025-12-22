using UnityEngine;

public struct UT_FUIConfigPair
{
    [SerializeField] public UT_EScreenUIType Type;
    [SerializeField] public string Address;
}

[CreateAssetMenu(fileName = "UIConfig", menuName = "UT Config/UI Config")]
public class UT_SO_UIConfig : ScriptableObject
{
    [SerializeField] private UT_UI_LoadingScreen _LoadingScreenPrefab;
    public UT_UI_LoadingScreen LoadingScreenPrefab => _LoadingScreenPrefab;

    [SerializeField] private UT_FUIConfigPair[] _UIPrefabAddressDict;
    public UT_FUIConfigPair[] UIPrefabAddressDict => _UIPrefabAddressDict;
};