using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/Devil")]
public class DevilDiceAbility : DiceData
{
    public int bonusScore = 5;

    public override void BeforeCalculateEffect(DiceState myState, List<DiceState> allDice)
    {
        for (int i = 0; i < allDice.Count; i++)
        {
            if (allDice[i] == null) return;
                
            if (allDice[i].originalValue % 2 == 0)
            {
                allDice[i].scoreValue += bonusScore;
            }
            else
            {
                allDice[i].scoreValue -= (bonusScore - 2);
            }
        }

    }
}