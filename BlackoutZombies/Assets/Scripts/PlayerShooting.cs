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
    private int _bulletsForThisGunTest;

    private const int LMB = 0;
    private float[] angleOffsets = new float[] { 8f, 0f, -8f };

    private void OnEnable()
    {
        _bulletsForThisGunTest = _bulletsCount;
    }

    public void ReloadGun()
    {
        _bulletsCount = _bulletsForThisGunTest;
    }

    public void Shoot()
    {
        if (Input.GetMouseButton(LMB) && _fireCooldown <= 0 && _bulletsCount > 0)
        {
            ShootByType();
            _bulletsCount--;
            _fireCooldown = _fireRate;
        }
        else
            _fireCooldown -= Time.deltaTime;
        TestReload();
    }

    private void ShootByType()
    {
        switch (shottingType)
        {
            case ShottingType.Shotgun:
                for (int i = 0; i < 3; i++)
                    Instantiate(_bullet, _firePoint.position, Quaternion.Euler(_firePoint.rotation.eulerAngles.x,
                        _firePoint.rotation.eulerAngles.y, _firePoint.rotation.eulerAngles.z + angleOffsets[i % angleOffsets.Length]));
                print("Shotgun shoot");
                break;
            case ShottingType.USI:
                Instantiate(_bullet, _firePoint.position, Quaternion.Euler(_firePoint.rotation.eulerAngles.x,
                    _firePoint.rotation.eulerAngles.y, _firePoint.rotation.eulerAngles.z + Random.Range(-9f, 9f)));
                print("USI shoot");
                break;
            default:
                Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
                print("Pistol or Gun shoot");
                break;
        }
    }

    private void TestReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _bulletsCount = _bulletsForThisGunTest;
            print($"Reload");
        }
    }
}
