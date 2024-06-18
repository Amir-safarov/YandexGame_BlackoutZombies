using UnityEngine;

public class TrapsRotation : MonoBehaviour
{
    private float _rotationSpeed = 2.3f;

    private void Update() =>
        transform.Rotate(0,0,_rotationSpeed);
}
