using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot_v2 : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    // DECLARATION OF THE DRAG & DROP EVENTS
    // These events recieves an item slot as their input parameter
    public event Action<ItemSlot_v2> OnPointerEnterEvent;
    public event Action<ItemSlot_v2> OnPointerExitEvent;
    public event Action<ItemSlot_v2> OnRightClickEvent;
    public event Action<ItemSlot_v2> OnBeginDragEvent;
    public event Action<ItemSlot_v2> OnEndDragEvent;
    public event Action<ItemSlot_v2> OnDragEvent;
    public event Action<ItemSlot_v2> OnDropEvent;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(1, 1, 1, 0);

    [SerializeField] Image image;

    private sItem _item;
    public sItem Item
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
                image.color = disabledColor;
            }
            else
            {
                image.sprite = _item.itemSprite; // if the slot is full, use the sItem to update the slot image
                image.color = normalColor;
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
        // Since this is the inventory and it can carry every type of object, it will always return true
        return true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Tracks when the right mouse button is clicked 
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (OnRightClickEvent != null)
            {
                OnRightClickEvent(this); // Setting the item from this slot as the event input parameter
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnPointerEnterEvent != null)
        {
            OnPointerEnterEvent(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnPointerExitEvent != null)
        {
            OnPointerExitEvent(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OnBeginDragEvent != null)
        {
            OnBeginDragEvent(this);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (OnEndDragEvent != null)
        {
            OnEndDragEvent(this);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragEvent != null)
        {
            OnDragEvent(this);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropEvent != null)
        {
            OnDropEvent(this);
        }
    }
}
