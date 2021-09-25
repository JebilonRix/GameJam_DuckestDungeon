using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [SerializeField]
    KeyCode MoveUp = KeyCode.W, MoveDown = KeyCode.S, MoveRight = KeyCode.D,
        MoveLeft = KeyCode.A, Interaction = KeyCode.E, Confimation = KeyCode.Space;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        #region Movement
        if (Input.GetKey(MoveUp))
        {
            TopDownMovement.instance.Move(0, 1);
        }
        else if (Input.GetKeyUp(MoveUp))
        {
            TopDownMovement.instance.Move(0, 0, false);
        }

        if (Input.GetKey(MoveDown))
        {
            TopDownMovement.instance.Move(0, -1);
        }
        else if (Input.GetKeyUp(MoveDown))
        {
            TopDownMovement.instance.Move(0, 0, false);
        }

        if (Input.GetKey(MoveRight))
        {
            TopDownMovement.instance.Move(1, 0);
        }
        else if (Input.GetKeyUp(MoveRight))
        {
            TopDownMovement.instance.Move(0, 0, false);
        }

        if (Input.GetKey(MoveLeft))
        {
            TopDownMovement.instance.Move(-1, 0);
        }
        else if (Input.GetKeyUp(MoveLeft))
        {
            TopDownMovement.instance.Move(0, 0, false);
        }
        #endregion

        if (Input.GetKeyDown(Interaction))
        {

        }
        if (Input.GetKeyDown(Confimation))
        {

        }
    }
}