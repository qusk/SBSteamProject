using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DiceAbility", menuName = "Scriptable Objects/DiceAbility")]
public class DiceAbility : ScriptableObject
{
    public string abilityName;

    [Header("주사위 스킨")]
    public DiceSkin skin; // ★ 이 부분이 있어야 UI가 작동합니다!

    // myState: 내 주사위 상태, allDice: 모든 주사위 상태 리스트
    public virtual void OnRuleEffect(DiceState myState, List<DiceState> allDice) { }

    public virtual void OnRollEffect(DiceState myState, List<DiceState> allDice) { }

    public virtual void BeforeCalculateEffect(DiceState myState, List<DiceState> allDice) { }

    public virtual void AfterCalculateEffect(DiceState myState, List<DiceState> allDice, ref int score) { }
}