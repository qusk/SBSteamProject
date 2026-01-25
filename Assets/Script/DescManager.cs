using TMPro;
using UnityEngine;

public class DescManager : MonoBehaviour
{
    public static DescManager instance;

    [Header("°ñµå")]
    public int Gold;
    public TextMeshProUGUI textMesh;
    
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
        textMesh.text = Gold.ToString();
    }

    public void SellGold(int gold)
    {
        Gold += gold;
        textMesh.text = Gold.ToString();
    }
}
