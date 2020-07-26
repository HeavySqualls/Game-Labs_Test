using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image itemImage;

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
                itemImage.sprite = _item.weaponSprite;
                itemImage.enabled = true;
            }
        }
    }

    private void OnValidate()
    {
        if (itemImage == null)
        {
            itemImage = GetComponent<Image>();
        }
    }
}
