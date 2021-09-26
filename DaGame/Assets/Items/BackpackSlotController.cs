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
    [SerializeField] private Image _itemImage;

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
        _itemImage.enabled = false;
    }

    public void InsertItem(Item item)
    {
        this._item = item;
        _itemImage.enabled = true;
        _itemImage.sprite = item.Sprite;
        _hasItem = true;
    }

    public void RemoveItem()
    {
        _hasItem = false;
        _itemImage.enabled = false;
        _itemImage.sprite = null;
        _item = null;
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
