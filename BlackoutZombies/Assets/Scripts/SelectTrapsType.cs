using System.Collections.Generic;
using UnityEngine;

public class SelectTrapsType : MonoBehaviour
{
    [SerializeField] private List<GameObject> _trapsType;

    public void SelectTrapType() =>
        Instantiate(_trapsType[Random.Range(0, _trapsType.Count - 1)], Vector3.zero, Quaternion.identity);

}
