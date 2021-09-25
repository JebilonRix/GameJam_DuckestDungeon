using UnityEngine;
using UnityEngine.Audio;

public class TopDownMovement : MonoBehaviour
{
    public static TopDownMovement instance;

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

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
        _rigidbody2D.MovePosition(_rigidbody2D.position + new Vector2(x, y) * (WalkingSpeed * Time.deltaTime));

        PlayWalkSFX(play);
    }
    public void PlayWalkSFX(bool play)
    {
        if (play)
        {
            walking.TransitionTo(0.05f);
        }
        else
        {
            notWalking.TransitionTo(0.05f);
        }
    }
}