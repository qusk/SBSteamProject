using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{

    public int total = 0;
    
    [Header("주사위 데이터 베이스")]
    public ItemSo[] itemDB;
    
    [Header("구매 주사위")]
    public BuyItem[] buyDice;


    int randomIndex = -1;
    List<int> usedIndex = new List<int>();

    public int RandomItem()
    {
        int weight = 0;
        int selectNum = 0;

        selectNum = Mathf.RoundToInt(total * Random.Range(0.0f,1.0f));

        for(int i = 0; i < itemDB.Length; i++)
        {
            
            weight += itemDB[i].weight;
            Debug.Log(selectNum + ":" +weight);
            if(selectNum <= weight)
            {

                return i;
                    //return itemDB[i]; 
            }
        }
        return -1;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < itemDB.Length; i++)
        {
            total += itemDB[i].weight;
        }
    }

    public void reroll()
    {
        for (int i = 0; i < buyDice.Length; i++)
        {
            randomIndex = RandomItem();
            if (!usedIndex.Contains(randomIndex))
            {
                buyDice[i].UpdateInfo(itemDB[randomIndex]);
                usedIndex.Add(randomIndex);
            }
            else
            {
                i--;
            }
        }
        usedIndex.Clear();    
    }

    
}
