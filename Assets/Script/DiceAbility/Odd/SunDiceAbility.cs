using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/sun")]
public class SunDiceAbility : DiceData
{
    public int bonusScore = 2;
    List<DiceData> usedData = new List<DiceData>();

    public override void OnRuleEffect(DiceState myState, List<DiceState> allDice, List<ScoreEventData> events)
    {
        foreach (var dice in allDice)
        {
            if(dice != null && dice.currentType == ScoreManager.DiceType.Odd && !usedData.Contains(dice.diceData))
            {
                dice.diceData.multiBonusScore *= 2;
                usedData.Add(dice.diceData);
            }   
        }  
        events.Add(new ScoreEventData(ScoreEventData.Type.GlobalBuffs, -1, 0, $"Sun! bonusScore x{bonusScore}"));
    }
}
