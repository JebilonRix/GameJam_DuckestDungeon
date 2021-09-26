using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private NpcPopupController _popupController;

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
                case 0: break; // Talk
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
        return true;
    }

    public void FeedItem(BackpackManager.BackpackItemInfo info)
    {
        
    }
}