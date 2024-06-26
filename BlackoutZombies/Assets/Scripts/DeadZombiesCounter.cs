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

    private int _currentDeadZombies;
    private int _totalDeadZombiesPP;
    private int _totalDeadZombiesYG;

    private const string TotalDeadZombiesCount = "TotalDeadZombiesCount";

    private void Awake()
    {
        InitialScoreVerification();
        EventManager.RestartSceneEvent.AddListener(ResetCurrentDeadZombies);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Insert))
            ResetTotalZombiesCount();
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
        if (!YandexGame.SDKEnabled)
            return;
        _totalDeadZombiesYG = YandexGame.savesData.totalDeadZombiesCount;
        int bestScore = Mathf.Max(_totalDeadZombiesYG, _totalDeadZombiesPP);

        _totalDeadZombiesPP = bestScore;
        _totalDeadZombiesYG = bestScore;
        EventManager.TransferZombieDeathEvent.AddListener(RegistartionNewZombie);
        print($"������ ���� �� ��������� �� ������: {_totalDeadZombiesPP} \n � YG {_totalDeadZombiesYG}");
    }

    public int GetCurrentDeadZombiesCount()
    {
        return _currentDeadZombies;
    }

    public int GetTotalDeadZombiesCount()
    {
        return _totalDeadZombiesYG;
    }

    private void RegistartionNewZombie()
    {
        _currentDeadZombies += 1;
        print($"����� ������ �����: {_currentDeadZombies}");
        TransferScoringState();
        CheckDeadZombiesCount();
    }

    private void TransferScoringState()
    {
        if (_currentDeadZombies <= 10)
            EventManager.InvokeTransferScore(ScoringStates.FirstState);
        else if (_currentDeadZombies > 10 && _currentDeadZombies <= 30)
            EventManager.InvokeTransferScore(ScoringStates.SecondState);
        else
            EventManager.InvokeTransferScore(ScoringStates.ThirdState);
    }

    private void CheckDeadZombiesCount()
    {
        if (_currentDeadZombies > _totalDeadZombiesPP)
        {
            PlayerPrefs.SetInt(TotalDeadZombiesCount, _currentDeadZombies);
            _totalDeadZombiesPP = PlayerPrefs.GetInt(TotalDeadZombiesCount);
            PlayerPrefs.Save();
            _totalDeadZombiesYG = _totalDeadZombiesPP;
            YandexGame.savesData.score = _totalDeadZombiesYG;
            YandexGame.SaveProgress();
            print($"����� ������ ���� �� ���������: {_totalDeadZombiesPP}");
        }
    }

    private void ResetTotalZombiesCount()
    {
        PlayerPrefs.SetInt(TotalDeadZombiesCount,0);
        _totalDeadZombiesPP = PlayerPrefs.GetInt(TotalDeadZombiesCount);
        _totalDeadZombiesYG = _totalDeadZombiesPP;
        YandexGame.savesData.score = _totalDeadZombiesYG;
        YandexGame.SaveProgress();
    }

    private void ResetCurrentDeadZombies(bool isReviev)
    {
        if (isReviev)
            return;
        _currentDeadZombies = 0;
    }
}
