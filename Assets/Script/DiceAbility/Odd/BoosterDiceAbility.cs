using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/booster")]
public class BoosterDiceAbility : DiceData
{
    public int bonusScore = 3;
    int count = 0;

    public override void AfterCalculateEffect(DiceState myState, List<DiceState> allDice, ref int totalScore, List<ScoreEventData> events)
    {
        bonusScore = bonusScore * multiBonusScore + plusBonusScore;

        count = 0;
        foreach (var dice in allDice)
        {
            if (dice != null && !dice.IsCurrentEven)
            {
                count++;
            }
        }

        if (count >= 3)
        {
            foreach (var dice in allDice)
            {
                dice.scoreValue += bonusScore;             
                events.Add(new ScoreEventData(ScoreEventData.Type.AddScore, dice.diceIndex, 0, $"booster! +{bonusScore}"));
            }
            if (Reroll)
            {
                GameManager.instance.CurrentRerollCount++;
                UiController.instance.UpdateRerollInfo(GameManager.instance.CurrentRerollCount, false);
                Reroll = false;
            }
        }
        int score = bonusScore * allDice.Count;
        totalScore += score;
    }
}
