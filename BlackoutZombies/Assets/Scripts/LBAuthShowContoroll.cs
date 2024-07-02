using System;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public class LBAuthShowContoroll : MonoBehaviour
{
    [SerializeField] private UIVisibilityController _authController;
    [SerializeField] private UIVisibilityController _LBController;

    private void OnEnable()
    {
        ShowCurrentButton();
    }

    private void ShowCurrentButton()
    {
        if(YandexGame.auth)
        {
            _authController.ObjectOff();
            _LBController.ObjectOn();
        }
        else
        {
            _authController.ObjectOn();
            _LBController.ObjectOff();
        }
    }
}
