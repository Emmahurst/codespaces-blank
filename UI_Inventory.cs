using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;


    //Inventory
    public List<Item> inventoryTest = new List<Item>();
    public int[] inventoryCountTest = new int[9];

    //Item on the ground
    public Item itemToPickUp;

    private void Awake()
    {



        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotContainer = transform.Find("itemSlotTemplate");
    }

    void AddItemToInventory(int itemCount, Item item)
    {
        inventoryTest.Add(itemToPickUp);

    }

    // Removing item from inventory
    public void RemoveItemFromInventory(int itemCount)
    {
        //inventoryTest.RemoveAt(i);
    }

    public void OnPickupItemTest(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inventory.AddItem(new Item { itemType = Item.ItemType.Sword, amount = 1 });
        }
    }


    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 30f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            x++;
            if (x > 4)
            {
                x = 0;
                y++;

            }
        }
    }



}

