using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/curb")]
public class CurbDiceAbility : DiceData
{
    int[] bonus = new int[7];
    public override void BeforeCalculateEffect(DiceState myState, List<DiceState> allDice, List<ScoreEventData> events)
    {
        Array.Clear(bonus, 0, 7);
        
        foreach (var dice in allDice)
        {          
            bonus[dice.modifiedValue]++;            
        }

        foreach(var dice in allDice)
        {
            int score = bonus[dice.modifiedValue] * dice.modifiedValue;
            dice.scoreValue += score;
            events.Add(new ScoreEventData(ScoreEventData.Type.AddScore, dice.diceIndex, 0, $"curb +{score}"));
        }
    }

}
