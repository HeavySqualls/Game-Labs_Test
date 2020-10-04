using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot_v2 : MonoBehaviour
{
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

    private void OnValidate()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }
    }
}
