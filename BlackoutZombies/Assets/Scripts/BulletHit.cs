using UnityEngine;

public class BulletHit : MonoBehaviour
{
    private const string ZombieTag = "Zombie";
    private const int BulletDamage = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ZombieTag))
        {
            Destroy(gameObject);
            ObjectHealth collisionObject = collision.gameObject.GetComponent<ObjectHealth>();
            if (collisionObject != null)
            {
                collisionObject.TakeDamage(BulletDamage);
                EventManager.InvokeTransferZombieDeath();
            }
        }
    }
}
