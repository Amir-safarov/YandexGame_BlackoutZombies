using System;
using UnityEngine;
using YG;

public class DeadZombiesCounter : MonoBehaviour
{
    public enum ScoringStates
    {
        FirstState,
        SecondState,
        ThirdState,
    }

    private float _currentDeadZombies;
    private float _totalDeadZombiesPP;
    private float _totalDeadZombiesYG;

    private const string TotalDeadZombiesCount = "TotalDeadZombiesCount";

    private void Awake()
    {
        InitialScoreVerification();
        EventManager.RestartSceneEvent.AddListener(ResetCurrentDeadZombies);
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent +=
           InitialScoreVerification;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent +=
           InitialScoreVerification;
    }

    private void InitialScoreVerification()
    {
        if (!PlayerPrefs.HasKey(TotalDeadZombiesCount))
            PlayerPrefs.SetInt(TotalDeadZombiesCount, 0);
        _totalDeadZombiesPP = PlayerPrefs.GetInt(TotalDeadZombiesCount);
        print($"Лучший счет по убийствам на начало: {_totalDeadZombiesPP}");
        EventManager.TransferZombieDeathEvent.AddListener(RegistartionNewZombie);
        if (!YandexGame.SDKEnabled)
            return;
        _totalDeadZombiesYG = YandexGame.savesData.totalDeadZombiesCount;
    }

    public float GetCurrentDeadZombiesCount()
    {
        return _currentDeadZombies;
    }

    public float GetTotalDeadZombiesCount()
    {
        return _totalDeadZombiesPP;
    }

    private void RegistartionNewZombie()
    {
        _currentDeadZombies += 0.5f;
        print($"Убито сейчас зомби: {_currentDeadZombies}");
        TransferScoringState();
        CheckDeadZombiesCount();
    }

    private void TransferScoringState()
    {
        if (_currentDeadZombies < 10)
            EventManager.InvokeTransferScore(ScoringStates.FirstState);
        else if (_currentDeadZombies > 10 && _currentDeadZombies < 30)
            EventManager.InvokeTransferScore(ScoringStates.SecondState);
        else
            EventManager.InvokeTransferScore(ScoringStates.ThirdState);
    }

    private void CheckDeadZombiesCount()
    {
        if (_currentDeadZombies > _totalDeadZombiesPP)
        {
            PlayerPrefs.SetFloat(TotalDeadZombiesCount, _currentDeadZombies);
            _totalDeadZombiesPP = PlayerPrefs.GetFloat(TotalDeadZombiesCount);
            print($"Новый лучший счет по убийствам: {_totalDeadZombiesPP}");
        }
    }

    private void ResetCurrentDeadZombies(bool isReviev)
    {
        if (isReviev)
            return;
        _currentDeadZombies = 0;
    }
}
