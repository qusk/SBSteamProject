using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/cherry")]
public class CherryDiceAbility : DiceData
{
    public int bonusScore = 5;

    public override void BeforeCalculateEffect(DiceState myState, List<DiceState> allDice, List<ScoreEventData> events)
    {
        bonusScore = bonusScore * multiBonusScore + plusBonusScore;

        if (!myState.IsCurrentEven)
        {
            foreach(var dice in allDice)
            {
                if (dice != null && !dice.IsCurrentEven)
                {
                    dice.scoreValue += bonusScore;
                    events.Add(new ScoreEventData(ScoreEventData.Type.AddScore, dice.diceIndex, 0, "Cherry!"));
                }
            }
        }

        bonusScore = 5;
    }
}
