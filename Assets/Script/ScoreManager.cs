using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static ScoreManager;

public class EffectWrapper
{
    private Delegate _effect;
    public DiceState _state;
    public DiceType _type;
    
    // ref 있는 생성자
    public EffectWrapper(EffectDelegate effect,DiceState state)
    {
        _effect = effect;
        _state = state;
        _type = state.currentType;
    }
    
    // ref 없는 생성자
    public EffectWrapper(EffectDelegate2 effect,DiceState state)
    {
        _effect = effect;
        _state = state;
        _type = state.currentType;
    }
    
    // 실행 (항상 ref int 전달)
    public void Execute(List<DiceState> allDice, ref int totalScore, List<ScoreEventData> events)
    {
        if (_effect is EffectDelegate withRef)
        {
            withRef(_state, allDice, ref totalScore, events);
        }
        else if (_effect is EffectDelegate2 withoutRef)
        {
            withoutRef(_state, allDice, events);
        }
    }

    public void Execute(List<DiceState> allDice,List<ScoreEventData> events)
    {
        if(_effect is EffectDelegate2 WithoutRef)
        {
            WithoutRef(_state, allDice, events);
        }
        else if(_effect is EffectDelegate WithRef)
        {
            int dummyScore = 0;
            WithRef(_state,allDice, ref dummyScore, events);
        }
        
    }
}



public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public enum DiceType { Even, Odd, Equal, Single, None, Roll }
    public int ignore = 0;

    public delegate void EffectDelegate(DiceState myState, List<DiceState> allDice, ref int totalScore, List<ScoreEventData> events);
    public delegate void EffectDelegate2(DiceState myState, List<DiceState> allDice, List<ScoreEventData> events);

    public List<EffectWrapper> effects = new List<EffectWrapper>();
    
    

    private void Awake()
    {
        if (instance == null) instance = this;
    }


    public (int totalScore, List<ScoreEventData> events) CalculateScore(Dice[] uiDice, DiceType filterType = DiceType.Roll)
    {
        List<DiceState> simulationStates = new List<DiceState>();
        List<ScoreEventData> scoreEvents = new List<ScoreEventData>();
        int totalScore = 0;

        for(int i = 0; i < uiDice.Length; i++)
        {
            if (uiDice[i] != null)
            {
                DiceData data = uiDice[i].MyState.diceData;
                if(filterType != DiceType.Roll && data.type != filterType)
                {
                    continue;
                }
                int originalValue = uiDice[i].MyState.originalValue;
                simulationStates.Add(new DiceState(data, i, originalValue));
            }
        }

        // 점수 로직
        // 1. 룰상 효과
        foreach (var state in simulationStates)
        {
            if (ignore > 0)
            {
                ignore--;
                continue;
            }
            if (state != null)
            {
                effects.Add(new EffectWrapper(state.diceData.OnRuleEffect, state));

                state.diceData.OnRuleEffect(state, simulationStates, scoreEvents);
            }
        }

        // 2. 굴림 효과
        foreach (var state in simulationStates)
        {
            if (ignore > 0)
            {
                ignore--;
                continue;
            }
            if (state != null)
            {
                effects.Add(new EffectWrapper(state.diceData.OnRollEffect, state));

                state.diceData.OnRollEffect(state, simulationStates, scoreEvents);
            }
        }

        // 3. 점수 계산 전 효과
        foreach (var state in simulationStates)
        {
            if (ignore > 0)
            {
                ignore--;
                continue;
            }
            if (state != null)
            {
                effects.Add(new EffectWrapper(state.diceData.BeforeCalculateEffect, state));

                state.diceData.BeforeCalculateEffect(state, simulationStates, scoreEvents);
            }
        }

        // 4. 점수 계산
        
        foreach (var state in simulationStates)
        {
            
            totalScore += state.scoreValue;
            scoreEvents.Add(new ScoreEventData(
                ScoreEventData.Type.AddScore,
                state.diceIndex,
                totalScore,
                $"+{state.scoreValue}"
                ));


            if (ignore > 0)
            {
                ignore--;
                continue;
            }
            if(state != null)
            {
                effects.Add(new EffectWrapper(state.diceData.CalculateEffect, state));

                state.diceData.CalculateEffect(state, simulationStates, ref totalScore, scoreEvents);
            }
        }

        // 5. 점수 계산 후 효과
        foreach (var state in simulationStates)
        {
            if (ignore > 0)
            {
                ignore--;
                continue;
            }
            if (state != null)
            {
                effects.Add(new EffectWrapper(state.diceData.AfterCalculateEffect, state));

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


