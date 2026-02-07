using UnityEngine;

[CreateAssetMenu(fileName = "itemSo",menuName = "Scriptable Object/ItemData")]
public class ItemSo : ScriptableObject
{

    public Sprite itemIcon;
    public string itemName;
    public int weight;
    public int gold;
    public int effectNum;
    [TextArea]
    public string itemDesc;

    public virtual void Consumable() { } // 일회성


    public virtual void RoundStart() { } // 지속 라운드 시작

    public virtual void RoundEnd() { } // 지속 라운드 끝


}
