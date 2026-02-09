using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/bam")]
public class BamDiceAbility : DiceData
{
    int maxValue = 0;

    public override void OnRollEffect(DiceState myState, List<DiceState> allDice, List<ScoreEventData> events)
    {
        //´«±Ý º¯ÇÔ

        maxValue = 0;
        foreach (var dice in allDice)
        {
            if(dice.modifiedValue > maxValue) maxValue = dice.modifiedValue;
        }

        foreach(var dice in allDice)
        {
            dice.modifiedValue = maxValue;
            dice.scoreValue = maxValue;
            events.Add(new ScoreEventData(ScoreEventData.Type.ChangeFace, dice.diceIndex, maxValue, "Change Bam!"));
        }
    }

}
