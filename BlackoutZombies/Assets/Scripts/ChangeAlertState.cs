using UnityEngine;
using YG;

public class ChangeAlertState : MonoBehaviour
{
    [SerializeField] private UIVisibilityController _alertTextController;
    private bool _alertOpen;
    public void AlertBtnClick()
    {
        if (_alertOpen)
            _alertTextController.ObjectOff();
        else
            _alertTextController.ObjectOn();
        _alertOpen = !_alertOpen;
    }
}
