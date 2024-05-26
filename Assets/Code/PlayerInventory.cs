using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory
{
    private List<Item> itemList;

    public PlayerInventory()
    {
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.Shovel, amount = 1 });
        Debug.Log(itemList.Count);
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }
}
