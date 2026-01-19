using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class Dice : MonoBehaviour
{
    public Image diceImage;

    public Sprite[] diceSprite;

    [Header("사운드 및 이펙트")]
    public GameObject effectPreb;
    public AudioClip rollSound;

    public IEnumerator RollDice(int resultIndex, float duration)
    {
        // 사운드
        if (SoundManager.instance != null)
        {
            SoundManager.instance.PlaySfx(rollSound);
        }

        // 주사위 흔들기
        transform.DOScale(Vector3.one * 1.3f, 0.2f).SetEase(Ease.OutBack);
        transform.DOShakeRotation(duration, new Vector3(0, 0, 90), 20);
        // 이미지 교체
        float timer = 0f;
        float switchInterval = 0.05f;

        while (timer < duration)
        {
            diceImage.sprite = diceSprite[Random.Range(0, 6)];
            yield return new WaitForSeconds(switchInterval);
            timer += switchInterval;
        }
        // 결과 확정
        diceImage.sprite = diceSprite[resultIndex - 1];
        // 사운드 & 회전 복구
        transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBounce);
        transform.rotation = Quaternion.identity;
        // 이펙트

    }
    
    public Sprite GetCurrentSprite()
    {
        return diceImage.sprite;
    }
}
