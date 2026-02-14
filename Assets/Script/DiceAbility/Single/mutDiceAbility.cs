using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/mut")]
public class MutDiceAbility : DiceData
{
    public override void OnRollEffect(DiceState myState, List<DiceState> allDice, List<ScoreEventData> scoreEvnet)
    {
        int total = 0;
        int weight = 0;
        int randNum = 0;
        foreach(var dice in GameManager.instance.dices.dices)
        {
            total += dice.weight;
        }
        randNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));
        foreach (var dice in GameManager.instance.dices.dices)
        {
            weight += dice.weight;
            if(weight >= randNum)
            {
                myState.diceData = dice;
                break;
            }
        }

    }
}
