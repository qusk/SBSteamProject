using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/vulture")]
public class VultureDiceAbility : DiceData
{
    public int bonusScore = 2;

    public override void CalculateEffect(DiceState myState, List<DiceState> allDice, ref int totalScore, List<ScoreEventData> events)
    {
        bonusScore = bonusScore * multiBonusScore + plusBonusScore;
        if (!myState.IsCurrentEven)
        {
            int score = bonusScore;
            totalScore *= score;
            events.Add(new ScoreEventData(ScoreEventData.Type.Multiplier, myState.diceIndex, 0, $"Vulture! x{score}"));
        }
        bonusScore = 2;
    }
}
