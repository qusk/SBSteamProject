using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Ability", menuName = "ItemAbility/Shield")]
public class Shield : ItemSo
{
    public override void Consumable()
    {
        GameManager.instance.heart += effectNum;
    }
}
