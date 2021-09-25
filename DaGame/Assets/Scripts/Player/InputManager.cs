using SEP;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    private InputFlag _moveUpFlag = new InputFlag(KeyCode.W);
    private InputFlag _moveDownFlag = new InputFlag(KeyCode.S);
    private InputFlag _moveRightFlag = new InputFlag(KeyCode.D);
    private InputFlag _moveLeftFlag = new InputFlag(KeyCode.A);
    private InputFlag _interactionFlag = new InputFlag(KeyCode.E);
    private InputFlag _confirmationFlag = new InputFlag(KeyCode.Space);


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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