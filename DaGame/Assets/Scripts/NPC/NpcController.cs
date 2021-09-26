using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private NpcPopupController _popupController;
    [SerializeField] private Animator _animator;
    [SerializeField] private NpcData _npcData;
    [SerializeField] private GameObject _emotionPopupBase;
    [SerializeField] private SpriteRenderer _emotionPopupSpriteRenderer;
    
   [SerializeField] private List<ItemPairs> _pairs;
  [SerializeField]  private List<Item> _itemsWeHave;
  private bool _isSoundPlayed;
  
    private void Start()
    {
        _emotionPopupBase.SetActive(false);
        _animator.runtimeAnimatorController = _npcData.IdleAnim;
        _pairs = new List<ItemPairs>();
        _itemsWeHave = new List<Item>();

        for (int i = 0; i < _npcData.Pairs.Count; i++)
        {
            ItemPairs emptyPair = new ItemPairs();
            ItemPairs fullPair = _npcData.Pairs[i];
            
            foreach (var VARIABLE in fullPair.ItemsWeGive)
            {
                emptyPair.ItemsWeGive.Add(VARIABLE);
            }
            
            foreach (var VARIABLE in fullPair.ItemsWeNeed)
            {
                emptyPair.ItemsWeNeed.Add(VARIABLE);
            }
            
            _pairs.Add(emptyPair);
        }

    }

    public void PlayerUpSignal()
    {
        if (_popupController.IsPopupWindowActive)
        {
            _popupController.GoUp();
        }
    }

    public void PlayerDownSignal()
    {
        if (_popupController.IsPopupWindowActive)
        {
            _popupController.GoDown();
        }
    }

    public void PlayerInteractionSignal(PlayerController playerController)
    {
        if (_popupController.IsPopupWindowActive)
        {
            switch (_popupController.GetCurrentIndex())
            {
                case 0:
                    _emotionPopupBase.SetActive(true);
                    _emotionPopupSpriteRenderer.sprite = _npcData.EmotionSprite;
                    _emotionPopupSpriteRenderer.transform.localScale = Vector3.one *_npcData.EmotionSizeMult;
                    DOVirtual.DelayedCall(1, () =>
                    {
                        _emotionPopupBase.SetActive(false);
                    });
                    
                    break; // Talk
                case 1:
                    playerController.OpenBackpackSignal();
                    break; // trade
                case 2: 
                    _popupController.ClosePopup();
                    playerController.CloseInteractionWithNpc();
                    break; // exit
            }

        }
        else
        {
            _popupController.OpenPopup();
        }
    }

    public bool IsItemFeasible(BackpackManager.BackpackItemInfo info)
    {
        Item item = info.Item;

        foreach (var parList in _pairs)
        {
            foreach (var VARIABLE in parList.ItemsWeNeed)
            {
                if (item == VARIABLE)
                {
                    return true;
                }
            }
          
        }
        
        return false;
    }

    public void FeedItem(BackpackManager.BackpackItemInfo info, PlayerController playerController)
    {
        _itemsWeHave.Add(info.Item);
         OnItemAdded(playerController);
    }

    private void OnItemAdded(PlayerController playerController)
    {

        for (int i = _pairs.Count - 1; i >= 0; i--)
        {
            ItemPairs pair = _pairs[i];
            bool weFoundMatchingPair = true;
            
            for (int j = 0; j < pair.ItemsWeNeed.Count; j++)
            {
                Item weNeed = pair.ItemsWeNeed[j];
                bool weDontHaveTheItem = true;
                for (int k = 0; k < _itemsWeHave.Count; k++)
                {
                    Item weHave = _itemsWeHave[k];

                    if (weNeed == weHave)
                    {
                        weDontHaveTheItem = false;
                        break;
                    }
                }

                if (weDontHaveTheItem)
                {
                    weFoundMatchingPair = false;
                    break;
                }
                
            }

            if (weFoundMatchingPair)
            {

                for (int j = pair.ItemsWeNeed.Count - 1; j >= 0; j--)
                {
                    Item weNeed = pair.ItemsWeNeed[j];

                    for (int k = _itemsWeHave.Count - 1; k >= 0; k--)
                    {
                        Item weHave = _itemsWeHave[k];

                        if (weNeed == weHave)
                        {
                            pair.ItemsWeNeed.RemoveAt(j);
                            _itemsWeHave.RemoveAt(k);
                        }
                    }
                }

                if (_npcData.OnTakeAudioClip != null)
                {
                    
                    playerController.TakeItemFromNpc(pair.ItemsWeGive, _npcData.TakeSprites, !_isSoundPlayed, _npcData.OnTakeAudioClip.length);
                }
                else
                {
                    playerController.TakeItemFromNpc(pair.ItemsWeGive, _npcData.TakeSprites, false, 0);
                }

                if (!_isSoundPlayed)
                {
                    if (_npcData.OnTakeAudioClip != null)
                    {
                        AudioManager.instance.TakeMemeSound(_npcData.OnTakeAudioClip);
                    }
                    _isSoundPlayed = true;
                }
                
                _pairs.RemoveAt(i);
                
                
                break;
            }
            
        }
    }
}