using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int CalculateScore(List<int> diceValues, List<DiceAbility> abilities)
    {
        // 주사위 상태 생성
        List<DiceState> diceStates = new List<DiceState>();

        for(int i = 0; i < diceValues.Count; i++)
        {
            diceStates.Add(new DiceState(i, diceValues[i]));
        }
        // 능력 없으면 기본 눈금 점수 계산 
        if(abilities == null || abilities.Count == 0)
        {
            int baseScore = 0;
            foreach(var state in diceStates)
            {
                baseScore += state.scoreValue;
                return baseScore;
            }
        }

        // 점수 계산 파이프 라인
        // Step 1. 룰상 효과
        for(int i = 0; i < diceStates.Count; i++)
        {
            if(i < abilities.Count && abilities[i] != null)
            {
                abilities[i].OnRuleEffect(diceStates[i], diceStates);
            }
        }
        // Step 2. 주사위 굴러갔을 시 효과
        for(int i = 0; i < diceStates.Count; i++)
        {
            if (i < abilities.Count && abilities[i] != null) { }
            {
                abilities[i].OnRollDiceEffect(diceStates[i], diceStates);
            }
        }
        // Step 3. 점수 계산 시 효과
        for(int i = 0; i < diceStates.Count; i++)
        {
            if(i < abilities.Count && abilities[i] != null)
            {
                abilities[i].OnBeforeCalculateScoreEffect(diceStates[i], diceStates);
            }
        }
        // Step 4. 점수 계산
        int currentScore = 0;
        foreach(var state in diceStates)
        {
            currentScore += state.scoreValue;
        }
        // Step 5. 점수 계산 후 효과
        for(int i = 0; i < diceStates.Count; i++)
        {
            if(i < abilities.Count && abilities[i] != null)
            {
                abilities[i].OnAfterCalculateScoreEffect(diceStates[i], diceStates, ref currentScore);
            }
        }

        return currentScore;
    }
}
