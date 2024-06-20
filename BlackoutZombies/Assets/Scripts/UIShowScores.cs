using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UIShowScores : MonoBehaviour
{
    private enum RequiredText
    {
        CurrentDeadZombies,
        TotalDeadZombies,
        CurrentScore,
        BestScore,
    }

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private DeadZombiesCounter _deadZombiesScore;
    [SerializeField] private ScoreCounter _score;
    [SerializeField] private RequiredText _requiredText;

    private void OnValidate()
    {
        if (_scoreText == null)
            _scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        ShowCurrentText();
    }

    private void ShowCurrentText()
    {
        switch (_requiredText)
        {
            case RequiredText.CurrentDeadZombies:
                _scoreText.text =
                    _deadZombiesScore.GetCurrentDeadZombiesCount().ToString();
                break;
            case RequiredText.TotalDeadZombies:
                _scoreText.text =
                    _deadZombiesScore.GetTotalDeadZombiesCount().ToString();
                break;
            case RequiredText.CurrentScore:
                _scoreText.text =
                    _score.GetCurrentScore().ToString();
                break;
            case RequiredText.BestScore:
                _scoreText.text =
                    _score.GetBestScoreScore().ToString();
                break;
        }

    }
}
