using UnityEngine;

public class UiController : MonoBehaviour
{

    public static UiController instance = null;
    public GameObject UIWindow = null;

    [Header("사운드")]
    public GameObject bgmVolumeImage;
    public GameObject bgmVolumeMuteImage;
    public GameObject sfxVolumeImage;
    public GameObject sfxVolumeMuteImage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenUI()
    {
        if (UIWindow)
        {
            UIWindow.SetActive(true);
        }
    }

    public void CloseUI()
    {
        if (UIWindow)
        {
            Debug.Log("Close");
            UIWindow.SetActive(false);
        }
    }

    public void ChangeScreenMode(int index) // 화면 모드 전환
    {
        switch (index)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                Debug.Log("전체화면");
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                Debug.Log("창모드");
                break;
            default:
                Debug.Log("Error");
                break;
        }
    }

    public void ChangeResolution(int index) // 화면 해상도 조절
    {
        switch (index)
        {
            case 0:
                Screen.SetResolution(1920, 1080, Screen.fullScreenMode);
                break;
            case 1:
                Screen.SetResolution(1600, 900, Screen.fullScreenMode);
                break;
            case 2:
                Screen.SetResolution(1280, 720, Screen.fullScreenMode);
                break;
            default:
                break;
        }
    }

    public void SetBgmVolume(float volume) // 배경음 조절
    {
        SoundManager.instance.SetBgmVolume(volume);
    }

    public void SetSfxVolume(float volume) // 효과음 조절
    {
        SoundManager.instance.SetSfxVolume(volume);
    }

    public void FBgmButton()
    {
        if (!SoundManager.instance.bgmVolumeMute) // 음소거 상태 아닐때
        {
            //bgmVolumeImage.SetActive(false);
            //bgmVolumeMuteImage.SetActive(true);
        }
        else // 음소거 상태
        {
            //bgmVolumeImage.SetActive(true);
            //bgmVolumeMuteImage.SetActive(false);
        }
        SoundManager.instance.bgmVolumeMute = !SoundManager.instance.bgmVolumeMute;
        SetBgmVolume(SoundManager.instance.bgmVolume);
    }

    public void FsfxButton()
    {
        if (!SoundManager.instance.sfxVolumeMute) // 음소거 상태 아닐때
        {
            //sfxVolumeImage.SetActive(false);
            //sfxVolumeMuteImage.SetActive(true);
        }
        else // 음소거 상태
        {
            //sfxVolumeImage.SetActive(true);
            //sfxVolumeMuteImage.SetActive(false);
        }
        SoundManager.instance.sfxVolumeMute = !SoundManager.instance.sfxVolumeMute;
        SetSfxVolume(SoundManager.instance.sfxVolume);
    }

}
