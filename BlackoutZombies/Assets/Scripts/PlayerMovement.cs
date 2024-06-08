using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private Vector2 _moveVector;
    private Vector2 _mousePosition;

    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    private void Update()
    {
        _moveVector.x = Input.GetAxisRaw(Horizontal);
        _moveVector.y = Input.GetAxisRaw(Vertical);
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(_mousePosition.y - transform.position.y, _mousePosition.x - transform.position.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.Euler(0, 0, angle);
        transform.position +=(Vector3)_moveVector * _movementSpeed * Time.deltaTime;
    }

}
