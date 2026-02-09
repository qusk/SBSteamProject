using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/goblin")]
public class GoblinDiceAbility : DiceData
{
    

    public override void AfterCalculateEffect(DiceState myState, List<DiceState> allDice, ref int totalScore, List<ScoreEventData> events)
    {
        if(myState == null) return;
        GameManager.instance.gold += myState.modifiedValue;
        events.Add(new ScoreEventData(ScoreEventData.Type.AddScore, myState.diceIndex, 0, $"Gold +{myState.modifiedValue}"));
    }

}
