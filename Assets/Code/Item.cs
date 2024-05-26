using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Shovel,
        Knife,
        //Needs items to be addded
    }
    public ItemType itemType;
    public int amount;
}
