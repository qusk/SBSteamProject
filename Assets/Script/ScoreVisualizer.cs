using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;


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
                        targetDice.transform.DOPunchScale(Vector3.one * 0.3f, 0.2f);
                        ShowScoreText(targetDice.transform.position, evt.desc);
                        
                    }
                    UpdateScoreBoard(evt.value);
                    yield return new WaitForSeconds(1.0f);
                    break;
                case ScoreEventData.Type.Multiplier:
                    if (targetDice != null)
                    {
                        targetDice.transform.DOPunchScale(Vector3.one * 0.3f, 0.2f);
                        ShowScoreText(targetDice.transform.position, evt.desc);
                    }
                    UpdateScoreBoard(evt.value);
                    yield return new WaitForSeconds(1.0f);
                    break;
                case ScoreEventData.Type.FinalScore:
                    finalScoreText.text = evt.value.ToString();
                    finalScoreText.transform.DOPunchScale(Vector3.one * 0.3f, 0.2f);
                    yield return new WaitForSeconds(1.0f);
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
        finalScoreText.transform.DOShakePosition(0.3f, 5f);
    }

    public void ShowScoreText(Vector3 wordPos, string text)
    {
        if (floatingText == null) return;

        GameObject obj = Instantiate(floatingText, effectCanvas);
        obj.transform.position = wordPos + Vector3.up * 30f;

        TextMeshProUGUI tmp = obj.GetComponentInChildren<TextMeshProUGUI>();
        tmp.text = text;

        obj.transform.DOMoveY(obj.transform.position.y + 100f, 1f);
        tmp.DOFade(0, 1f).OnComplete(() => Destroy(obj));
    }
}
