using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreenUI : MonoBehaviour
{
    public static HomeScreenUI instance;
    public AudioClip clip;
    [Header("text")]
    public TextMeshProUGUI bestRoundtext;
    public TextMeshProUGUI bestScoretext;

  
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

    void Start()
    {
        bestRoundtext.text = "최고 라운드: " + Player.instance.player.bestRound.ToString();
        bestScoretext.text = "최고 점수: " + Player.instance.player.bestScore.ToString();
        AudioManager.instance.PlayBgm(AudioManager.Bgm.Home, true);  
    }

}
