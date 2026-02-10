using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Ability", menuName = "DiceAbility/core")]
public class CoreDiceAbility : DiceData
{
    public int bonusScore = 3;
    List<EffectWrapper> effect = new List<EffectWrapper>();

    public override void AfterCalculateEffect(DiceState myState, List<DiceState> allDice, ref int totalScore, List<ScoreEventData> events)
    {
        effect.Clear();
        for (int i = 0; i < ScoreManager.instance.effects.Count; i++)
        {
            if(ScoreManager.instance.effects[i]._type == ScoreManager.DiceType.Even && 
                ScoreManager.instance.effects[i]._state != myState && ScoreManager.instance.effects.Count > 0)
            {
                var lastEffect = ScoreManager.instance.effects[i];
                effect.Add(lastEffect);
            }
        }

        foreach(var ef in effect)
        {
            Debug.Log(ef._state.modifiedValue);
            ef.Execute(allDice,ref totalScore,events);
        }
    }
}
