using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/turbo")]
public class TurboDiceAbility : DiceData
{
    public int bonusScore = 3;

    public override void BeforeCalculateEffect(DiceState myState, List<DiceState> allDice, List<ScoreEventData> events)
    {
        if(myState.IsCurrentEven)
        {
            foreach(var dice in allDice)
            {
                if(dice != null && dice.IsCurrentEven)
                {
                    dice.scoreValue *= bonusScore;
                    events.Add(new ScoreEventData(ScoreEventData.Type.Multiplier, dice.diceIndex, 0, "Turbo x3"));
                }
            }
        }
    }

}
