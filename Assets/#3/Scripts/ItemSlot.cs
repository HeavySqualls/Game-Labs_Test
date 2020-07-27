using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image itemImage;

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
}
