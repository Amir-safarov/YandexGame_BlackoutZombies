using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private enum ShottingType { Pistol, USI, Shotgun, Gun }
    [SerializeField] private ShottingType shottingType;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField, Range(8, 30)] private int _bulletsCount;
    [SerializeField, Range(0.1f, 2f)] private float _fireRate;

    private float _fireCooldown;
    private int _bulletsForThisGun;

    private const int LMB = 0;
    private readonly float[] angleOffsets = new float[] { 8f, 0f, -8f };

    private void OnEnable()
    {
        DeafaultBulletsCount();
        EventManager.InvokeTransferBullets(_bulletsCount);
    }

    private void Awake()
    {
        EventManager.RestartSceneEvent.AddListener(ReloadGun);
    }


    public void ReloadGun(bool isRevive = false)
    {
        _bulletsCount = _bulletsForThisGun;
        EventManager.InvokeTransferBullets(_bulletsCount);
    }

    public void Shoot()
    {
        if (Input.GetMouseButton(LMB) && _fireCooldown <= 0 && _bulletsCount > 0)
        {
            ShootByType();
            _bulletsCount--;
            EventManager.InvokeTransferBullets(_bulletsCount);
            _fireCooldown = _fireRate;
        }
        else
            _fireCooldown -= Time.deltaTime;
    }

    private void DeafaultBulletsCount()
    {
        _bulletsForThisGun = _bulletsCount;
    }

    private void ShootByType()
    {
        switch (shottingType)
        {
            case ShottingType.Shotgun:
                for (int i = 0; i < 3; i++)
                    Instantiate(_bullet, _firePoint.position, Quaternion.Euler(_firePoint.rotation.eulerAngles.x,
                        _firePoint.rotation.eulerAngles.y, _firePoint.rotation.eulerAngles.z + angleOffsets[i % angleOffsets.Length]));
                break;
            case ShottingType.USI:
                Instantiate(_bullet, _firePoint.position, Quaternion.Euler(_firePoint.rotation.eulerAngles.x,
                    _firePoint.rotation.eulerAngles.y, _firePoint.rotation.eulerAngles.z + Random.Range(-9f, 9f)));
                break;
            default:
                Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
                break;
        }
    }
}
