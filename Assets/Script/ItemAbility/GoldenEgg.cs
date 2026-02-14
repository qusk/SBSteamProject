using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Ability", menuName = "ItemAbility/GoldenEgg")]
public class GoldenEgg : ItemSo
{
    public int bonusScore = 1;
    public override void RoundEnd()
    {
        GameManager.instance.gold += bonusScore;
    }
}
