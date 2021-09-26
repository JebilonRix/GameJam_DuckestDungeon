using System;
using System.Collections.Generic;
using DG.Tweening;
using SEP;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] private GameObject playerPrefab;
    public float WalkingSpeed = 10f;

    [Header("Sound")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip walkingSFX;
    [SerializeField] private AudioMixerSnapshot walking;
    [SerializeField] private AudioMixerSnapshot notWalking;
    [SerializeField] private AudioMixer mixer;

    [Header("Depedencies")] 
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private BackpackManager _backpackManager;
    [SerializeField] private GiveMemeCanvasController _giveMemeCanvasController;


    private bool _isInRangeOfNpcInteraction;
    private bool _isInteractingWithNpc;
    private NpcController _inRangeNpcController;
    
    private InputFlag _moveUpFlag = new InputFlag(KeyCode.W);
    private InputFlag _moveDownFlag = new InputFlag(KeyCode.S);
    private InputFlag _moveRightFlag = new InputFlag(KeyCode.D);
    private InputFlag _moveLeftFlag = new InputFlag(KeyCode.A);
    private InputFlag _interactionFlag = new InputFlag(KeyCode.E);
    private InputFlag _confirmationFlag = new InputFlag(KeyCode.Space);
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        source.clip = walkingSFX;
        source.loop = true;
    }
    private void Start()
    {
        source.Play();
    }
    public void Move(float x, float y, bool play = true)
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + new Vector2(x, y) * (WalkingSpeed * Time.fixedDeltaTime));

        PlayWalkSFX(play);
    }

    public void OnInteractionCommandTrigger()
    {
        if (_isInRangeOfNpcInteraction)
        {

            if (_isInteractingWithNpc== false)
            {
                _isInteractingWithNpc = true;
                PlayWalkSFX(false);
            }
            
            _inRangeNpcController.PlayerInteractionSignal(this);
        }
    }
    
    
    public void CloseInteractionWithNpc()
    {
        if (_isInteractingWithNpc)
        {
            _isInteractingWithNpc = false;
        }
    }
    
    
    public void OnInRangeOfNpcInteractionValueChange()
    {
        if (_isInRangeOfNpcInteraction)
        {
            // On player is in range
        }
        else
        {
            // On player out of range
        }
        
    }
    
    public void OpenBackpackSignal()
    {
        if (_backpackManager.IsBackpackActive)
        {
            
        }
        else
        {
            _backpackManager.OpenBackpack();
        }
    }

    
    public void PlayWalkSFX(bool play)
    {
        if (play)
        {
            walking.TransitionTo(0.1f);
        }
        else
        {
            notWalking.TransitionTo(0.1f);
        }
    }
    
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        var behaviour = other.GetComponent<NpcTriggerColliderBehaviour>();

        if (behaviour != null)
        {
            _inRangeNpcController = behaviour.ParentController;

            if (_isInRangeOfNpcInteraction == false)
            {
                _isInRangeOfNpcInteraction = true;
                OnInRangeOfNpcInteractionValueChange();
            }
            
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var behaviour = other.GetComponent<NpcTriggerColliderBehaviour>();

        if (behaviour != null)
        {
            _inRangeNpcController = null;

            if (_isInRangeOfNpcInteraction == true)
            {
                _isInRangeOfNpcInteraction = false;
                OnInRangeOfNpcInteractionValueChange();
            }
        }
    }
    
    private void Update()
    {
        _moveUpFlag.SetFlags();
        _moveDownFlag.SetFlags();
        _moveRightFlag.SetFlags();
        _moveLeftFlag.SetFlags();
        _interactionFlag.SetFlags();
        _confirmationFlag.SetFlags();
    }
    
    public void FixedUpdate()
    {
        if (_backpackManager.IsBackpackActive)
        {
            if (_moveRightFlag.Start)
            {
                _backpackManager.OnRightCommandTrigger();
            }

            if (_moveLeftFlag.Start)
            {
                _backpackManager.OnLeftCommandTrigger();
            }

            if (_confirmationFlag.Start)
            {
                var info = _backpackManager.GetItemInfo();

                OnGetItemInfoFromBackpack(info);
           

            }
            
        }
        else if (_isInteractingWithNpc)
        {
            if (_moveUpFlag.Start)
            {
               _inRangeNpcController.PlayerUpSignal();
            }

            if (_moveDownFlag.Start)
            {
                _inRangeNpcController.PlayerDownSignal();
            }
        }
        else
        {
            #region Movement

            Vector2 movement = Vector2.zero;

            if (_moveUpFlag.Update)
            {
                movement += Vector2.up;
            }

            if (_moveDownFlag.Update)
            {
                movement += Vector2.down;
            }


            if (_moveRightFlag.Update)
            {
                movement += Vector2.right;
            }

            if (_moveLeftFlag.Update)
            {
                movement += Vector2.left;
            }

            if (movement.magnitude < 0.5f)
            {
                PlayerController.Instance.Move(0, 0, false);
            }
            else
            {
                PlayerController.Instance.Move(movement.normalized.x, movement.normalized.y, true);
            }


            #endregion
        }
        
        
        if (_interactionFlag.Start)
        {
            PlayerController.Instance.OnInteractionCommandTrigger();
        }


        _moveUpFlag.ResetStartEndFlags();
        _moveDownFlag.ResetStartEndFlags();
        _moveRightFlag.ResetStartEndFlags();
        _moveLeftFlag.ResetStartEndFlags();
        _interactionFlag.ResetStartEndFlags();
        _confirmationFlag.ResetStartEndFlags();
    }

    private void OnGetItemInfoFromBackpack(BackpackManager.BackpackItemInfo info)
    {
        if (_isInteractingWithNpc)
        {
            if (_inRangeNpcController.IsItemFeasible(info))
            {
                _inRangeNpcController.FeedItem(info,this);

                if (!info.IsEmpty)
                {
                    _backpackManager.RemoveItem(info);
                }
            }
            else
            {
                _backpackManager.CloseBackpack();
            }
        }
        
        
    }

    public void TakeItemFromNpc(List<Item> pairsItemsWeGive, List<Sprite> giveMem, bool isLong, float duration)
    {
        Tween giveTween = _giveMemeCanvasController.ActivateMeme(giveMem, isLong, duration).OnKill(() =>
        {
            _backpackManager.CloseBackpack();
            _backpackManager.AddItems(pairsItemsWeGive);
        });
        

    }
}