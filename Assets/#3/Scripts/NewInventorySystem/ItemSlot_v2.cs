using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot_v2 : MonoBehaviour, IPointerClickHandler
{
    public event Action <sItem> OnRightClickEvent; // This event recieves an sItem as its input parameter

    [SerializeField] Image image;

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
}
