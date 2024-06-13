using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    private int Health
    {
        get
        {
            if (_health <= 0)
            {
                ObjectDeath();
                _health = 0;
            }
            if (_health > MaxObjectHealth)
                _health = MaxObjectHealth;
            return _health;
        }
        set { _health = value; }
    }

    [SerializeField, Range(1, 3)] private int _health;
    [SerializeField] private bool _isPlayersHealth;
    [SerializeField] private SpriteRenderer _deafoultSprite;
    [SerializeField] private Sprite _deadSprite;
    [SerializeField] private GameObject _deadZombieObject;
    [SerializeField] private GameRoot _gameRoot;
    [SerializeField] private ZombiesSpawner _zombiesSpawner;

    private const string ZombieTag = "Zombie";
    private const int ZombiesDamage = 1;
    private const int MaxObjectHealth = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ZombieTag))
            TakeDamage(ZombiesDamage);
    }


    public void HealthPointUpObject(int outHPUp)
    {
        Health += outHPUp;
        print($"{name} health up {outHPUp}. Health {Health}");
    }

    public void TakeDamage(int outDamage)
    {
        Health -= outDamage;
        print($"{name} taked damage {outDamage}. Health {Health}");
    }

    private void ObjectDeath()
    {
        if (_isPlayersHealth)
        {
            EventManager.InvokePlayersDeath();
            _deafoultSprite.sprite = _deadSprite;
        }
        else
        {
            Instantiate(_deadZombieObject, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
