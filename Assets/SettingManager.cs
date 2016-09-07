using UnityEngine;
using System.Collections;
using UButton = UnityEngine.UI.Button;

public class SettingManager : MonoBehaviour
{
    public UButton bgmOnButton;
    public UButton bgmOffButton;
    public UButton sfxOnButton;
    public UButton sfxOffButton;

    public void Awake()
    {
        bgmOnButton.onClick.AddListener(() => SoundManager.BgmVolume = 1f);
        bgmOffButton.onClick.AddListener(() => SoundManager.BgmVolume = 0f);
        sfxOnButton.onClick.AddListener(() => SoundManager.SfxVolume = 1f);
        sfxOffButton.onClick.AddListener(() => SoundManager.SfxVolume = 0f);
    }
}
