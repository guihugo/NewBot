using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//aiosudaisudh
public class Item
{
    public enum ItemType{
        Up,
        Down,
        Left,
        Right,
    }

    public ItemType itemType;
    public int amount;
}
