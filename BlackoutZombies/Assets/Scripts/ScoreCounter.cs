using UnityEngine;
using YG;
using static DeadZombiesCounter;

public class ScoreCounter : MonoBehaviour
{
    private const string BestScore = "BestScore";

    private int _bestScorePP;
    private int _currentScore;
    private int _bestScoreYG;

    private void Awake()
    {
        InitialScoreVerification();
        EventManager.RestartSceneEvent.AddListener(ResetCurrentScore);
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent +=
            InitialScoreVerification;
    }

    private void OnDisable()
    { 
        YandexGame.GetDataEvent -=
            InitialScoreVerification;
    }

    private void InitialScoreVerification()
    {
        if (!PlayerPrefs.HasKey(BestScore))
            PlayerPrefs.SetInt(BestScore, 0);
        _bestScorePP = PlayerPrefs.GetInt(BestScore);
        EventManager.TransferScoreEvent.AddListener(RegistartionScore);
        if (!YandexGame.SDKEnabled)
            return;
        _bestScoreYG = YandexGame.savesData.score;
        print($"Лучший счет на начало: {_bestScorePP}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Insert))
            ResetBestScore();
    }

    public int GetCurrentScore()
    {
        return _currentScore;
    }

    public int GetBestScoreScore()
    {
        return _bestScorePP;
    }

    private void ResetBestScore()
    {
        PlayerPrefs.SetInt(BestScore, 0);
    }

    private void ResetCurrentScore()
    {
        _currentScore = 0;
    }

    private void RegistartionScore(ScoringStates scoringStates)
    {
        if (ScoringStates.FirstState == scoringStates)
            _currentScore += 100;
        else if (ScoringStates.SecondState == scoringStates)
            _currentScore += 150;
        else if (ScoringStates.FirstState == scoringStates)
            _currentScore += 300;
        CheckBestScore();
    }

    //tobo make set new score lisener 
    private void CheckBestScore()
    {
        if (_currentScore > _bestScorePP)
        {
            PlayerPrefs.SetInt(BestScore, _currentScore);
            _bestScorePP = PlayerPrefs.GetInt(BestScore);
            PlayerPrefs.Save();
            YandexGame.NewLeaderboardScores("Название таблицы", _bestScorePP);
            print($"Лучший обновлен на : {_bestScorePP}");
        }
    }


}
