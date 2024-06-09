using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    private int Health
    {
        get
        {
            if (_health <= 0)
            {
                Dead();
                _health = 0;
            }
            return _health;
        }
        set { _health = value; }
    }

    [SerializeField, Range(1, 3)] private int _health;
    [SerializeField] private bool _isPlayersHealth;
    [SerializeField] private SpriteRenderer _deafoultSprite;
    [SerializeField] private Sprite _deadSprite;
    [SerializeField] private GameObject _deadZombieObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            TakeDamage(1);
    }

    public void TakeDamage(int outDamage)
    {
        Health -= outDamage;
        print($"{this.name} taked damage {outDamage}. Health {Health}");
    }

    private void Dead()
    {
        if (_isPlayersHealth)
        {
            _deafoultSprite.sprite = _deadSprite;
        }
        else
        {
            Instantiate(_deadZombieObject,transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
