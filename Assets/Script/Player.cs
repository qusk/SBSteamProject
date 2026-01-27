using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public PlayerSo player;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

#if UNITY_EDITOR
        Array.Clear(player.itemSo1, 0, 5);
#endif

        DescManager.instance.UpdateInfo(player);
    }

    public void PushPlayerDice(ItemSo Dice)
    {
        for (int i = 0; i < player.itemSo1.Length; i++)
        {
            if(player.itemSo1[i] == null)
            {
                player.itemSo1[i] = Dice;
                return;
            }
                
        }
        
    }

    public void PullPlayerDice(ItemSo Dice)
    {
        for (int i = 0; i < player.itemSo1.Length; i++)
        {
            if (player.itemSo1[i] == Dice)
            {
                player.itemSo1[i] = null;
                return;
            }

        }
    }


}
