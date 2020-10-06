using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    [SerializeField] GameObject inventoryGameObj;
    [SerializeField] KeyCode[] toggleInventoryKeys;

    void Update()
    {
        for (int i = 0; i < toggleInventoryKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleInventoryKeys[i]))
            {
                inventoryGameObj.SetActive(!inventoryGameObj.activeSelf);
                break;
            }
        }
    }
}
