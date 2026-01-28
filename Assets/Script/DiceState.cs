
using JetBrains.Annotations;
using UnityEngine;

public class DiceState
{

    public int sequence;      // 주사위 순서
    public int originalValue; // 최초 주사위 값
    public int modifiedValue; // 효과 적용 후 주사위 값
    public int scoreValue;    // 점수 계산용 주사위 값

    public DiceState(int sequence, int value)
    {
        this.sequence = sequence;
        originalValue = value;
        modifiedValue = value;
        scoreValue = value;
    }
}
