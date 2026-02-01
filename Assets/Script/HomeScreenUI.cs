using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreenUI : MonoBehaviour
{
    public static HomeScreenUI instance;
    public DiceAbility[] Dices;
    [Header("text")]
    public TextMeshProUGUI bestRoundtext;
    public TextMeshProUGUI bestScoretext;
    public TextMeshProUGUI synergyGoldText;
    public GameObject synergyButton;
    public GameObject synergy;

    public int synergyGold;
    
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

    void Start()
    {
        bestRoundtext.text = "최고 라운드: " + Player.instance.player.bestRound.ToString();
        bestScoretext.text = "최고 점수: " + Player.instance.player.bestScore.ToString();
    }

    //public void PushSynergyImage(DiceAbility Dice)
    //{
    //    for (int i = 0; i < synergy.transform.childCount; i++)
    //    {
    //        if (!synergy.transform.GetChild(i).gameObject.activeSelf)
    //        {
    //            synergyGold += Dice.gold;
    //            synergyGoldText.text = "필요 골드: " + synergyGold.ToString();
    //            Dices[i] = Dice;
    //            synergy.transform.GetChild(i).GetComponent<SynergyDIce>().Dice = Dice;
    //            synergy.transform.GetChild(i).GetComponent<Image>().sprite = Dice.itemIcon;
    //            synergy.transform.GetChild(i).gameObject.SetActive(true);
    //            return;
    //        }
    //    }
    //}

    //public void PopSynergyImage(ItemSo item)
    //{
    //    for (int i = 0; i < synergy.transform.childCount; i++)
    //    {
    //        if (Dices[i] == item)
    //        {
    //            synergyGold -= item.Gold;
    //            synergyGoldText.text = "필요 골드: " + synergyGold.ToString();
    //            Dices[i] = null;
    //            synergy.transform.GetChild(i).GetComponent<SynergyDIce>().Dice = item;
    //            synergy.transform.GetChild(i).GetComponent<Image>().sprite = item.itemIcon;
    //            synergy.transform.GetChild(i).gameObject.SetActive(false);
    //            return;
    //        }
    //    }
    //}

    //public void SynergyButtonImageUpdate()
    //{
    //    for(int i = 0; i < synergyButton.transform.childCount; i++)
    //    {
    //        Transform synergyChild = synergyButton.transform.GetChild(i);
    //        if(Dices[i] != null)
    //        {
    //            synergyChild.GetComponent<Image>().sprite = Dices[i].itemIcon;
    //            if (!synergyChild.gameObject.activeSelf)
    //                synergyChild.gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            synergyChild.gameObject.SetActive(false);
    //        }
    //    }
    //}

    public void ResetPlayerDices()
    {
        Player.instance.ResetDices(Dices);
    }
    


}
