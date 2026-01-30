using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class DiceManager : MonoBehaviour
{
    [Header("UI 연결")]
    public GameObject rollPanel;
    public CanvasGroup panelCanvasGroup;

    [Header("주사위 오브젝트")]
    public Dice[] panelDiceScript;
    public Image[] boardResultImage;

    [Header("주사위 능력")]
    public List<DiceAbility> diceAbilities;


    [Header("기본 설정")]
    public DiceAbility defaultDiceAbilit;
    public DiceSkin defaultDiceSkin;
    public float delayNextDice = 1.5f;

    private bool _isRolling = false;
    public bool isRolling => _isRolling;
    private int[] _resultStore = new int[6];

    void Start()
    {
        SetupDiceAbilities();
        if (rollPanel != null)
        {
            rollPanel.SetActive(false);
        }

        if (panelCanvasGroup != null)
        {
            panelCanvasGroup.alpha = 0;
        }

        foreach (var img in boardResultImage)
        {
            if(img != null)
            {
                img.gameObject.SetActive(false);
            }
        }

        foreach (var dice in panelDiceScript) 
        {
            if(dice != null)
            {
                dice.gameObject.SetActive(false);
            }
        }
    }

    public void SetupDiceAbilities()
    {
        for(int i = 0; i < panelDiceScript.Length; i++)
        {
            panelDiceScript[i].SetDefaultSkin(defaultDiceSkin);

            if(i < diceAbilities.Count && diceAbilities[i] != null) 
            {
                panelDiceScript[i].SetAbility(diceAbilities[i]);
            }
            else
            {
                panelDiceScript[i].SetAbility(defaultDiceAbilit);
            }
        }
    }

    public void StartRolling()
    {
        if(_isRolling)
        {
            return;
        }
        StartCoroutine(RollRoutine());
    }


    IEnumerator RollRoutine()
    {
        _isRolling = true;

        UiController.instance.SetRollBtnInteractable(false);

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

        // 점수 계산
        List<int> rollResults = new List<int>(_resultStore);
      
        int finalScore = ScoreManager.instance.CalculateScore(rollResults, diceAbilities);

        if(GameManager.instance != null)
        {
            GameManager.instance.ProcessRollResult(finalScore, diceAbilities, rollResults);
        }

        _isRolling = false;
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

    public void ResetForNewRound()
    {
        _isRolling = false;
    }
}
