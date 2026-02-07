using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/sky")]
public class SkyDiceAbility : DiceData
{
    public int bonusScore = 2;

    public override void BeforeCalculateEffect(DiceState myState, List<DiceState> allDice, List<ScoreEventData> events)
    {
        //for (int i = 0; i < allDice.Count; i++)
        //{
        //    if (allDice[i] != null && myState.isEven)
        //    {
        //        allDice[i].scoreValue *= bonusScore;
        //    }
        //}

        foreach (var dice in allDice)
        {
            if (dice != null && dice.IsCurrentEven)
            {
                dice.scoreValue *= bonusScore;
                events.Add(new ScoreEventData(ScoreEventData.Type.Multiplier, dice.diceIndex, 0, "Sky"));
            }
        }
    }
}