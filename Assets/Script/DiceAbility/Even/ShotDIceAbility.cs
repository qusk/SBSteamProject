using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/Shot")]
public class ShotDiceAbility : DiceData
{
    public int bonusScore = 3;

    public override void BeforeCalculateEffect(DiceState myState, List<DiceState> allDice)
    {
        for (int i = 0; i < allDice.Count; i++)
        {
            if (allDice[i] != null && allDice[i].originalValue % 2 == 0)
            {
                allDice[i].scoreValue += bonusScore;
            }
        }

    }
}
