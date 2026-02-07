using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Ability", menuName = "ItemAbility/Rapier")]
public class Rapier : ItemSo
{
    public override void RoundStart()
    {
        GameManager.instance.currentRound += effectNum;
    }
}
