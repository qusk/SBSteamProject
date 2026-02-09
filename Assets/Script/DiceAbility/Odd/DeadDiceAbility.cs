using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/dead")]
public class DeadDiceAbility : DiceData
{
    public int bonusScore = 0;

    public override void AfterCalculateEffect(DiceState myState, List<DiceState> allDice, ref int totalScore, List<ScoreEventData> events)
    {
        bonusScore = 0;
        foreach (var dice in allDice)
        {
            if (dice != null && !dice.IsCurrentEven)
            {
                bonusScore++;

                events.Add(new ScoreEventData(ScoreEventData.Type.AddScore, dice.diceIndex, 0, "Dead bonus"));
            }
        }
        totalScore *= (bonusScore * multiBonusScore + plusBonusScore);
        events.Add(new ScoreEventData(ScoreEventData.Type.GlobalBuffs, -1, 0, "Dead"));
    }
}
