using UnityEngine;

[CreateAssetMenu(fileName = "playSo",menuName = "Scriptable Object/playerData")]
public class PlayerSo : ScriptableObject
{
    public ItemSo[] itemSo1; 
    public int heart;
    public int gold;
    public int roundScore;
    public int currentRound;
    
}
