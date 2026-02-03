using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{

    public static UiController instance = null;

    public DiceSkin defaultDiceSkin;

    [Header("인게임 정보 UI (상시 표시)")]
    public TextMeshProUGUI roundInfoText; 
    public TextMeshProUGUI lifeText;     
    public TextMeshProUGUI targetScoreInfoText;
    public TextMeshProUGUI myScoreInfoText;

    [Header("라운드 결과 패널 (승리/패배)")]
    public GameObject resultPanel;
    public TextMeshProUGUI resultTitleText;  
    public TextMeshProUGUI resultTargetScoreText;
    public TextMeshProUGUI resultMyScoreText;
    public TextMeshProUGUI resultLifeText; 

    [Header("게임 오버 패널")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI goRoundText; 
    public TextMeshProUGUI goBestScoreText;
    public Image[] lastDice;

    [Header("다시 던지기")]
    public Button rollBtn;
    public TextMeshProUGUI rerollText;


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

    private void Start()
    {
        AudioManager.instance.PlayBgm(AudioManager.Bgm.Battle, true);
    }

    public void UpdateInGameInfo(int round, int lives, int currentScore, int targetScore)
    {
        if(roundInfoText)
        {
            roundInfoText.text = $"Round: {round.ToString()}";
        }
        if(lifeText)
        {
            lifeText.text = $"Lives: {lives.ToString()}";
        }
        if(targetScoreInfoText)
        {
            targetScoreInfoText.text = $"Target Score: {targetScore.ToString()}";
        }
        if(myScoreInfoText)
        {
            myScoreInfoText.text = $"My Score: {currentScore.ToString()}";
        }
    }

    public void HideAllPanels()
    {
        if (resultPanel) resultPanel.SetActive(false);
        if (gameOverPanel) gameOverPanel.SetActive(false);
    }

    public void ShowResultPanel(bool isSuccess, int targetScore, int currentScore, int currentLife)
    {
        if (resultPanel != null)
        {
            resultPanel.SetActive(true);
        }

        if(isSuccess)
        {   
            resultTitleText.text = "ROUND CLEAR!";
        }
        else
        {
            resultTitleText.text = "ROUND FAILED!";
        }

        if (resultTargetScoreText) 
        {
            resultTargetScoreText.text = $"Target Score: {targetScore}";
        }

        if (resultMyScoreText) 
        {
            resultMyScoreText.text = $"My Score: {currentScore}";
        }

        if(resultLifeText)
        {
            resultLifeText.text = $"Life Left: ♥ X {currentLife}";
        }
    }

    public void ShowGameOverPanel(int round, int bestScore, List<DiceData> datas, List<int> values)
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (goRoundText)
        {
            goRoundText.text = $"You reached Round: {round}";
        }

        if (goBestScoreText)
        {
            goBestScoreText.text = $"Your Best Score: {bestScore}";
        }

        if(lastDice != null)
        {
            for (int i = 0; i < lastDice.Length; i++)
            {
                if(i < values.Count)
                {
                    lastDice[i].gameObject.SetActive(true);

                    DiceData data = null;
                    
                    if(datas != null && i < datas.Count)
                    {
                        data = datas[i];
                    }
                    int index = values[i];

                    if(data != null && data.skin != null)
                    {
                        lastDice[i].sprite = data.skin.GetSprite(index);
                    }
                    else if(defaultDiceSkin != null)
                    {
                        lastDice[i].sprite = defaultDiceSkin.GetSprite(index);
                    }
                }
                else
                {
                    lastDice[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void UpdateRerollInfo(int count, bool isFirst)
    {
        if(rerollText != null)
        {
            rerollText.text = isFirst ? "Roll" : $"Reroll :{count}";
        }
    }

    public void SetRollBtnInteractable(bool state)
    {
        if(rollBtn != null)
        {
            rollBtn.interactable = state;
        }
    }

    public void GotoLobby()
    {
        GameManager.instance.LoadHomeScreen();
    }

}
