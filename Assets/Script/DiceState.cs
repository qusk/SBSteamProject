
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class DiceState
{
    public DiceData diceData;

    public int diceIndex;      // 주사위 순서
    public int originalValue; // 최초 주사위 값
    public int modifiedValue;  // 효과 적용 후 주사위 값
    public int scoreValue;    // 점수 계산용 주사위 값
    public int changeValue;     
    public bool change;

    public ScoreManager.DiceType currentType;
    public bool isForceOdd = false;

    public bool IsCurrentEven => !isForceOdd && (modifiedValue % 2 == 0);
    public DiceState(DiceData data, int index, int value)
    {
        diceData = data;
        diceIndex = index;
        originalValue = value;
        modifiedValue = value;
        scoreValue = value;
        changeValue = 0;
        change = false;

        this.currentType = data != null ? data.type : ScoreManager.DiceType.None;
        this.isForceOdd = false;
    }

    
}
