using UnityEngine;

public class RoomTeleport : MonoBehaviour
{
    public GameObject targetSpawn;
    public BoxCollider2D exitCollider;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = targetSpawn.transform.position;
        }
    }
}
