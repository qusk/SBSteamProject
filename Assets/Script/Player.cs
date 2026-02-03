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
        if (DescManager.instance != null)
            DescManager.instance.UpdateInfo(player);
    }

    public void PushPlayerDices(DiceData Dice)
    {
        for (int i = 0; i < player.DiceSo.Length; i++)
        {
            if (player.DiceSo[i] == null)
            {
                player.DiceSo[i] = Dice;
                return;
            }

        }

    }

    public void PullPlayerDices(DiceData Dice)
    {
        for (int i = 0; i < player.DiceSo.Length; i++)
        {
            if (player.DiceSo[i] == Dice)
            {
                player.DiceSo[i] = null;
                return;
            }

        }
    }

    public void ResetDices(DiceData[] Dices)
    {
        for(int i = 0;i < Dices.Length; i++)
        {
            player.DiceSo[i] = Dices[i];
        }
    }

}
