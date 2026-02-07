using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/Devil")]
public class DevilDiceAbility : DiceData
{
    public int bonusScore = 5;

    public override void BeforeCalculateEffect(DiceState myState, List<DiceState> allDice, List<ScoreEventData> events)
    {
        foreach(var dice in allDice)
        {
            if(dice.IsCurrentEven)
            {
                dice.scoreValue += bonusScore;
                events.Add(new ScoreEventData(ScoreEventData.Type.AddScore, dice.diceIndex, 0, "Devil + effect"));
            }
            else
            {
                int panelty = bonusScore - 3;
                dice.scoreValue -= panelty;
                events.Add(new ScoreEventData(ScoreEventData.Type.AddScore, dice.diceIndex, 0, "Devil - effect"));
            }
        }
    }
}