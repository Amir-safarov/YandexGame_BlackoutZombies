using System.Collections;
using UnityEngine;

public class PlayerLightZone : MonoBehaviour
{
    private float CurrentLightRange
    {
        get
        {
            if (_currentLightRange >= ExtraLightRange)
                _currentLightRange = ExtraLightRange;
            if (_currentLightRange < MinLightRange)
                _currentLightRange = MinLightRange;
            return _currentLightRange;
        }
        set { _currentLightRange = value; }

    }
    [SerializeField] private Light _playerLightSource;

    private float _currentLightRange;
    private bool _isNeedToUpdateLight;

    private const float BatteryExtinctionStep = 0.005f;
    private const float ExtraLightRange = 30;
    private const float MaxLightRange = 17;
    private const float OnStartLightRange = 11;
    private const float MinLightRange = 4;

    private void OnEnable()
    {
        _isNeedToUpdateLight = true;
        CurrentLightRange = OnStartLightRange;
        _playerLightSource.range = CurrentLightRange;
    }

    private void Awake()
    {
        EventManager.PlayerDeathEvent.AddListener(LastLightBattery);
    }

    private void Update()
    {
        if (!_isNeedToUpdateLight)
            return;
        UpdateLightRange();
    }

    public void ReloadBattery()
    {
        CurrentLightRange = MaxLightRange;
    }

    private void LastLightBattery()
    {
        CurrentLightRange = ExtraLightRange;
    }


    private void UpdateLightRange()
    {
        CurrentLightRange -= BatteryExtinctionStep;
        _playerLightSource.range = CurrentLightRange;
    }
}
