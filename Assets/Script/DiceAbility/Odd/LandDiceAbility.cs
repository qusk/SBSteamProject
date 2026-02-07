using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/land")]
public class LandDiceAbility : DiceData
{
    public int bonusScore = 2;

    public override void BeforeCalculateEffect(DiceState myState, List<DiceState> allDice)
    {
        for (int i = 0; i < allDice.Count; i++)
        {
            if (allDice[i] != null && !myState.isEven)
            {
                allDice[i].scoreValue *= bonusScore;
            }
        }

    }
}
