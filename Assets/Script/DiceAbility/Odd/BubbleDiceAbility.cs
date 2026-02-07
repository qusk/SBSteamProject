using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/bubble")]
public class BubbleDiceAbility : DiceData
{
    public int bonusScore = 3;

    public override void OnRollEffect(DiceState myState, List<DiceState> allDice)
    {
        if (myState != null && myState.isEven == false)
        {
            myState.isEven = true;
            myState.scoreValue += bonusScore;
        }
        
    }

}
