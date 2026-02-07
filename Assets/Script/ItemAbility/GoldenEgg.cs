using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Ability", menuName = "ItemAbility/GoldenEgg")]
public class GoldenEgg : ItemSo
{
    public override void RoundEnd()
    {
        GameManager.instance.gold += 1;
    }
}
