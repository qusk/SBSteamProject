using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public (int totalScore, List<ScoreEventData> events) CalculateScore(Dice[] uiDice)
    {
        List<DiceState> simulationStates = new List<DiceState>();
        List<ScoreEventData> scoreEvents = new List<ScoreEventData>();

        for(int i = 0; i < uiDice.Length; i++)
        {
            if (uiDice[i] != null)
            {
                DiceData data = uiDice[i].MyState.diceData;
                int originalValue = uiDice[i].MyState.originalValue;
                simulationStates.Add(new DiceState(data, i, originalValue));
            }
        }

        // 점수 로직
        // 1. 룰상 효과
        foreach(var state in simulationStates)
        {
            if(state != null)
            {
                state.diceData.OnRuleEffect(state, simulationStates, scoreEvents);
            }
        }

        // 2. 굴림 효과
        foreach (var state in simulationStates)
        {
            if (state != null)
            {
                state.diceData.OnRollEffect(state, simulationStates, scoreEvents);
            }
        }

        // 3. 점수 계산 전 효과
        foreach (var state in simulationStates)
        {
            if (state != null)
            {
                state.diceData.BeforeCalculateEffect(state, simulationStates, scoreEvents);
            }
        }

        // 4. 점수 계산
        int totalScore = 0;
        foreach(var state in simulationStates)
        {
            totalScore += state.scoreValue;
            scoreEvents.Add(new ScoreEventData(
                ScoreEventData.Type.AddScore,
                state.diceIndex,
                totalScore,
                $"+{state.scoreValue}"
                ));
        }

        // 5. 점수 계산 후 효과
        foreach(var state in simulationStates)
        {
            if(state != null)
            {
                state.diceData.AfterCalculateEffect(state, simulationStates, ref totalScore, scoreEvents);
            }
        }

        scoreEvents.Add(new ScoreEventData(
            ScoreEventData.Type.FinalScore, 
            -1,
            totalScore,
            "Total"));

        return (totalScore, scoreEvents);
    }
}
