using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/bam")]
public class BamDiceAbility : DiceData
{
    int maxValue = 0;

    public override void OnRollEffect(DiceState myState, List<DiceState> allDice, List<ScoreEventData> events)
    {
        //´«±Ý º¯ÇÔ

        
        foreach (var dice in allDice)
        {
            if(dice.modifiedValue > maxValue) maxValue = dice.modifiedValue;
        }

        foreach(var dice in allDice)
        {
            dice.modifiedValue = maxValue;
            dice.scoreValue = maxValue;
            dice.change = true;
            events.Add(new ScoreEventData(ScoreEventData.Type.ChangeFace, dice.diceIndex, maxValue, "Change Bam!"));
        }
        
        maxValue = 0;

        ChangeModi(myState, allDice, events);
        
        
    }



}
