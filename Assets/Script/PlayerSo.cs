using UnityEngine;

[CreateAssetMenu(fileName = "playSo",menuName = "Scriptable Object/playerData")]
public class PlayerSo : ScriptableObject
{
    [Header("인 게임")]
    public ItemSo[] itemSo;
    public DiceData[] DiceSo;
    public int heart;
    public int gold;
    public int roundScore;
    public int currentRound;

    [Header("대기 화면")]
    public int bestRound;
    public int bestScore;
    
}
