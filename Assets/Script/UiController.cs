using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{

    public static UiController instance = null;

    [Header("1. 인게임 정보 UI (상시 표시)")]
    public TextMeshProUGUI roundInfoText; 
    public TextMeshProUGUI lifeText;     
    public TextMeshProUGUI targetScoreInfoText;
    public TextMeshProUGUI myScoreInfoText;

    [Header("2. 라운드 결과 패널 (승리/패배)")]
    public GameObject resultPanel;
    public TextMeshProUGUI resultTitleText;  
    public TextMeshProUGUI resultTargetScoreText;
    public TextMeshProUGUI resultMyScoreText;
    public TextMeshProUGUI resultLifeText; 

    [Header("3. 게임 오버 패널")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI goRoundText; 
    public TextMeshProUGUI goBestScoreText;
    public Image[] lastDice;


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

    public void ShowGameOverpanel(int round, int bestScore, Sprite[] lastDiceImages)
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

        if(lastDice != null && lastDiceImages != null)
        {
            for (int i = 0; i < lastDice.Length; i++)
            {
                if(i < lastDiceImages.Length)
                {
                    lastDice[i].gameObject.SetActive(true);
                    lastDice[i].sprite = lastDiceImages[i];
                }
                else
                {
                    lastDice[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void GotoLobby()
    {
        Debug.Log("로비 씬으로 이동~");
    }

}
