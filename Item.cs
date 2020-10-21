using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int cost;
    public string name;
    public int quantity;

    public Item(string newName, int newCost, int initialQuantity)
    {
        cost = newCost;
        name = newName;
        quantity = initialQuantity;
    }
}

public class SpecialItem
{
    public int cost;
    public string name;
    public bool isAquired = false;
    public bool isActive = false;

    public SpecialItem(string newName, int newCost, bool aquired)
    {
        name = newName;
        cost = newCost;
        isAquired = aquired;
    }
}
