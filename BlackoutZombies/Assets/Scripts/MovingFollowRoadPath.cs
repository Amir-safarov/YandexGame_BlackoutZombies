using System.Collections.Generic;
using UnityEngine;

public class MovingFollowRoadPath : MonoBehaviour
{
    [SerializeField] private RoadPath _roadPath;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField, Range(0.1f, 10)] private float _distanceOffset;
    [SerializeField] private float _movingSpeed = 5f;

    private IEnumerator<Transform> _dotInPath;

    private void OnValidate()
    {
        if (_rb == null)
            _rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        EventManager.PlayerDeathEvent.AddListener(TrapsDeactivate);
        EventManager.RestartSceneEvent.AddListener(TrapsActivate);
    }

    private void Start()
    {
        _dotInPath = _roadPath.GetNextPathPoint();
        _dotInPath.MoveNext();

        if (_dotInPath.Current == null)
            return;

        transform.position = _dotInPath.Current.position;
    }

    private void Update()
    {
        if (_dotInPath == null || _dotInPath.Current == null)
            return;
        transform.position = Vector3.MoveTowards(transform.position, _dotInPath.Current.position, Time.deltaTime * _movingSpeed);

        float suffitientOffset = (transform.position - _dotInPath.Current.position).sqrMagnitude;

        if (suffitientOffset < Mathf.Pow(_distanceOffset, 2))
        {
            _dotInPath.MoveNext();
            Vector3 direction = (_dotInPath.Current.position - transform.position).normalized;
            float angle = Vector3.SignedAngle(Vector3.right, direction, Vector3.forward);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    private void TrapsDeactivate() =>
        _rb.simulated = false;

    private void TrapsActivate(bool isRevive = false) =>
        _rb.simulated = true;

}
