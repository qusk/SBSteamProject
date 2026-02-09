using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/cupid")]
public class CupidDiceAbility : DiceData
{
    public int bonusScore = 2;
    int[] bonus = new int[7];
    public override void BeforeCalculateEffect(DiceState myState, List<DiceState> allDice, List<ScoreEventData> events)
    {
        Array.Clear(bonus, 0, 7);

        foreach (var dice in allDice)
        {
            bonus[dice.modifiedValue]++;
        }

        foreach (var dice in allDice)
        {
            if(bonus[dice.modifiedValue] >= 2)
            {
                dice.scoreValue *= bonusScore;
                events.Add(new ScoreEventData(ScoreEventData.Type.AddScore, dice.diceIndex, 0, $"Cupid x{bonusScore}"));
            }
        }
    }

}
