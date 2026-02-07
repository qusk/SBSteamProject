using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;


public class ScoreVisualizer : MonoBehaviour
{
    public static ScoreVisualizer instance;

    public TextMeshProUGUI finalScoreText;
    public GameObject floatingText;
    public Transform effectCanvas;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public IEnumerator PlayScoreEventSequence(Dice[] uiDice, List<ScoreEventData> scoreEvent)
    {
        foreach(var evt in scoreEvent)
        {
            Dice targetDice = null;
            if(evt.targetIndex >= 0 && evt.targetIndex < uiDice.Length)
            {
                targetDice = uiDice[evt.targetIndex];
            }

            switch(evt.type)
            {
                case ScoreEventData.Type.AddScore:
                    if(targetDice != null)
                    {
                        PlayDotweenEffect(targetDice, "Punch");
                        ShowFloatingText(targetDice.transform.position, evt.desc);
                        
                    }
                    UpdateScoreBoard(evt.value);
                    yield return new WaitForSeconds(1.0f);
                    break;
                case ScoreEventData.Type.Multiplier:
                    if (targetDice != null)
                    {
                        targetDice.transform.DOPunchScale(Vector3.one * 0.35f, 0.3f);
                        ShowFloatingText(targetDice.transform.position, evt.desc);
                    }
                    UpdateScoreBoard(evt.value);
                    yield return new WaitForSeconds(1.0f);
                    break;
                case ScoreEventData.Type.GlobalBuffs:
                    foreach(var dice in uiDice)
                    {
                        if (dice == null || !dice.gameObject.activeSelf) continue;

                        PlayDotweenEffect(dice, "Jump");
                        ShowFloatingText(dice.transform.position, evt.desc);
                    }
                    break;
                case ScoreEventData.Type.ChangeFace:
                    if(evt.targetIndex == -1) 
                    {
                        foreach(var dice in uiDice)
                        {
                            if(dice != null && dice.gameObject.activeSelf)
                            {
                                dice.transform.DOShakeRotation(0.3f, 90f);
                            }
                        }
                        yield return new WaitForSeconds(1.0f);

                        foreach(var dice in uiDice)
                        {
                            if(dice != null && dice.gameObject.activeSelf)
                            {
                                dice.UpdateDiceImage(evt.value);

                                ShowFloatingText(dice.transform.position, evt.desc);
                            }
                        }
                        yield return new WaitForSeconds(5.0f);
                    }
                    else if(targetDice != null)
                    {
                        targetDice.transform.DOShakePosition(0.3f, 90f);
                        yield return new WaitForSeconds(0.25f);

                        targetDice.UpdateDiceImage(evt.value);
                        ShowFloatingText(targetDice.transform.position, evt.desc);

                        yield return new WaitForSeconds(0.5f);
                    }
                    break;
                case ScoreEventData.Type.FinalScore:
                            finalScoreText.text = evt.value.ToString();
                            finalScoreText.transform.DOPunchScale(Vector3.one * 0.35f, 0.3f);
                            yield return new WaitForSeconds(0.35f);
                            break;
            }
        }
    }

    public void UpdateScoreBoard(int targetValue)
    {
        int originalValue = 0;
        int.TryParse(finalScoreText.text, out originalValue);

        DOVirtual.Int(originalValue, targetValue, 0.3f, (x) =>
        {
            finalScoreText.text = x.ToString();
        });
        finalScoreText.transform.DOShakePosition(0.3f, 2f);
    }

    public void PlayDotweenEffect(Dice dice, string type)
    {
        Transform t = dice.transform;

        t.DOKill(dice);
        t.localScale = Vector3.one;
        t.localRotation = Quaternion.identity;

        switch(type)
        {
            case "Punch":
                t.DOPunchScale(Vector3.one * 0.3f, 0.3f, 10, 1);
                break;
            case "Jump":
                t.DOLocalJump(t.localPosition, 30f, 1, 0.4f);
                t.DORotate(new Vector3(0, 0, 360), 0.4f, RotateMode.FastBeyond360);
                break;
            case "Shake":
                t.DOShakePosition(0.3f, 5f, 20, 90, false, true);
                break;
            case "Flash":
                Image img = dice.GetComponent<Image>();
                if(img != null)
                {
                    img.DOColor(Color.white, 0.1f).SetLoops(2, LoopType.Yoyo);
                }
                break;
        }
    }

    public void ShowFloatingText(Vector3 wordPos, string text)
    {
        if (floatingText == null) return;

        GameObject obj = Instantiate(floatingText, effectCanvas);
        obj.transform.position = wordPos + Vector3.up * 70f;

        TextMeshProUGUI tmp = obj.GetComponentInChildren<TextMeshProUGUI>();
        tmp.text = text;

        obj.transform.DOMoveY(obj.transform.position.y + 100f, 1.5f);
        tmp.DOFade(0, 1f).OnComplete(() => Destroy(obj));
    }
}
