using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public InputActionReference move;
    public InputActionReference interact;
    private Vector2 moveDirection;
    private bool isFacingRight = true;
    void Start()
    {}
    void Update()
    {
        moveDirection = move.action.ReadValue<Vector2>(); 
        transform.Translate(new Vector3(moveDirection.x * moveSpeed * Time.deltaTime, moveDirection.y * moveSpeed * Time.deltaTime, 0));
        if (moveDirection.x > 0 && !isFacingRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isFacingRight = true;
        }
        else if (moveDirection.x < 0 && isFacingRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
            isFacingRight = false;
        }
         
        

    }
}
