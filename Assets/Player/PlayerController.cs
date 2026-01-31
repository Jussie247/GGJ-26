using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;
    void Start()
    {}
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.position = new Vector3(horizontal, vertical, 0) * moveSpeed * Time.deltaTime;
    }
}
