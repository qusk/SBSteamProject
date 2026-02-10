using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/mirror")]
public class MirrorDiceAbility : DiceData
{

    public override void BeforeCalculateEffect(DiceState myState, List<DiceState> allDice, List<ScoreEventData> events)
    {

        if (ScoreManager.instance.effects.Count > 0)
        {
            var lastEffect = ScoreManager.instance.effects[ScoreManager.instance.effects.Count - 2];
            lastEffect.Execute(allDice, events);
        }
    }

}
