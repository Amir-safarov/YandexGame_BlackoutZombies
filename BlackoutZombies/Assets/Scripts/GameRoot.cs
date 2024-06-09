using System;
using System.Collections;
using UnityEngine;
using YG.Example;

public class GameRoot : MonoBehaviour
{
    [SerializeField] private PlayerShooting _playerShooting;
    [SerializeField] private PlayerMovement _playerMove;

    private bool _canPlayerControll;
    private const string PlayerTag = "Player";



    private void Update()
    {
/*        if (!_canPlayerControll)
            return;
*/        _playerMove.Move();
        _playerShooting.Shoot();
    }
    public void FindPlayerShootingObject()
    {
        if (_playerShooting == null)
            _playerShooting = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<PlayerShooting>();
    }

    public void OpenPlayerControll() =>
        _canPlayerControll = true;

    public void ClosePlayerControll() =>
        _canPlayerControll = false;
}
