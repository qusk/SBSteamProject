using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class Dice : MonoBehaviour
{
    public Image diceImage;

    [Header("사운드 및 이펙트")]
    public GameObject effectPrefab;
    public AudioClip rollSound;

    private DiceAbility _myAbility;
    private DiceSkin _defaultSkin;

    public void SetAbility(DiceAbility ability)
    {
        this._myAbility = ability;
        // 다이스 기본 세팅
        UpdateDiceImage(1);
    }

    public void SetDefaultSkin(DiceSkin skin)
    {
        _defaultSkin = skin;
    }

    public void UpdateDiceImage(int value)
    {
        if(_myAbility != null && _myAbility.skin != null)
        {
            diceImage.sprite = _myAbility.skin.GetSprite(value);
        }
        else if(_defaultSkin != null)
        {
            diceImage.sprite = _defaultSkin.GetSprite(value);
        }
    }

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
            if(_myAbility != null && _myAbility.skin != null)
            {
                diceImage.sprite = _myAbility.skin.GetSprite(Random.Range(1, 7));
            }
            else if(_defaultSkin != null)
            {
                diceImage.sprite = _defaultSkin.GetSprite(Random.Range(1, 7));
            }

            yield return new WaitForSeconds(switchInterval);
            timer += switchInterval;
        }

        // 결과 확정
        UpdateDiceImage(resultIndex);
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
