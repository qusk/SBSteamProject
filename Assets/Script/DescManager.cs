using TMPro;
using UnityEngine;

public class DescManager : MonoBehaviour
{
    public static DescManager instance;

    [Header("°ñµå")]
    public int Gold;
    public TextMeshProUGUI textGold;

    [Header("¶ó¿îµå")]
    public TextMeshProUGUI roundScore;
    public TextMeshProUGUI currentRound;
    
    GameObject selectDesc;
    public bool descOn = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateInfo(PlayerSo player)
    {
        roundScore.text = "ÇöÀç ¶ó¿îµå:" + player.roundScore.ToString();
        currentRound.text = "¶ó¿îµå Á¡¼ö: " + player.currentRound.ToString();
        textGold.text = "°ñµå: " + player.gold.ToString();
    }

    public void SelectDesc(GameObject selectDesc)
    {
        this.selectDesc = selectDesc;
        descOn = true;
    }

    public void DeSelectDesc()
    {
        if (selectDesc != null)
        {
            descOn = false;
            this.selectDesc.SetActive(false);
        }

    }

    public void BuyGold(int gold)
    {
        Gold -= gold;
        textGold.text = "°ñµå: " + Gold.ToString();
    }

    public void SellGold(int gold)
    {
        Gold += gold;
        textGold.text = "°ñµå: " + Gold.ToString();
    }
}
