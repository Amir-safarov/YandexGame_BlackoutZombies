using System.Collections.Generic;
using UnityEngine;

public class UIVisibilityController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _gameObjectCollection;

    public void ObjectOn() =>
        ChangeObjectsVisibility(true);
    
    public void ObjectOff() =>
        ChangeObjectsVisibility(false);


    private void ChangeObjectsVisibility(bool _objectsEnable)
    {
        foreach (GameObject gameObject in _gameObjectCollection)
            gameObject.SetActive(_objectsEnable);
    }
}
