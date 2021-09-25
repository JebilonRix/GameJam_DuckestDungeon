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
        Vector2 movement = Vector2.zero;
        
        if (Input.GetKey(MoveUp))
        {
            movement += Vector2.up;
        }

        if (Input.GetKey(MoveDown))
        {
            movement += Vector2.down;
        }


        if (Input.GetKey(MoveRight))
        {
            movement += Vector2.right;
        }


        if (Input.GetKey(MoveLeft))
        {
            movement += Vector2.left;
        }


        if (movement.magnitude < 0.1f)
        {
            TopDownMovement.instance.Move(0,0,false);
        }
        else
        {
            TopDownMovement.instance.Move(movement.normalized.x, movement.normalized.y,true);
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