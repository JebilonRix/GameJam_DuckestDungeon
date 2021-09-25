using System;
using SEP;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [SerializeField]
    KeyCode MoveUp = KeyCode.W, MoveDown = KeyCode.S, MoveRight = KeyCode.D,
        MoveLeft = KeyCode.A, Interaction = KeyCode.E, Confimation = KeyCode.Space;

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
<<<<<<< HEAD
       
        if (_moveUpFlag.End)
        {
            TopDownMovement.instance.Move(0, 0, false);
        }
        else if (_moveUpFlag.Update)
        {
            TopDownMovement.instance.Move(0, 1);
        }

        if (_moveDownFlag.End)
        {
            TopDownMovement.instance.Move(0, 0, false);
=======
        Vector2 movement = Vector2.zero;
        
        if (Input.GetKey(MoveUp))
        {
            movement += Vector2.up;
>>>>>>> alpago
        }
        else if (_moveDownFlag.Update)
        {
<<<<<<< HEAD
            TopDownMovement.instance.Move(0, -1);
        }

        if (_moveRightFlag.End)
        {
            TopDownMovement.instance.Move(0, 0, false);
        }
        else if (_moveRightFlag.Update)
=======
            movement += Vector2.down;
        }


        if (Input.GetKey(MoveRight))
>>>>>>> alpago
        {
            movement += Vector2.right;
        }

<<<<<<< HEAD
        if (_moveLeftFlag.End)
=======

        if (Input.GetKey(MoveLeft))
>>>>>>> alpago
        {
            movement += Vector2.left;
        }
<<<<<<< HEAD
        else if (_moveLeftFlag.Update)
=======


        if (movement.magnitude < 0.1f)
>>>>>>> alpago
        {
            TopDownMovement.instance.Move(0,0,false);
        }
<<<<<<< HEAD
=======
        else
        {
            TopDownMovement.instance.Move(movement.normalized.x, movement.normalized.y,true);
        }
        
        #endregion
>>>>>>> alpago

        #endregion
        
        
        if (Input.GetKeyDown(Interaction))
        {

        }
        if (Input.GetKeyDown(Confimation))
        {

        }
        
        _moveUpFlag.ResetStartEndFlags();
        _moveDownFlag.ResetStartEndFlags();
        _moveRightFlag.ResetStartEndFlags();
        _moveLeftFlag.ResetStartEndFlags();
        _interactionFlag.ResetStartEndFlags();
        _confirmationFlag.ResetStartEndFlags();
        
    }
}