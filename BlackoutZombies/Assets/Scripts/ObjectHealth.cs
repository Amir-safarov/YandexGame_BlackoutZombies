using System.Collections;
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

    [SerializeField] private BoxCollider2D _playerCollider;
    [SerializeField, Range(1, 3)] private int _health;
    [SerializeField] private bool _isPlayersHealth;
    [SerializeField] private SpriteRenderer _deafoultSprite;
    [SerializeField] private Sprite _deadSprite;
    [SerializeField] private GameObject _deadPlayer;
    [SerializeField] private GameObject _deadZombieObject;

    private const string ZombieTag = "Zombie";
    private const string DamageObjectTag = "DamageObject";
    private const int ZombiesDamage = 1;
    private const int DamageObjectDamage = 4;
    private const int MaxObjectHealth = 3;
    private const float InvulnerabilityTime = 1.5f;

    private readonly Color DeafaultPlayerSpriteColor = new Color(1f, 1f, 1f, 1f);
    private readonly Color PlayerInvulnerabilitySpriteColor = new Color(1f, 1f, 1f, 0.9f);

    private void OnValidate()
    {
        if (!_playerCollider)
            _playerCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ZombieTag))
        {
            TakeDamage(ZombiesDamage);
            StartCoroutine(PlayerInvulnerability());
        }
        if (collision.CompareTag(DamageObjectTag))
            TakeDamage(DamageObjectDamage);
        EventManager.InvokeTransferHeart(Health);
    }

    public void HealthPointUpObject(int outHPUp)
    {
        Health += outHPUp;
        EventManager.InvokeTransferHeart(Health);
    }

    public void TakeDamage(int outDamage)
    {
        Health -= outDamage;
        print($"{name} taked damage {outDamage}. Health {Health}");
    }

    private IEnumerator PlayerInvulnerability()
    {
        _deafoultSprite.color = PlayerInvulnerabilitySpriteColor;
        _playerCollider.isTrigger = false;
        yield return new WaitForSeconds(InvulnerabilityTime);
        _deafoultSprite.color = DeafaultPlayerSpriteColor;
        _playerCollider.isTrigger = true;
    }

    private void ObjectDeath()
    {
        if (_isPlayersHealth)
        {
            _deafoultSprite.color = DeafaultPlayerSpriteColor;
            _deafoultSprite.sprite = _deadSprite;
            EventManager.InvokePlayersDeath();
        }
        else
        {
            Instantiate(_deadZombieObject, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
