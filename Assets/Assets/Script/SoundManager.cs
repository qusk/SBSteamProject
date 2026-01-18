using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("볼륨")]
    public bool bgmVolumeMute;
    public bool sfxVolumeMute;
    public float bgmVolume;
    public float sfxVolume;

    [Header("오디오 소스")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void PlayBgm(AudioClip clip)
    {
        if (bgmSource == clip) return;

        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlaySfx(AudioClip clip)
    {
        if(clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void SetBgmVolume(float volume)
    {
        if (!bgmVolumeMute)
        {
            bgmSource.mute = false;
            bgmVolume = volume;
            bgmSource.volume = bgmVolume;
        }
        else
        {
            bgmSource.mute = true;
        }
    }

    public void SetSfxVolume(float volume)
    {
        if (!sfxVolumeMute)
        {
            sfxSource.mute = false;
            sfxVolume = volume;
            sfxSource.volume = sfxVolume;
        }
        else
        {
            sfxSource.mute = true;
        }
    }
}
