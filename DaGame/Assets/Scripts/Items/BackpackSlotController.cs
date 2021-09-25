using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackSlotController : MonoBehaviour
{

    private Item _item;
    private bool _hasItem;
    public bool HasItem => _hasItem;
    public Item Item => _item;


    public void InsertItem(Item item)
    {
        this._item = item;
        _hasItem = true;
    }

    public Item RemoveItem()
    {
        _hasItem = false;
        return _item;
    }
    
    
}
