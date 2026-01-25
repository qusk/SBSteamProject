using UnityEngine;

public class DiceState
{
    //주사위 순서
    public int diceSequence;
    //주사위 최초 눈금
    public int originalValue;
    //주사위가 효과를 받을 시 변경되는 눈금
    public int modifiedValue;
    //주사위 점수 값
    public int scoreValue;

    public DiceState(int sequence, int value)
    {
        diceSequence = sequence;
        originalValue = value;
        originalValue = value;
        scoreValue = value;
    }
}
