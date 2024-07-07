using UnityEngine;
using YG;

public class AuthBTNClickController : MonoBehaviour
{
    [SerializeField] private UIVisibilityController _authInfoController;
    [SerializeField] private DeadZombiesCounter _deadZombiesScore;
    [SerializeField] private ScoreCounter _score;
    [SerializeField] private bool _authInfoOn;

    public void AuthBtnClick()
    {
        if (_authInfoOn)
        {
            YandexGame.AuthDialog();
            _score.InitialScoreVerification();
            _deadZombiesScore.InitialScoreVerification();
        }
        else
            _authInfoController.ObjectOn();
        _authInfoOn = !_authInfoOn;
    }
}
