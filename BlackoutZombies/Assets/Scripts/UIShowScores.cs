using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShowScores : MonoBehaviour
{
    private enum RequiredText
    {
        CurrentDeadZombies,
        TotalDeadZombies,
        CurrentScore,
        BestScore,
    }

    [SerializeField] private TextMeshProUGUI _scoreTextTMP;
    [SerializeField] private DeadZombiesCounter _deadZombiesScore;
    [SerializeField] private ScoreCounter _score;
    [SerializeField] private RequiredText _requiredText;


    private void OnValidate()
    {
        if (_scoreTextTMP == null)
            _scoreTextTMP = GetComponent<TextMeshProUGUI>();
    }

    private void Awake()
    {
        EventManager.TransferZombieDeathEvent.AddListener(ShowCurrentTextTMP);
    }

    private void OnEnable()
    {
        ShowCurrentTextTMP();
    }

    private void ShowCurrentTextTMP()
    {
        switch (_requiredText)
        {
            case RequiredText.CurrentDeadZombies:
                _scoreTextTMP.text =
                    _deadZombiesScore.GetCurrentDeadZombiesCount().ToString();
                break;
            case RequiredText.TotalDeadZombies:
                _scoreTextTMP.text =
                    _deadZombiesScore.GetTotalDeadZombiesCount().ToString();
                break;
            case RequiredText.CurrentScore:
                _scoreTextTMP.text =
                    _score.GetCurrentScore().ToString();
                break;
            case RequiredText.BestScore:
                _scoreTextTMP.text =
                    _score.GetBestScoreScore().ToString();
                break;
        }
    }
}
