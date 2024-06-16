using System;
using UnityEngine;

public class RestartAfterPlayerDeath : MonoBehaviour
{
    [SerializeField] private UIVisibilityController _restartController;

    private void Awake()
    {
        EventManager.PlayerDeathEvent.AddListener(OpenRestartMenu);
    }

    private void OpenRestartMenu() =>
        _restartController.ObjectOn();
}
