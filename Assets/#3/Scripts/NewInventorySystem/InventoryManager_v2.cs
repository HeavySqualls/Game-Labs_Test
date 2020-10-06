using UnityEngine;
using UnityEngine.UI;
using Kryz.CharacterStats;

public class InventoryManager_v2 : MonoBehaviour
{
    public CharacterStat HP;
    public CharacterStat Shield;
    public CharacterStat ShieldRegen;
    public CharacterStat ReloadSpeed;

    public CharacterStat GunA_Damage;
    public CharacterStat GunA_Reload;

    public CharacterStat GunB_Damage;
    public CharacterStat GunB_Reload;

    [SerializeField] Inventory_v2 inventory;
    public EquipmentPanel_v2 equipmentPanel;
    [SerializeField] StatPanel statPanel;
    [SerializeField] ItemTooltip itemTooltip;

    [SerializeField] Image draggableItem;
    private ItemSlot_v2 draggedSlot;

    private void OnValidate()
    {
        if (itemTooltip == null)
        {
            itemTooltip = FindObjectOfType<ItemTooltip>();
        }
    }

    private void Awake()
    {
        statPanel.SetStats(HP, Shield, ShieldRegen, GunA_Damage, GunA_Reload, GunB_Damage, GunB_Reload);
        statPanel.UpdateStatValues();

        // SET UP THE EVENTS

        // Right Click Events
        inventory.OnRightClickEvent += Equip;
        equipmentPanel.OnRightClickEvent += UnEquip;
        // Pointer Enter
        inventory.OnPointerEnterEvent += ShowTooltip;
        equipmentPanel.OnPointerEnterEvent += ShowTooltip;
        // Pointer Exit
        inventory.OnPointerExitEvent += HideTooltip;
        equipmentPanel.OnPointerExitEvent += HideTooltip;
        // Begin Drag
        inventory.OnBeginDragEvent += BeginDrag;
        equipmentPanel.OnBeginDragEvent += BeginDrag;
        //End Drag
        inventory.OnEndDragEvent += EndDrag;
        equipmentPanel.OnEndDragEvent += EndDrag;
        // Drag
        inventory.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;
        // Drop
        inventory.OnDropEvent += Drop;
        equipmentPanel.OnDropEvent += Drop;
    }

    // ------------------------------------------------------------------------- EQUIP / UNEQUIP ----- //
    private void Equip(ItemSlot_v2 itemSlot)
    {
        // check to make sure the item in the item slot is indeed an equipment item before equipping it 
        sEquipment equippableItem = itemSlot.Item as sEquipment;
        if (equippableItem != null)
        {
            Equip(equippableItem);
        }
    }

    private void UnEquip(ItemSlot_v2 itemSlot)
    {
        sEquipment equippableItem = itemSlot.Item as sEquipment;
        if (equippableItem != null)
        {
            Unequip(equippableItem);
        }
    }

    // ------------------------------------------------------------------------- TOOLTIPS ----- //
    private void ShowTooltip(ItemSlot_v2 itemSlot)
    {
        sEquipment equippableItem = itemSlot.Item as sEquipment;
        if (equippableItem != null)
        {
            itemTooltip.ShowTooltip(equippableItem);
        }
    }

    private void HideTooltip(ItemSlot_v2 itemSlot)
    {
        itemTooltip.HideTooltip();
    }

    // ------------------------------------------------------------------------- DRAG AND DROP ----- //
    private void BeginDrag(ItemSlot_v2 itemSlot)
    {
        if (itemSlot.Item != null)
        {
            // set the dragged slot (the slot where the item originally came from) 
            draggedSlot = itemSlot;
            // set the draggable items image sprite to the same as the sprite from 
            // the item we are dragging & enable the game object
            draggableItem.sprite = itemSlot.Item.itemSprite;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    private void EndDrag(ItemSlot_v2 itemSlot)
    {
        draggedSlot = null;
        draggableItem.enabled = false;
    }

    private void Drag(ItemSlot_v2 itemSlot)
    {
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
    }

    private void Drop(ItemSlot_v2 dropItemSlot)
    {
        // before attempting to drop the item, make sure that the slot from the item where we are dropping the
        // item can recieve the slot that started the drag, and check that the slot that started the drag can recieve 
        // the item at where we are dropping it (make sure they can make an exchange)
        if (draggedSlot == null) return;

        if (dropItemSlot.CanRecieveItem(draggedSlot.Item) && draggedSlot.CanRecieveItem(dropItemSlot.Item))
        {
            sEquipment dragItem = draggedSlot.Item as sEquipment;
            sEquipment dropItem = dropItemSlot.Item as sEquipment;

            if (draggedSlot is EquipmentSlot_v2)
            {
                Debug.Log("Equipping " + dropItem.name);
                if (dragItem != null) Unequip(dragItem);
                if (dropItem != null) Equip(dropItem);
            }
            if (dropItemSlot is EquipmentSlot_v2)
            {
                if (dragItem != null) Equip(dragItem);
                if (dropItem != null) Unequip(dragItem);
            }

            statPanel.UpdateStatValues();

            sItem draggedItem = draggedSlot.Item;
            draggedSlot.Item = dropItemSlot.Item;
            dropItemSlot.Item = draggedItem;
        }
    }

    public void Equip(sEquipment item)
    {
        Debug.Log("Equipping " + item.name);

        if (inventory.RemoveItem(item))
        {
            sEquipment previousItem;

            // if there was an item already inside the desired equipment slot...
            if (equipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    // ...add that item back to the inventory
                    inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();
            }
            // if we could not equip the current item, return it to the inventory as well 
            else
            {
                inventory.AddItem(item);
            }
        }
    }

    public void Unequip(sEquipment item)
    {
        // make sure the inventory is not full, then remove the item from the equipment panel...
        if (!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            // ...and add it to the inventory
            inventory.AddItem(item);
            item.Unequip(this);
            statPanel.UpdateStatValues();
        }
    }
}
