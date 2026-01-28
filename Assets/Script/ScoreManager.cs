using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public int CalculateScore(List<int> rollResult, List<DiceAbility> abilities)
    {
        List<DiceState> diceStates = new List<DiceState>();
        for (int i = 0; i < rollResult.Count; i++)
        {
            diceStates.Add(new DiceState(i, rollResult[i]));
        }

        if (abilities == null || abilities.Count == 0)
        {
            int basicScore = 0;
            foreach (var state in diceStates)
            {
                basicScore += state.scoreValue;
                
            }
            return basicScore;
        }


        // 점수 로직
        // 1. 룰상 효과
        for (int i = 0; i < diceStates.Count; i++)
        {
            if (i < abilities.Count && abilities[i] != null)
            {
                abilities[i].OnRuleEffect(diceStates[i], diceStates);
            }
        }

        // 2. 굴림 효과
        for (int i = 0; i < diceStates.Count; i++)
        {
            if (i < abilities.Count && abilities[i] != null)
            {
                abilities[i].OnRollEffect(diceStates[i], diceStates);
            }
        }

        // 3. 점수 계산 전 효과
        for (int i = 0; i < diceStates.Count; i++)
        {
            if(i < abilities.Count && abilities[i] != null)
            {
                abilities[i].BeforeCalculateEffect(diceStates[i], diceStates);
            }
        }

        // 4. 점수 계산
        int totalScore = 0;
        foreach(var state in diceStates)
        {
            totalScore += state.scoreValue;
        }

        // 5. 점수 계산 후 효과
        for(int i = 0; i < diceStates.Count; i++)
        {
            if(i < abilities.Count && abilities[i] != null)
            {
                abilities[i].AfterCalculateEffect(diceStates[i], diceStates, ref totalScore);
            }
        }

        return totalScore;
    }
}
