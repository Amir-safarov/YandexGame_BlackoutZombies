using UnityEngine;

public class BulletHit : MonoBehaviour
{
    private const string ZombieTag = "Zombie";
    private const int ZombiesDamage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ZombieTag))
        {
            collision.GetComponent<ObjectHealth>().TakeDamage(ZombiesDamage);
            Destroy(gameObject);
        }
    }
}
