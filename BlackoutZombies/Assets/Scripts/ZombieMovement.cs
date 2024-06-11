using UnityEngine;

public class ZombieMovement : MonoBehaviour, IMovable
{
    [SerializeField] private Transform _playerePosition;
    [SerializeField] private float _movementSpeed;
    private float _rotationSpeed = 0.1f;

    private void OnEnable()
    {
        _movementSpeed = Random.Range(5.5f, 6.5f);
    }

    public void Move()
    {
        Vector2 targetDirection = _playerePosition.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, angle), _rotationSpeed);
        transform.position = Vector3.MoveTowards(transform.position, _playerePosition.position, _movementSpeed * Time.deltaTime);
    }

    private void Update()
    {
        Move();
    }
}
