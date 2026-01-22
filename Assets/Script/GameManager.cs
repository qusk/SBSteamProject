using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("테스트용 설정")]
    public int round = 1;
    public int targetScore = 20;
    public int maxLifes = 3;
    public int currentLifes ;

    
    public int currentScore = 0;
    public int bestScore = 0;

    public DiceManager diceManager;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLifes = maxLifes;
        UpdateGameUi();
        StartRound();
    }

    // Update is called once per frame
    void UpdateGameUi()
    {
        UiController.instance.UpdateInGameInfo(round, currentLifes, currentScore, targetScore);
    }

    public void StartRound()
    {
        currentScore = 0;
        UiController.instance.HideAllPanels();
        UpdateGameUi();

        if(diceManager != null)
        {
            diceManager.ResetForNewRound();
        }

    }

    public void ProcessRoundResult(int finalScore)
    {
        currentScore = finalScore;
        if(currentScore > bestScore)
        {
            bestScore = currentScore;
        }

        UpdateGameUi();

        if(currentScore >= targetScore)
        {
            StartCoroutine(Winsequence());
        }
        else
        {
            StartCoroutine(LoseSequence());
        }
    }

    IEnumerator Winsequence()
    {
        yield return new WaitForSeconds(1.0f);
        UiController.instance.ShowResultPanel(true, targetScore, currentScore, currentLifes);
    }

    IEnumerator LoseSequence()
    {
        currentLifes--;
        UpdateGameUi();
        yield return new WaitForSeconds(1.0f);

        if (currentLifes > 0)
        {
            UiController.instance.ShowResultPanel(false, targetScore, currentScore, currentLifes);
        }
        else
        {
            Sprite[] lastDiceImages = diceManager.GetLastDiceSprites();
            UiController.instance.ShowGameOverpanel(round, bestScore, lastDiceImages);
        }
    }

    public void OnClickNextRound()
    {
        Debug.Log("다음 라운드로 이동~");
        // 라운드 이동 처리 필요
    }
}
