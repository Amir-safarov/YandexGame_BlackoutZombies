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
        EventManager.TransferZombieDeathEvent.AddListener(RegistartionNewZombie);
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled)
            InitialScoreVerification();
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
        InitialScoreVerification();
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -=
           InitialScoreVerification;
    }

    public void InitialScoreVerification()
    {
        if (!PlayerPrefs.HasKey(TotalDeadZombiesCount))
            PlayerPrefs.SetInt(TotalDeadZombiesCount, 0);
        _totalDeadZombiesPP = PlayerPrefs.GetInt(TotalDeadZombiesCount);
        _totalDeadZombiesYG = YandexGame.savesData.totalDeadZombiesCount;
        int bestScore = Mathf.Max(_totalDeadZombiesYG, _totalDeadZombiesPP);

        _totalDeadZombiesPP = bestScore;
        _totalDeadZombiesYG = bestScore;
        print($"Лучший счет по убийствам на начало: {_totalDeadZombiesPP} \n и YG {_totalDeadZombiesYG}");
    }

    public int GetCurrentDeadZombiesCount()
    {
        return _currentDeadZombies;
    }

    public int GetTotalDeadZombiesCount()
    {
        return _totalDeadZombiesPP;
    }

    private void RegistartionNewZombie()
    {
        _currentDeadZombies += 1;
        //print($"Убито сейчас зомби: {_currentDeadZombies}");
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
            if (YandexGame.SDKEnabled && YandexGame.auth)
                YandexGame.savesData.totalDeadZombiesCount = _totalDeadZombiesYG;
            YandexGame.SaveProgress();
            print($"Новый лучший счет по убийствам: {_totalDeadZombiesPP}");
        }
    }

    private void ResetTotalZombiesCount()
    {
        PlayerPrefs.SetInt(TotalDeadZombiesCount, 0);
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
        YandexGame.FullscreenShow();
    }
}
