using System;
using UnityEngine;

public class RemoveUsedObject : MonoBehaviour
{
    private void Awake()
    {
        EventManager.RestartSceneEvent.AddListener(RemoveBody);
    }

    private void RemoveBody() =>
        Destroy(gameObject);
}
