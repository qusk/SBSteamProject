using TMPro;
using UnityEngine;

public class DescManager : MonoBehaviour
{
    public static DescManager instance;

    [Header("°ñµå")]
    public TextMeshProUGUI textGold;

    [Header("¶ó¿îµå")]
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
        Player.instance.player.gold -= gold;
        textGold.text = "°ñµå: " + Player.instance.player.gold.ToString();
    }

    public void SellGold(int gold)
    {
        Player.instance.player.gold += gold;
        textGold.text = "°ñµå: " + Player.instance.player.gold.ToString();
    }
}
