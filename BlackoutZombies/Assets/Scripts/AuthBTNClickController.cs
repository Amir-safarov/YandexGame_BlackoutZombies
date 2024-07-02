using UnityEngine;
using YG;

public class AuthBTNClickController : MonoBehaviour
{
    [SerializeField] private UIVisibilityController _authInfoController;
    [SerializeField] private bool _authInfoOn;

    public void AuthBtnClick()
    {
        if (_authInfoOn)
            YandexGame.AuthDialog();
        else
        {
            print("kdsnfknsjfnj");
            _authInfoController.ObjectOn();
        }
        _authInfoOn = !_authInfoOn;
    }
}
