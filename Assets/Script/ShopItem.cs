using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{

    int totalDice = 0;
    int totalItem = 0;
    BuyDice[] buyDice;
    BuyItem[] buyItem;

    [Header("데이터 베이스")]
    public ItemSo[] itemDB;
    public DiceData[] DiceDB;
    
    [Header("구매 아이템 슬롯")]
    public ItemSlot[] itemSlots;
    public int DiceSlotNum;
    public int itemSlotNum;

    [Header("프리팹")]
    public GameObject Dice;
    public GameObject Item;
    public GameObject myDicePanel;
    public GameObject Iventory;


    int randomIndex = -1;
    //List<int> usedIndex = new List<int>();

    void Start()
    {
        SetUp();
        Reroll();

        AudioManager.instance.PlayBgm(AudioManager.Bgm.Shop, true);
    }

    public void Reroll()
    {
        RerollDice();
        ReRollItem();
    }

    public void SetUp()
    {
        buyDice = new BuyDice[DiceSlotNum];
        buyItem = new BuyItem[itemSlotNum];

        for (int i = 0; i < DiceDB.Length; i++)
        {
            totalDice += DiceDB[i].weight;
        }

        for (int i = 0; i < itemDB.Length; i++)
        {
            totalItem += itemDB[i].weight;
        }

        BuyDice slotChildDice = null;
        

        for (int i = myDicePanel.transform.childCount - 1; i >= 0; i--)
        {
            slotChildDice = myDicePanel.transform.GetChild(myDicePanel.transform.childCount - 1 - i).GetComponentInChildren<BuyDice>();
            Debug.Log(slotChildDice);
            if (Player.instance.player.DiceSo[i] == null)
            {
                Player.instance.PushPlayerDices(Player.instance.defaultDice,i);
            }
            slotChildDice.UpdateDiceInfo(Player.instance.player.DiceSo[i], true);

        }

        Transform slotChildItem = null;
        GameObject item;

        for (int i = 0; i < Iventory.transform.childCount; i++)
        {
            if (Player.instance.player.itemSo[i] != null)
            {
                slotChildItem = Iventory.transform.GetChild(i);
                item = Instantiate(Item);
                item.GetComponent<BuyItem>().UpdateInfo(Player.instance.player.itemSo[i], true);
                item.transform.SetParent(slotChildItem.transform);
                item.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;
            }
        }
    }

    int RandomDice()
    {
        int weight = 0;
        int selectNum = 0; 
        selectNum = Mathf.RoundToInt(totalDice * Random.Range(0.0f,1.0f));

        for(int i = 0; i < DiceDB.Length; i++)
        {
            weight += DiceDB[i].weight;
            if(selectNum <= weight)
            {
                return i;
            }
        }
        return -1;
    }

    int RandomItem()
    {
        int weight = 0;
        int selectNum = 0;
        selectNum = Mathf.RoundToInt(totalItem * Random.Range(0.0f, 1.0f));

        for (int i = 0; i < itemDB.Length; i++)
        {
            weight += itemDB[i].weight;
            if (selectNum <= weight)
            {
                return i;
            }
        }
        return -1;
    }


    void RerollDice()
    {
        for (int i = 0; i < DiceSlotNum; i++)
        {
            if (itemSlots[i].transform.childCount > 0)
            {
                buyDice[i] = itemSlots[i].transform.GetComponentInChildren<BuyDice>();
                     
            }
            else
            {
                buyDice[i] = Instantiate(Dice).GetComponent<BuyDice>();
                buyDice[i].transform.SetParent(itemSlots[i].transform);
                buyDice[i].transform.GetComponent<RectTransform>().localPosition = Vector3.zero;
            }


            randomIndex = RandomDice();

            buyDice[i].UpdateDiceInfo(DiceDB[randomIndex], false);
            
            //if (usedIndex.Count < DiceDB.Length)
            //{
            //    if (!usedIndex.Contains(randomIndex))
            //    {
            //        buyDice[i].UpdateDiceInfo(DiceDB[randomIndex], false);
            //        usedIndex.Add(randomIndex);
            //    }
            //    else
            //    {
            //        i--;
            //    }
            //}
            //else if(usedIndex.Count >= DiceDB.Length)
            //{
            //    buyDice[i].UpdateDiceInfo(DiceDB[randomIndex], false);
            //}

        }
        //usedIndex.Clear();

    }

    void ReRollItem()
    {
        for(int i = 0; i < itemSlotNum; i++)
        {
            if (itemSlots[i + DiceSlotNum].transform.childCount > 0)
            {
                buyItem[i] = itemSlots[i + DiceSlotNum].transform.GetComponentInChildren<BuyItem>();

            }
            else
            {
                buyItem[i] = Instantiate(Item).GetComponent<BuyItem>();
                buyItem[i].transform.SetParent(itemSlots[i + DiceSlotNum].transform);
                buyItem[i].transform.GetComponent<RectTransform>().localPosition = Vector3.zero;
            }


            randomIndex = RandomItem();

            buyItem[i].UpdateInfo(itemDB[randomIndex], false);
        }

    }





    public void SelectDiceComb()
    {
        GameObject myDicePanelSlot;
        
        for (int i = 0; i < myDicePanel.transform.childCount; i++)
        {
            myDicePanelSlot = myDicePanel.transform.GetChild(i).gameObject;
            if (myDicePanelSlot.transform.childCount == 0)
            {
                Player.instance.PushPlayerDices(Player.instance.defaultDice);

            }
        }
    }

}
