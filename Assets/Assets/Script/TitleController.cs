using UnityEngine;

public class TitleController : MonoBehaviour
{
    public TitleUiManager uiManager;

    [Header("타이틀 전용 사운드")]
    public AudioClip titleBgm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiManager.InitUI();
        
        if(SoundManager.instance != null)
        {
            SoundManager.instance.PlayBgm(titleBgm);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(uiManager.exitPanel.activeSelf)
            {
                OnClickCancelExit();
            }
            else
            {
                OnClickExit();
            }
        }
    }

    public void OnClickStart()
    {
        Debug.Log("로비 씬으로 이동~");
    }
    
    public void OnClickExit()
    {
        uiManager.OpenExitPanel();
    }

    public void OnClickConfirmExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnClickCancelExit()
    {
        uiManager.CloseExitPanel();
    }

}
