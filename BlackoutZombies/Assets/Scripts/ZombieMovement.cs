using UnityEngine;

public class ZombieMovement : MonoBehaviour, IMovable
{
    [SerializeField] private Transform _playerePosition;

    private float _rotationSpeed = 0.25f;
    private float _movementSpeed;
    private bool _canMove;

    private const float _minMovementSpeed = 3f;
    private const float _maxMovementSpeed = 4f;


    private void OnEnable()
    {
        _movementSpeed = Random.Range(_minMovementSpeed, _maxMovementSpeed);
    }

    private void Update()
    {
        if (!_canMove)
            return;
        Move();
    }

    public void Move()
    {
        Vector2 targetDirection = _playerePosition.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, angle), _rotationSpeed);
        transform.position = Vector3.MoveTowards(transform.position, _playerePosition.position, _movementSpeed * Time.deltaTime);
    }

    public void OpenZombieMove() =>
        _canMove = true;

    public void CloseZombieMove() =>
        _canMove = false;
}
