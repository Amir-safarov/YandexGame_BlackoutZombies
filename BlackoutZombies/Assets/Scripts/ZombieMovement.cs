using UnityEngine;

public class ZombieMovement : MonoBehaviour, IMovable
{
    [SerializeField] private Transform _playerePosition;
    [SerializeField] private float _movementSpeed;
    private float _rotationSpeed = 0.1f;
    private bool _canMove;

    private void OnEnable()
    {
        _movementSpeed = Random.Range(5f, 6f);
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

    public void OpenZombieMove()=>
        _canMove = true;

    public void CloseZombieMove()=>
        _canMove = false;
}
