using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class BulletMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField, Range(1f, 20)] private float _bulletSpeed;

    private const float _bulletLifeTime = 1.5f;

    private void OnValidate()
    {
        if(_rb == null)
            _rb= GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, _bulletLifeTime);
    }

    private void FixedUpdate()
    {
        _rb.velocity = transform.right * _bulletSpeed;
    }
}
