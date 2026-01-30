using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("테스트용 설정")]
    public int currentRound = 1;
    public int targetScore = 20;
    public int maxLives = 3;
    public int currentLives ;

    public int maxRerollCount = 1;
    private int _currentRerollCount;
    private bool _isFirstRoll = true;

    public int currentScore = 0;
    public int bestScore = 0;

    public DiceManager diceManager;

    private List<DiceAbility> _lastDiceAbility;
    private List<int> _lastValue;

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
        currentLives = maxLives;
        UpdateGameUi();
        StartRound();
    }

    // Update is called once per frame
    void UpdateGameUi()
    {
        if(UiController.instance != null)
        {
            UiController.instance.UpdateInGameInfo(currentRound, currentLives, currentScore, targetScore);
        }
    }

    public void StartRound()
    {
        _isFirstRoll = true;
        _currentRerollCount = maxRerollCount;
        currentScore = 0;

        UiController.instance.HideAllPanels();
        UpdateGameUi();

        UiController.instance.UpdateRerollInfo(_currentRerollCount, _isFirstRoll);
        UiController.instance.SetRollBtnInteractable(true);

        if (diceManager != null)
        {
            diceManager.ResetForNewRound();
        }

    }

    public void OnClickRollBtn()
    {

        if (UiController.instance.rollBtn.interactable == false) return;

        UiController.instance.rollBtn.interactable = false;

        if (_isFirstRoll)
        {
            _isFirstRoll = false;
            diceManager.StartRolling();
            Debug.Log("첫번째 굴리기");
        }
        else if(!_isFirstRoll && _currentRerollCount > 0)
        {
            _currentRerollCount--;
            diceManager.StartRolling();
            Debug.Log("다시 굴리기");
        }

        UiController.instance.UpdateRerollInfo(_currentRerollCount, _isFirstRoll);
        if(!_isFirstRoll && _currentRerollCount <= 0)
        {
            UiController.instance.SetRollBtnInteractable(false);
        }
    }

    public void OnClickScoreConfirm()
    {
        if (UiController.instance.rollBtn.interactable == false && diceManager.isRolling) return;
        Debug.Log("점수 확정 버튼 클릭");
        CompleteRound();    
    }

    public void ProcessRollResult(int finalScore, List<DiceAbility> abilities, List<int> values)
    {
        currentScore = finalScore;
        _lastDiceAbility = abilities;
        _lastValue = values;

        // 최고 점수 갱신
        if(currentScore > bestScore)
        {
            bestScore = currentScore;
        }

        UpdateGameUi();

        if (_currentRerollCount <= 0)
        {
            Debug.Log("리롤 횟수 소진, 자동 점수 확정 결과 패널 등장");
            CompleteRound();
        }
        else
        {
            UiController.instance.SetRollBtnInteractable(true);
        }
    }

    public void CompleteRound()
    {
        UiController.instance.SetRollBtnInteractable(false);

        bool isSuccess = currentScore >= targetScore;

        if (isSuccess)
        {
            UiController.instance.ShowResultPanel(true, targetScore, currentScore, currentLives);
        }
        else
        {
            currentLives--;
            UpdateGameUi();
            if (currentLives > 0)
            {
                UiController.instance.ShowResultPanel(false, targetScore, currentScore, currentLives);
            }
            else
            {
                // 게임 오버시 모든 눈금을 1로 채워서 보여주기 위해 사용
                List<int> fakeValue = new List<int>();
                if(_lastValue != null)
                {
                    for(int i = 0; i < _lastValue.Count; i++)
                    {
                        fakeValue.Add(1);
                    }
                }
                UiController.instance.ShowGameOverPanel(currentRound, bestScore, _lastDiceAbility, fakeValue);
            }
        }
    }

    public void OnClickNextRound()
    {
        Debug.Log("다음 라운드로 이동~");
        // 라운드 이동 처리 필요
    }
}
    