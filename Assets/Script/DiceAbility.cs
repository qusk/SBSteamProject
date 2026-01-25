using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DiceAblity", menuName = "Scriptable Objects/DiceAblity")]
public class DiceAbility : ScriptableObject
{
    public string abilityName;

    // 룰상 효과
    public virtual void OnRuleEffect(DiceState myValue, List<DiceState> allDice) { }
    
    // 주사위가 굴려졌을 때
    public virtual void OnRollDiceEffect(DiceState myValue, List<DiceState> allDice) { }

    // 점수 계산 시
    public virtual void OnBeforeCalculateScoreEffect(DiceState myValue, List<DiceState> allDice) { }
    // 점수 계산 후
    public virtual void OnAfterCalculateScoreEffect(DiceState myValue, List<DiceState> allDice, ref int finalScore) { }
}
