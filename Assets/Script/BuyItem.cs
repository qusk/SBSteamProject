using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    bool bought = false;
    ItemSo itemInfo;
    Image img;
    Image childImage;
    TextMeshProUGUI Desc;
    Transform canvas;
    Transform previousParent;
    RectTransform rect;
    CanvasGroup canvasGroup;
    public bool inPotiner = false;

    DiceAbility DiceInfo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        img = GetComponent<Image>();
        childImage = GetComponentsInChildren<Image>(true)[1];
        canvas = FindObjectOfType<Canvas>().transform;
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        Desc = childImage.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        Desc.text = DiceInfo.Desc;
    }

    public void UpdateInfo(ItemSo item)
    {
        itemInfo = item;
        img.sprite = item.itemIcon;
        Desc.text = itemInfo.itemDesc;
    }

    public void UpdateDiceInfo(DiceAbility ability)
    {
        DiceInfo = ability;
        img.sprite = ability.skin.GetSprite(1);
        Desc.text = ability.Desc;
    }

    public void OnPointerEnter()
    {
        inPotiner = true;
        DescManager.instance.DeSelectDesc();
        childImage.gameObject.SetActive(true);
    }

    public void OnPointerExit()
    {
        inPotiner = false;
        if (!DescManager.instance.descOn)
        {
            childImage.gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (inPotiner)
            {
                DescManager.instance.SelectDesc(childImage.gameObject);
            }

        }
        else if (eventData.button == PointerEventData.InputButton.Right && bought)
        {
            DescManager.instance.SellGold(DiceInfo.gold - 1);
            Player.instance.PullPlayerDices(DiceInfo);
            Destroy(gameObject);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        previousParent = transform.parent;

        transform.SetParent(canvas);
        transform.SetAsLastSibling();

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!bought)
        {
            if(transform.parent.CompareTag("BuySlot") || transform.parent == canvas ||
                Player.instance.player.gold - DiceInfo.gold < 0)
            {
                transform.SetParent(previousParent);
                rect.position = previousParent.GetComponent<RectTransform>().position;
            }
            else
            {
                bought = !bought;
                DescManager.instance.BuyGold(DiceInfo.gold);
                Player.instance.PushPlayerDices(DiceInfo);
            }
        }
        else
        {
            if (transform.parent == canvas || transform.parent.CompareTag("BuySlot"))
            {
                transform.SetParent(previousParent);
                rect.position = previousParent.GetComponent<RectTransform>().position;
            }
        }

        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }

}

