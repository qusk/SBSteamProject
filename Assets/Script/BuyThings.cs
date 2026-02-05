using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyThings : MonoBehaviour, IBeginDragHandler, IDragHandler
{

    protected Image img;
    protected bool bought = false;
    protected Image childImage;
    protected TextMeshProUGUI Desc;
    protected Transform canvas;
    [SerializeField]protected Transform previousParent;
    protected RectTransform rect;
    protected CanvasGroup canvasGroup;
    public bool inPotiner = false;
    protected bool isDragged = false;

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

    public virtual void OnPointerEnter()
    {
        inPotiner = true;
        DescManager.instance.DeSelectDesc();
        childImage.gameObject.SetActive(true);
    }

    public virtual void OnPointerExit()
    {
        inPotiner = false;
        if (!DescManager.instance.descOn)
        {
            childImage.gameObject.SetActive(false);
        }
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        //if (isDragged == true) return;

        isDragged = true;
        previousParent = transform.parent;
        Debug.Log(previousParent);

        transform.SetParent(canvas);
        transform.SetAsLastSibling();

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
    }
}
