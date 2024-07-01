using UnityEngine;

public class BulletHit : MonoBehaviour
{
    private const string ZombieTag = "Zombie";
    private const int BulletDamage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ZombieTag))
        {
            Destroy(gameObject);
            collision.GetComponent<ObjectHealth>().TakeDamage(BulletDamage);
            EventManager.InvokeTransferZombieDeath();
        }
    }
}
