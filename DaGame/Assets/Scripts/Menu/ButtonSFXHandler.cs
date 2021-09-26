using UnityEngine;

public class ButtonSFXHandler : MonoBehaviour
{
    [SerializeField] private AudioClip accept, back, over;
    [SerializeField] private AudioSource source;
    public void Button_SFXPlay(string clipName)
    {
        switch (clipName.ToLower())
        {
            case "accept": source.PlayOneShot(accept); break;
            case "back": source.PlayOneShot(back); break;
            case "over": source.PlayOneShot(over); break;
        }
    }
}
