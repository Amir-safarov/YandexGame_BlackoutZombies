using UnityEngine;
using YG;

public class AuthBTNClickController : MonoBehaviour
{
    [SerializeField] private UIVisibilityController _authInfoController;
    [SerializeField] private DeadZombiesCounter _deadZombiesScore;
    [SerializeField] private ScoreCounter _score;
    [SerializeField] private SceneTransfer _sceneTransfer;
    [SerializeField] private bool _authInfoOn;

    private void OnEnable()
    {
        _score.InitialScoreVerification();
        _deadZombiesScore.InitialScoreVerification();
    }

    public void AuthBtnClick()
    {
        if (_authInfoOn)
        {
            YandexGame.AuthDialog();
            _sceneTransfer.LoadNewScene();
        }
        else
            _authInfoController.ObjectOn();
        _authInfoOn = !_authInfoOn;
    }
}
