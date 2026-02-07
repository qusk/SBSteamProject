using UnityEngine;

public class ScoreEventData
{
    public enum Type
    {
        Rule,          // 룰
        ChangeFace,    // 눈금 변경
        AddScore,      // 점수 추가
        GlobalBuffs,   // 전체 효과
        Multiplier,    // 점수 배율
        FinalScore     // 최종 점수
    }

    public Type type; 
    public int targetIndex; // 연출 대상
    public int value;          // 변동된 값
    public string desc;        // +3과 같은 연출용 텍스트
    
    public ScoreEventData(Type type, int index, int value, string desc)
    {
        this.type = type;
        targetIndex = index;
        this.value = value;
        this.desc = desc;
    }
}
