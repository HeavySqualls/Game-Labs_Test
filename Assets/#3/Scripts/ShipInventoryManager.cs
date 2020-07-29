using UnityEngine;
using Kryz.CharacterStats;
using UnityEngine.UI;

public class ShipInventoryManager : MonoBehaviour
{
    public CharacterStat HP;
    public CharacterStat Shield;
    public CharacterStat ShieldRegen;
    public CharacterStat ReloadSpeed;

    public CharacterStat GunA_Damage;
    public CharacterStat GunA_Reload;

    public CharacterStat GunB_Damage;
    public CharacterStat GunB_Reload;

    public EquipmentPanel equipmentPanel;
    [SerializeField] Inventory inventory;
    [SerializeField] StatPanel statPanel;
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] Image draggableItem;

    private ItemSlot draggedSlot;

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

        // SET UP EVENTS:

        // Right Click Events
            // Modules
        inventory.OnModuleItemRightClickEvent += Equip;
        equipmentPanel.OnModuleItemRightClickEvent += UnEquip;
            // Weapons
        inventory.OnWeaponItemRightClickEvent += Equip;
        equipmentPanel.OnWeaponItemRightClickEvent += UnEquip;
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

    private void Equip (ItemSlot itemSlot)
    {
        sEquipment equippableItem = itemSlot.item as sEquipment;
        if (equippableItem != null)
        {
            Equip(equippableItem);
        }
    }

    private void UnEquip(ItemSlot itemSlot)
    {
        sEquipment equippableItem = itemSlot.item as sEquipment;
        if (equippableItem != null)
        {
            Unequip(equippableItem);
        }
    }

    private void ShowTooltip(ItemSlot itemSlot)
    {
        sEquipment equippableItem = itemSlot.item as sEquipment;
        if (equippableItem != null)
        {
            itemTooltip.ShowTooltip(equippableItem);
        }
    }

    private void BeginDrag(ItemSlot itemSlot)
    {
        if (itemSlot.item != null)
        {
            draggedSlot = itemSlot;
            draggableItem.sprite = itemSlot.item.itemSprite;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    private void EndDrag(ItemSlot itemSlot)
    {
        draggedSlot = null;
        draggableItem.enabled = false;
    }

    private void Drag(ItemSlot itemSlot)
    {
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
    }

    private void Drop(ItemSlot dropItemSlot)
    {
        if (dropItemSlot.CanRecieveItem(draggedSlot.item) && draggedSlot.CanRecieveItem(dropItemSlot.item))
        {
            sEquipment dragItem = draggedSlot.item as sEquipment;
            sEquipment dropItem = dropItemSlot.item as sEquipment;

            if (draggedSlot is EquipmentSlot)
            {
                Debug.Log("Equipping " + dropItem.name);
                if (dragItem != null) Unequip(dragItem);//dragItem.Unequip(this);
                if (dropItem != null) Equip(dropItem);//dropItem.Equip(this);
            }
            if (dropItemSlot is EquipmentSlot)
            {
                if (dragItem != null) Equip(dragItem);//dragItem.Equip(this);
                if (dropItem != null) Unequip(dragItem);// dropItem.Unequip(this);
            }

            statPanel.UpdateStatValues();

            sItem draggedItem = draggedSlot.item;
            draggedSlot.item = dropItemSlot.item;
            dropItemSlot.item = draggedItem;
        }
    }

    private void HideTooltip(ItemSlot itemSlot)
    {
        itemTooltip.HideTooltip();
    }

    public void Equip(sEquipment _item)
    {
        Debug.Log("Equipping " + _item.name);

        sEquipment previousItem;
        if (equipmentPanel.AddItem(_item, out previousItem))
        {
            if (previousItem != null)
            {
                //TODO: potentially just destroy the item here instead
                inventory.AddItem(previousItem);
                previousItem.Unequip(this);

                statPanel.UpdateStatValues();
            }

            _item.Equip(this);
            statPanel.UpdateStatValues();

            Debug.Log("Added " + _item.name);
        }
        else
        {
            inventory.AddItem(_item);
        }
    }

    public void Unequip(sEquipment _item)
    {
        equipmentPanel.RemoveItem(_item);
        _item.Unequip(this);
        statPanel.UpdateStatValues();
    }
}
