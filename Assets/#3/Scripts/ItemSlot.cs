using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image itemImage;
    [SerializeField] ItemTooltip tooltip;

    public event Action<sItem> OnRightClickEvent;

    private sItem _item;
    public sItem item
    {
        get { return _item; }
        set
        {
            _item = value;

            if (_item == null)
            {
                itemImage.enabled = false;
            }
            else
            {
                itemImage.sprite = _item.itemSprite;
                itemImage.enabled = true;
            }
        }
    }

    protected virtual void OnValidate()
    {
        if (itemImage == null)
        {
            itemImage = GetComponent<Image>();
        }

        if (tooltip == null)
        {
            tooltip = FindObjectOfType<ItemTooltip>();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null && OnRightClickEvent != null)
            {
                OnRightClickEvent(item);
                Debug.Log("Right Click!");
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item is sEquipment)
        {
            tooltip.ShowTooltip((sEquipment)item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }
}
