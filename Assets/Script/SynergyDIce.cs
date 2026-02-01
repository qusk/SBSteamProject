using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SynergyDIce : MonoBehaviour, IPointerClickHandler
{
    public ItemSo Dice;


    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if (gameObject.CompareTag("Scroll"))
            {
                //Player.instance.PushPlayerDice(Dice);
                //HomeScreenUI.instance.PushSynergyImage(Dice);
            }
            else if (gameObject.CompareTag("Synergy"))
            {
                //Player.instance.PullPlayerDice(Dice);
                //HomeScreenUI.instance.PopSynergyImage(Dice);
            }
        }
    }

}
