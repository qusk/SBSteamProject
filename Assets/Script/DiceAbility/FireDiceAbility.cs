using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/fire")]
public class FireDiceAbility : DiceData
{
    public int bonusScore = 3;

    public override void BeforeCalculateEffect(DiceState myState, List<DiceState> allDice, List<ScoreEventData> scoreEvent)
    {
        myState.scoreValue *= bonusScore;

    }
}
