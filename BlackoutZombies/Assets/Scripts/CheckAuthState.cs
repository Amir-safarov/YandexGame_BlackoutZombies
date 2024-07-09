using UnityEngine;
using YG;

public class CheckAuthState : MonoBehaviour
{
    [SerializeField] private UIVisibilityController _aboutAuthVisibilityController;
    [SerializeField] private UIVisibilityController _authVisibilityController;

    private void OnEnable()
    {
        CheckAuth();
    }

    private void OnDisable()
    {
        CheckAuth();
    }

    public void CheckAuth()
    {
        if (YandexGame.auth)
        {
            _authVisibilityController.ObjectOff();
        }
        else
        {
            _authVisibilityController.ObjectOn();
        }

    }
}
