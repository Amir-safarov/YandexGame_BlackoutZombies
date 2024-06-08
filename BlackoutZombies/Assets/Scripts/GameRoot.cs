using UnityEngine;

public class GameRoot : MonoBehaviour
{
    [SerializeField] private PlayerShooting _playerShooting;
    [SerializeField] private PlayerMovement _playerMove;

    private bool _canPlayerControll;

    private void Update()
    {
        if (!_canPlayerControll)
            return;
        _playerMove.Move();
        _playerShooting.Shoot();
    }

    public void OpenPlayerControll() =>
        _canPlayerControll = true;

    public void ClosePlayerControll() =>
        _canPlayerControll = false;
}
