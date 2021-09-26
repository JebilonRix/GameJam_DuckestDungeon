using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BackpackManager : MonoBehaviour
{
    [Header("Dependendices")] 
    [SerializeField] private GameObject _ctnBackpack;

    [SerializeField] private RectTransform _ctnBackpackRectTransform;
    [SerializeField] private List<BackpackSlotController> _slots;


    [Header("Settings")]
    [SerializeField] private float _someSetting;

    private float _backpackDisabledYPos = -250;

    private float _backpackEnabledYPos = 0;

    private bool _isBackpackActive;
    private int _slotIndex;
    public bool IsBackpackActive => _isBackpackActive;


    private void Start()
    {
        CloseBackpack(true);
    }

    public void OnRightCommandTrigger()
    {
        _slotIndex = (_slotIndex+1) % _slots.Count;
    }

    public void OnLeftCommandTrigger()
    {
        _slotIndex = (_slotIndex-1) % _slots.Count;
    }

    public void OpenBackpack()
    {
        _isBackpackActive = true;

        _ctnBackpackRectTransform.anchoredPosition = new Vector2(0, _backpackDisabledYPos);
        _ctnBackpack.SetActive(true);
        _ctnBackpackRectTransform.DOAnchorPosY(_backpackEnabledYPos, 0.5f);
    }

    public void CloseBackpack( bool Immediate = false)
    {

        if (Immediate)
        {
            _ctnBackpack.SetActive(false);
            _ctnBackpackRectTransform.anchoredPosition = new Vector2(0, _backpackEnabledYPos);
            _isBackpackActive = false;
        }
        else
        {
            _ctnBackpackRectTransform.anchoredPosition = new Vector2(0, _backpackEnabledYPos);
            _isBackpackActive = false;
            _ctnBackpackRectTransform.DOAnchorPosY(_backpackDisabledYPos, 0.5f)
                .SetEase(Ease.InBack)
                .OnComplete( () =>
                {
                    _ctnBackpack.SetActive(false);
                } );
        }

        
       
    }


    public void InsertItem(Item item)
    {
        bool itemInserted = false;
        for (int i = 0; i < _slots.Count; i++)
        {
            var slot = _slots[i];

            if (!slot.HasItem)
            {
                itemInserted = true;
                slot.InsertItem(item);
                break;
            }
        }

        if (itemInserted)
        {
            // Nice we good to go
        }
        else
        {
            Debug.LogError("NOT ENOUGH SLOTS!");
        }
        
        
    }

    public void RemoveItem(BackpackItemInfo info)
    {
        var slot = _slots[info.SlotIndex];

        if (slot.HasItem)
        {
            slot.RemoveItem();
            for (int i = 0; i < _slots.Count; i++)
            {
                var slotToFit = _slots[i];

                if (!slotToFit.HasItem)
                {
                    continue;
                }

                for (int j = 0; j < i; j++)
                {
                    var emptySlot = _slots[j];

                    if (!emptySlot.HasItem)
                    {
                        emptySlot.InsertItem(slotToFit.RemoveItem());
                        break;
                    }
                }
            }
        }
        else
        {
            Debug.LogError("ERROR, slot has no item");
        }
        
    }

    public BackpackItemInfo GetItemInfo()
    {
        var slot = _slots[_slotIndex];
        return new BackpackItemInfo(_slotIndex,slot.Item, !slot.HasItem);
    }

    public class BackpackItemInfo
    {
        public bool IsEmpty;
        public Item Item;
        public int SlotIndex;

        public BackpackItemInfo( int slotIndex, Item item,  bool isEmpty = false)
        {
            this.Item = item;
            this.IsEmpty = isEmpty;
            this.SlotIndex = slotIndex;
        }
    }
}
