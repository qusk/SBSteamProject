using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{

    public int total = 0;
    
    [Header("주사위 데이터 베이스")]
    public ItemSo[] itemDB;
    
    [Header("구매 주사위")]
    public BuyItem[] buyDice;
    public ItemSlot[] itemSlots;
    public GameObject Dice;



    int randomIndex = -1;
    public List<int> usedIndex = new List<int>();

    void Start()
    {
        for(int i = 0; i < itemDB.Length; i++)
        {
            total += itemDB[i].weight;
        }
    }

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
            }
        }
        return -1;
    }

    

    public void reroll()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].transform.childCount > 0)
            {
                buyDice[i] = itemSlots[i].transform.GetComponentInChildren<BuyItem>();
            }
            else
            {
                buyDice[i] = Instantiate(Dice).GetComponent<BuyItem>();
                buyDice[i].transform.SetParent(itemSlots[i].transform);
                buyDice[i].transform.GetComponent<RectTransform>().localPosition = Vector3.zero;
            }


            randomIndex = RandomItem();
            if(usedIndex.Count < itemDB.Length)
            {
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
            else if(usedIndex.Count >= itemDB.Length)
            {
                buyDice[i].UpdateInfo(itemDB[randomIndex]);
            }

            

        }
        usedIndex.Clear();

    }
  
}
