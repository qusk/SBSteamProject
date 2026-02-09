using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/Shot")]
public class ShotDiceAbility : DiceData
{
    public int bonusScore = 3;

    public override void BeforeCalculateEffect(DiceState myState, List<DiceState> allDice, List<ScoreEventData> events)
    {       
        foreach(var dice in allDice)
        {
            if(dice != null && dice.IsCurrentEven)
            {
                dice.scoreValue += bonusScore;
                events.Add(new ScoreEventData(ScoreEventData.Type.AddScore, dice.diceIndex, 0, "Shot"));
            }
        }
    }
}
