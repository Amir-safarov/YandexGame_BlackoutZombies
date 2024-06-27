using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveInvoke : MonoBehaviour
{
    [SerializeField] private InputKeyboardUI _inputKeyboard;
    public void RevivePlayer()
    {
        _inputKeyboard.RestartScene(true);
    }
}
