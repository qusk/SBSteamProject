using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/rainbow")]
public class RainbowDiceAbility : DiceData
{
    public int bonusScore = 3;
    bool allSame = true;

    public override void AfterCalculateEffect(DiceState myState, List<DiceState> allDice, ref int totalScore, List<ScoreEventData> events)
    {
        allSame = true;
        for (int i = 0; i < allDice.Count; i++)
        {
            if(allDice[i].modifiedValue != allDice[0].modifiedValue)
            {
                allSame = false;
                break;
            }
        }
        if (allSame)
        {
            int score = totalScore * bonusScore;
            totalScore = score;
            events.Add(new ScoreEventData(ScoreEventData.Type.GlobalBuffs, -1, 0, "Rainbow"));
        }
    }

}