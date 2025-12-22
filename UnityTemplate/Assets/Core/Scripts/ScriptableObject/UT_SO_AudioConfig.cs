using UnityEngine;

[CreateAssetMenu(fileName = "AudioConfig", menuName = "UT Config/Audio Config")]
public class UT_SO_AudioConfig : ScriptableObject
{
    [SerializeField] private string _BgMusicAddress;
    public string BgMusicAddress => _BgMusicAddress;


};