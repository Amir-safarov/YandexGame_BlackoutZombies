using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    #region TODO Select shooting type
    [SerializeField] private enum ShottingType { Pistol, USI, Shotgun, Gun }
    [SerializeField] private ShottingType shottingType; 
    #endregion

    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField, Range(0.1f, 2f)] private float _fireRate;

    private float _fireCooldown;
    
    private const int LMB = 0;

    private void Update()
    {
        if (Input.GetMouseButton(LMB) && _fireCooldown <=0)
        {
            Shoot();
            _fireCooldown = _fireRate;
        }
        else
            _fireCooldown -= Time.deltaTime;
    }

    private void Shoot()
    {
        Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
    }
}
