using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/chain")]
public class ChainDiceAbility : DiceData
{
    public int bonusScore = 6;
    public int count = 3;

    public override void OnRollEffect(DiceState myState, List<DiceState> allDice)
    {
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, allDice.Count);
            allDice[randomIndex].scoreValue = bonusScore;
        }
    }

}
