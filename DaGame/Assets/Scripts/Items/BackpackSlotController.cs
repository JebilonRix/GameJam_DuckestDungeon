using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BackpackSlotController : MonoBehaviour
{

    [Header("Dependencies")] 
    [SerializeField] private Image _image;

    [Header("Settings")]
    [SerializeField] private Color _selectColor;
    [SerializeField] private Color _deselectColor;
    
    
    
    private Item _item;
    private bool _hasItem;
    public bool HasItem => _hasItem;
    public Item Item => _item;

    private void Awake()
    {
        OnDeselected(true);
    }

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


    public void OnSelected(bool immediate = false)
    {
        if (immediate)
        {
            _image.color =  _selectColor;
        }
        else
        {
            DOVirtual.Float(0, 1, 0.1f, value =>
            {
                _image.color = Color.Lerp(_deselectColor, _selectColor, value);
            });   
        }
    }

    public void OnDeselected(bool immediate = false)
    {
        if (immediate)
        {
            _image.color =  _deselectColor;
        }
        else
        {      
            DOVirtual.Float(0, 1, 0.1f, value =>
            {
                _image.color = Color.Lerp(_selectColor,_deselectColor, value);
            });
        }
        
    }


    
}
