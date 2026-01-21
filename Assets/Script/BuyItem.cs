using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    
    ItemSo itemInfo;
    Image img;
    Image childImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        img = GetComponent<Image>();
        childImage = GetComponentsInChildren<Image>(true)[1];
    }

    public void UpdateInfo(ItemSo item)
    {
        itemInfo = item;
        img.sprite = item.itemIcon;
    }

    public void OnClickDesc()
    {
        if (childImage.gameObject.activeSelf)
        {
            childImage.gameObject.SetActive(false);
        }
        else
        {
            childImage.gameObject.SetActive(true);
        }
    }

}
