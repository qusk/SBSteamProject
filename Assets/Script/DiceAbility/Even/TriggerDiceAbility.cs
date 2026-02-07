using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/trigger")]
public class TriggerDiceAbility : DiceData
{
    public int bonusScore = 2;

    public override void AfterCalculateEffect(DiceState myState, List<DiceState> allDice, ref int totalScore)
    {
        for (int i = 0; i < allDice.Count; i++)
        {
            if (allDice[i].originalValue % 2 == 1) return;

            if (i == allDice.Count - 1 && allDice[i].originalValue % 2 == 0)
            {
                totalScore *= bonusScore;
            }
        }

    }
}
