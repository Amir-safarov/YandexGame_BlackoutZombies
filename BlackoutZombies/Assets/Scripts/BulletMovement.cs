using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class BulletMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField, Range(15f, 20)] private float _bulletSpeed;
    [SerializeField, Range(2f, 10)] private float _bulletLifeTime;

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
