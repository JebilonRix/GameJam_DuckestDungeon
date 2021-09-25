using System;
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

    private bool _isInRangeOfNpcInteraction;
    private NpcController _inRangeNpcController;
    
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
}