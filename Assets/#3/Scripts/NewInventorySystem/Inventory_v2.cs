using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory_v2 : MonoBehaviour
{
    [SerializeField] List<sItem> startingItems;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot_v2[] itemSlots;

    public event Action<ItemSlot_v2> OnPointerEnterEvent;
    public event Action<ItemSlot_v2> OnPointerExitEvent;
    public event Action<ItemSlot_v2> OnRightClickEvent;
    public event Action<ItemSlot_v2> OnBeginDragEvent;
    public event Action<ItemSlot_v2> OnEndDragEvent;
    public event Action<ItemSlot_v2> OnDragEvent;
    public event Action<ItemSlot_v2> OnDropEvent;

    //public event Action<sItem> OnItemRightClickedEvent; // sets up an inventory event, similar to the slot event 

    private void Start()
    {
        SetStartingItems();

        for (int i = 0; i < itemSlots.Length; i++)
        {
            // Adds a listener to the slots' OnRightClickEvent to subscribe to the inventory's OnItemRightClickedEvent 
            itemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            itemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            itemSlots[i].OnRightClickEvent += OnRightClickEvent;
            itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            itemSlots[i].OnEndDragEvent += OnEndDragEvent;
            itemSlots[i].OnDragEvent += OnDragEvent;
            itemSlots[i].OnDropEvent += OnDropEvent;
        } 
    }

    private void OnValidate()
    {
        if (itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot_v2>();
        }

        SetStartingItems();
    }

    private void SetStartingItems() // calls every time a change happens to the inventory
    {
        int i = 0;
        for (; i < startingItems.Count && i < itemSlots.Length; i++) // for every item we have, we'll assign it to an item slot
        {
            itemSlots[i].Item = startingItems[i];
        }

        for (; i < itemSlots.Length; i++) // for every spot that does not have an item, set it to null
        {
            itemSlots[i].Item = null;
        }
    }

    public bool AddItem(sItem item) // public method for adding items to the inventory
    {
        // loop through all of the item slots
        for (int i = 0; i < itemSlots.Length; i++)
        {
            // check if there is an item slot that is empty
            if (itemSlots[i].Item == null)
            {
                // if so, add the item and return true
                itemSlots[i].Item = item;
                return true;
            }
        }
        // otherwise there are no available slots, return false
        return false;
    }

    public bool RemoveItem(sItem item)
    {
        // loop through all of the item slots
        for (int i = 0; i < itemSlots.Length; i++)
        {
            // check if there is an item slot that contains the item we are looking to remove
            if (itemSlots[i].Item == item)
            {
                // if so, remove the item and return true
                itemSlots[i].Item = null;
                return true;
            }
        }
        // otherwise there are no slots that have the item, return false
        return false;
    }

    public bool IsFull() // checks if the current item count is equal to, or greater than the number of possible item slots
    {
        // loop through all of the item slots
        for (int i = 0; i < itemSlots.Length; i++)
        {
            // check if there is an item slot that is empty
            if (itemSlots[i].Item == null)
            {
                // if so, the inventory is not full, and return false
                return false;
            }
        }
        // otherwise there are no available slots, the inventory is full, return true
        return true;
    }
}
