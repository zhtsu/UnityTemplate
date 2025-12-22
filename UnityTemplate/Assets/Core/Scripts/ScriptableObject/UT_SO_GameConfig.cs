using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "UT Config/Game Config")]
public class UT_SO_GameConfig : ScriptableObject
{
    [SerializeField] private UT_UIRoot _UIRootPrefab;
    public UT_UIRoot UIRootPrefab => _UIRootPrefab;

    [SerializeField] private Camera _MainCameraPrefab;
    public Camera MainCameraPrefab => _MainCameraPrefab;

    [SerializeField] private UT_AudioRoot _AudioRootPrefab;
    public UT_AudioRoot AudioRootPrefab => _AudioRootPrefab;

    [SerializeField] private UT_ServiceContainer _ServiceContainerPrefab;
    public UT_ServiceContainer ServiceContainerPrefab => _ServiceContainerPrefab;

    [SerializeField] private float _MouseDragThreshold = 5f;
    public float MouseDragThreshold => _MouseDragThreshold;
};