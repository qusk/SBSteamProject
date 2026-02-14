using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "itemSo",menuName = "Scriptable Object/ItemData")]
public class ItemSo : ScriptableObject
{

    public Sprite itemIcon;
    public int itemNum;
    public string itemName;
    public int weight;
    public int gold;
    public int sell;
    public int effectNum;
    [TextArea]
    public string itemDesc;

    public virtual void Consumable() { } // 일회성tlsqkd

    public virtual void RoundStart(DiceState myState, List<DiceState> allDice, ref int totalSoce, List<ScoreEventData> events) { } // 지속 라운드 시작

    public virtual void RoundEnd() { } // 지속 라운드 끝


}
