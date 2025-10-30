using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "UT Config/Game Config")]
public class UT_SO_GameConfig : ScriptableObject
{
    [SerializeField] public UT_UIRoot UIRootPrefab;
    [SerializeField] public Camera MainCameraPrefab;
    [SerializeField] public UT_AudioRoot AudioRootPrefab;

    [SerializeField] public UT_ServiceContainer ServiceContainerPrefab;
    [SerializeField] public UT_EventService EventService;
    [SerializeField] public UT_SpawnService SpawnService;
    [SerializeField] public UT_UIService UIService;

    [SerializeField] public float MouseDragThreshold = 5f;
};