using System;
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
    
    private bool _isInRangeOfNpcInteraction;
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
            InteractWithNpc(_inRangeNpcController);
        }
    }

    private void InteractWithNpc(NpcController inRangeNpcController)
    {
        Debug.Log("Interacted");
    }

    public void OnConfirmationCommandTrigger()
    {
       
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
            _isInRangeOfNpcInteraction = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var behaviour = other.GetComponent<NpcTriggerColliderBehaviour>();

        if (behaviour != null)
        {
            _inRangeNpcController = null;
            _isInRangeOfNpcInteraction = false;
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
        if (_confirmationFlag.Start)
        {
            PlayerController.Instance.OnConfirmationCommandTrigger();
        }

        _moveUpFlag.ResetStartEndFlags();
        _moveDownFlag.ResetStartEndFlags();
        _moveRightFlag.ResetStartEndFlags();
        _moveLeftFlag.ResetStartEndFlags();
        _interactionFlag.ResetStartEndFlags();
        _confirmationFlag.ResetStartEndFlags();
    }
    
}