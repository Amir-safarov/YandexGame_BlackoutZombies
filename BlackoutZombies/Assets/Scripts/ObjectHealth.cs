using System;
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
    [SerializeField] private SpriteRenderer _platerSpriteRenderer;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _deadSprite;
    [SerializeField] private GameObject _deadPlayer;
    [SerializeField] private GameObject _deadZombieObject;

    private const string ZombieTag = "Zombie";
    private const string PlayerTag = "Player";
    private const string DamageObjectTag = "DamageObject";
    private const int ZombiesDamage = 1;
    private const int DamageObjectDamage = 4;
    private const int MaxObjectHealth = 3;
    private const float InvulnerabilityTime = 1.5f;

    private readonly Color DeafaultPlayerSpriteColor = new Color(1f, 1f, 1f, 1f);
    private readonly Color PlayerInvulnerabilitySpriteColor = new Color(1f, 1f, 1f, 0.9f);

    private string _currentObjectTag;

    private void OnValidate()
    {
        if (!_playerCollider)
            _playerCollider = GetComponent<BoxCollider2D>();
        _defaultSprite = _platerSpriteRenderer.sprite;
    }

    private void Awake()
    {
        EventManager.RestartSceneEvent.AddListener(SetDeafaultState);
    }

    private void SetDeafaultState(bool isReviev)
    {
        _platerSpriteRenderer.sprite = _defaultSprite;
        Health = MaxObjectHealth;
        _platerSpriteRenderer.color = DeafaultPlayerSpriteColor;
        _playerCollider.isTrigger = true;
    }

    private void OnEnable()
    {
        _currentObjectTag = gameObject.tag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckZombiesHit(collision);
        CheckDamageObjectHit(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckDamageObjectHit(collision);
    }

    public void HealthPointUpObject(int outHPUp)
    {
        Health += outHPUp;
        EventManager.InvokeTransferHeart(Health);
    }

    public void TakeDamage(int outDamage)
    {
        Health -= outDamage;
        print($"{name} taked damage {outDamage}.\n Health {Health}");
    }

    private void CheckDamageObjectHit(Collider2D collision)
    {
        if (collision.CompareTag(DamageObjectTag))
        {
            TakeDamage(DamageObjectDamage);
            EventManager.InvokeTransferHeart(Health);
        }
    }
    private void CheckDamageObjectHit(Collision2D collision)
    {
        if (collision.collider.CompareTag(DamageObjectTag))
        {
            TakeDamage(DamageObjectDamage);
            EventManager.InvokeTransferHeart(Health);
        }
    }

    private void CheckZombiesHit(Collider2D collision)
    {
        if (_currentObjectTag == PlayerTag && collision.CompareTag(ZombieTag))
        {
            TakeDamage(ZombiesDamage);
            StartCoroutine(PlayerInvulnerability());
            EventManager.InvokeTransferHeart(Health);
        }
    }
    private IEnumerator PlayerInvulnerability()
    {
        _platerSpriteRenderer.color = PlayerInvulnerabilitySpriteColor;
        _playerCollider.isTrigger = false;
        yield return new WaitForSeconds(InvulnerabilityTime);
        _platerSpriteRenderer.color = DeafaultPlayerSpriteColor;
        _playerCollider.isTrigger = true;
    }

    private void ObjectDeath()
    {
        if (_isPlayersHealth)
        {
            _platerSpriteRenderer.color = DeafaultPlayerSpriteColor;
            _platerSpriteRenderer.sprite = _deadSprite;
            EventManager.InvokePlayersDeath();
        }
        else
        {
            Instantiate(_deadZombieObject, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
