using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/ruler")]
public class RulerDiceAbility : DiceData
{
    public bool usedEffect = false;

    public override void OnRuleEffect(DiceState myState, List<DiceState> allDice)
    {
        myState.diceData.type = ScoreManager.Type.None;
    }

    public override void AfterCalculateEffect(DiceState myState, List<DiceState> allDice, ref int totalScore)
    {
        if (usedEffect == true) return;
        totalScore += ScoreManager.instance.CalculateScore(GameManager.instance.diceManager.panelDiceScript, ScoreManager.Type.Even);
    }
}
