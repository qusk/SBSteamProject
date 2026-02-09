using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/mirror")]
public class MirrorDiceAbility : DiceData
{
    int index = 0;

    public override void BeforeCalculateEffect(DiceState myState, List<DiceState> allDice, List<ScoreEventData> events)
    {
        index = myState.diceIndex - 1;

    }

}
