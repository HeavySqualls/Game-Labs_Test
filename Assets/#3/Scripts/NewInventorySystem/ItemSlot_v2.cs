using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot_v2 : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Action <sItem> OnRightClickEvent; // This event recieves an sItem as its input parameter

    [SerializeField] Image image;
    [SerializeField] ItemTooltip tooltip;

    private sItem _item;
    public sItem Item // making the item a property 
    {
        get
        {
            return _item;
        }
        set
        {
            _item = value;

            if (_item == null) // if the slot is empty, hide the image component
            {
                image.enabled = false;
            }
            else
            {
                image.sprite = _item.itemSprite; // if the slot is full, use the sItem to update the slot image
                image.enabled = true;
            }
        }
    }

    protected virtual void OnValidate()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }

        if (tooltip == null)
        {
            tooltip = FindObjectOfType<ItemTooltip>(); // Since this only runs in the editor, it is ok for it to happen here as its of no s
        }
    }

    public virtual bool CanRecieveItem(sItem item)
    {
        return true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Tracks when the right mouse button is clicked 
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (Item != null && OnRightClickEvent != null)
            {
                OnRightClickEvent(Item); // Setting the item from this slot as the event input parameter
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Need to check if the item is of an equippable item type, if it isn't we will need to figure out 
        // a way to handle that case
        if (Item is sEquipment)
        {
            tooltip.ShowTooltip((sEquipment)Item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }
}
