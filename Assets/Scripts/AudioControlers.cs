using UnityEngine;
using UnityEngine.UI;

public class AudioControlers : MonoBehaviour
{
    public static AudioControlers audioControlersInstance;
    [SerializeField]
    public Toggle musicToggle;

    [SerializeField]
    public Slider sliderVolumeMusic;

    [SerializeField]
    public Toggle vfxToggle;

    [SerializeField]
    public Slider sliderVolumeSFX;

    [SerializeField]
    public GameObject perdeuPopUp;

    public static bool toggleVfx;
    public static bool toggleMusic;

    public static float MusicVolume;

    int boolToInt(bool val)
    {
        if (val)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }

    private void Awake()
    {
        musicToggle.isOn = intToBool(PlayerPrefs.GetInt("MusicToogle", 0));
        vfxToggle.isOn = intToBool(PlayerPrefs.GetInt("VfxToogle", 0));
        sliderVolumeMusic.value = PlayerPrefs.GetFloat("MusicVolume", sliderVolumeMusic.value);
        sliderVolumeSFX.value = PlayerPrefs.GetFloat("VfxVolume", sliderVolumeSFX.value);
    }


    public void Update()
    {
        ToggleMusic();
        ToggleVFX();
        AjustaVolume();
    }

    public void ToggleMusic()
    {
        if (musicToggle.isOn == false)
        {
            AudioManager.audioManagerInstance.backgroundMusic.Pause();
            sliderVolumeMusic.interactable = false;
        }
        else if (musicToggle.isOn == true &&
            !AudioManager.audioManagerInstance.backgroundMusic.isPlaying)
        {
            if (perdeuPopUp.gameObject.activeSelf)
            {
                return;
            }
            else
            {
                AudioManager.audioManagerInstance.backgroundMusic.Play();
                sliderVolumeMusic.interactable = true;
            }
        }
        PlayerPrefs.SetInt("MusicToogle", boolToInt(musicToggle.isOn));
    }
    public void ToggleVFX()
    {
        if (vfxToggle.isOn == false)
        {
            toggleVfx = false;
            sliderVolumeSFX.interactable = false;
        }
        else
        {
            toggleVfx = true;
            sliderVolumeSFX.interactable = true;
        }
        PlayerPrefs.SetInt("VfxToogle", boolToInt(vfxToggle.isOn));
    }
    public void AjustaVolume()
    {
        AudioManager.audioManagerInstance.backgroundMusic.volume = sliderVolumeMusic.value;
        AudioManager.audioManagerInstance.volumeVFX = sliderVolumeSFX.value;
        PlayerPrefs.SetFloat("MusicVolume", sliderVolumeMusic.value);
        PlayerPrefs.SetFloat("VfxVolume", sliderVolumeSFX.value);
    }






}
