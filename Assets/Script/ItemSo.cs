using UnityEngine;

[CreateAssetMenu(fileName = "itemSo",menuName = "Scriptable Object/ItemData")]
public class ItemSo : ScriptableObject
{
   // public enum ItemRank { S,A,B,C}

    //public ItemRank rank;
    public Sprite itemIcon;
    public string itemName;
    public int weight;
    public int Gold;
    [TextArea]
    public string itemDesc;
 
}
