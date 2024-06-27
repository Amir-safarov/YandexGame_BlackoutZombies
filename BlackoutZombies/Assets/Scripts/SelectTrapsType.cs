using System.Collections.Generic;
using UnityEngine;

public class SelectTrapsType : MonoBehaviour
{
    [SerializeField] private List<GameObject> _trapsType;

    private readonly Vector3 TrapPosition = Vector3.zero;

    private void Awake()
    {
        EventManager.RestartSceneEvent.AddListener(SelectTrapType);
    }

    public void SelectTrapType(bool isRevive = false) =>
        Instantiate(_trapsType[Random.Range(0, _trapsType.Count - 1)], TrapPosition, Quaternion.identity);

}
