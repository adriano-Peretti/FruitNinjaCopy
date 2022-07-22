using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManagerInstance;

    [SerializeField]
    public AudioSource backgroundMusic;

    [SerializeField]
    AudioSource blade_SFX;

    [SerializeField]
    AudioSource Bomb_SFX;

    [SerializeField]
    AudioSource powerUp_SFX;

    public float fadeTime = 1;

    public float volumeVFX;


    void Start()
    {
        backgroundMusic.Play();
        if (audioManagerInstance == null)
        {
            audioManagerInstance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void BladeSoundEffect()
    {

        if (AudioControlers.toggleVfx == true)
        {
            blade_SFX.volume = volumeVFX;
            blade_SFX.Play();
        }
        else
        {
            return;
        }
    }

    public void BombSoundEffect()
    {
        if (AudioControlers.toggleVfx == true)
        {
            Bomb_SFX.volume = volumeVFX;
            Bomb_SFX.Play();
        }
        else
        {
            return;
        }
    }

    public void PowerUpSoundEffect()
    {
        if (AudioControlers.toggleVfx == true)
        {
            powerUp_SFX.volume = volumeVFX;
            powerUp_SFX.Play();
        }
        else
        {
            return;
        }
    }


}
