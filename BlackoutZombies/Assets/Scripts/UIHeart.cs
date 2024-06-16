using System;
using System.Collections.Generic;
using UnityEngine;

public class UIHeart : MonoBehaviour
{
    [SerializeField] private UIVisibilityController _parentCanvas;
    [SerializeField] private List<GameObject> _heartsList;

    private void Awake()
    {
        EventManager.TransferHeartCountEvent.AddListener(UpdateHeartsvisibility);
        EventManager.PlayerDeathEvent.AddListener(CloseParentCanvas);
    }

    private void UpdateHeartsvisibility(int _heartCount)
    {
        if (_heartCount <= 0)
            return;
        HeartsListOff();
        _heartsList[_heartCount - 1].SetActive(true);
    }

    private void HeartsListOff()
    {
        foreach (var item in _heartsList)
            item.SetActive(false);
    }

    private void CloseParentCanvas() =>
        _parentCanvas.ObjectOff();
}
