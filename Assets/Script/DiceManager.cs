using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class DiceManager : MonoBehaviour
{
    [Header("UI 연결")]
    public GameObject rollPanel;
    public Button rollBtn;
    public TextMeshProUGUI rerollText;
    public CanvasGroup panelCanvasGroup;

    [Header("주사위 오브젝트")]
    public Dice[] panelDiceScript;
    public Image[] boardResultImage;

    [Header("설정")]
    public float delayNextDice = 1.5f;
    public int maxRerollCount = 1;

    private int _currentRerollCount;
    private bool _isRolling = false;
    private bool _isFirstRoll = true;
    private int[] _resultStore = new int[6];

    void Start()
    {
        _currentRerollCount = maxRerollCount;

        foreach (var img in boardResultImage)
        {
            if(img != null)
            {
                img.gameObject.SetActive(false);
            }
        }

        if(rollPanel != null)
        {
            rollPanel.SetActive(false);
            if(panelCanvasGroup != null)
            {
                panelCanvasGroup.alpha = 0;
            }
        }

        foreach (var dice in panelDiceScript) 
        {
            if(dice != null)
            {
                dice.gameObject.SetActive(false);
            }
        }

        UpdateGameUI();

        if(rollBtn != null)
        {
            rollBtn.onClick.AddListener(OnClickRoll);
        }
    }
    
    public void OnClickRoll()
    {
        if (_isRolling) return;

        if(!_isFirstRoll)
        {
            if (_currentRerollCount > 0)
            {
                _currentRerollCount--;
                UpdateGameUI();
            }
            else return;
        }

        StartCoroutine(RollRoutine());
    }

    IEnumerator RollRoutine()
    {
        _isRolling = true;
        rollBtn.interactable = false;

        if (rollPanel != null)
        {
            rollPanel.SetActive(true);

            foreach (var dice in panelDiceScript)
            {
                dice.gameObject.SetActive(false);
            }

            if (panelCanvasGroup != null)
            {
                panelCanvasGroup.alpha = 0;
                panelCanvasGroup.DOFade(1f, 0.5f);
            }
        }

        yield return new WaitForSeconds(0.5f);

        for(int i = 0; i < panelDiceScript.Length; i++)
        {
            int randomDiceResult = Random.Range(1, 7);
            _resultStore[i] = randomDiceResult;

            GameObject diceObj = panelDiceScript[i].gameObject;
            diceObj.SetActive(true);

            diceObj.transform.localScale = Vector3.zero;

            StartCoroutine(panelDiceScript[i].RollDice(randomDiceResult, 0.7f));

            yield return new WaitForSeconds(delayNextDice);
        }

        yield return new WaitForSeconds(1.5f);
        
        if(rollPanel != null) 
        {
            rollPanel.SetActive(false);
        }

        ApplyResultToBoard();

        // 임시 점수 계산
        int totalScore = 0;
        foreach(int val in _resultStore)
        {
            totalScore += val;
        }

        if(GameManager.instance != null)
        {
            GameManager.instance.ProcessRoundResult(totalScore);
        }

        _isRolling = false;
        _isFirstRoll = false;
        UpdateGameUI();
    }

    public Sprite[] GetLastDiceSprites()
    {
        Sprite[] lastDiceSprite = new Sprite[panelDiceScript.Length];
        for(int i = 0; i < panelDiceScript.Length; i++)
        {
            lastDiceSprite[i] = panelDiceScript[i].GetCurrentSprite();
        }

        return lastDiceSprite;
    }

    void ApplyResultToBoard()
    {
        for(int i = 0; i < panelDiceScript.Length; i++)
        {
            boardResultImage[i].gameObject.SetActive(true);
            boardResultImage[i].sprite = panelDiceScript[i].GetCurrentSprite();
        }
    }

    void UpdateGameUI()
    {
        if(rerollText != null)
        {
            rerollText.text = _isFirstRoll ? $"Reroll: {maxRerollCount}" : $"Reroll: {_currentRerollCount}";
        }

        if (!_isFirstRoll && _currentRerollCount < 0)
        {
            rollBtn.interactable = false;
        }
        else
        {
            rollBtn.interactable = true;
        }
    }

    public void ResetForNewRound()
    {
        if(rollBtn != null)
        {
            rollBtn.interactable = true;
        }

        _currentRerollCount = maxRerollCount;
        _isFirstRoll = true;
        UpdateGameUI();
    }
}
