using UnityEngine;

public class NpcAi : MonoBehaviour
{
    public NpcState npcState = NpcState.FirstTalk;

    void StateMachine()
    {
        switch (npcState)
        {
            case NpcState.FirstTalk:
                break;
            case NpcState.SecondTalk:
                break;
            case NpcState.Yes:
                break;
            case NpcState.No:
                break;
        }
    }
}
public enum NpcState
{
    FirstTalk, SecondTalk, Yes, No
}