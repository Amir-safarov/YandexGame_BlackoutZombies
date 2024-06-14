using System;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent PlayerDeathEvent = new UnityEvent();
    public static UnityEvent<int> TransferHeartCountEvent = new UnityEvent<int>();
    public static UnityEvent<int> TransferBulletsCountEvent = new UnityEvent<int>();

    public static void InvokePlayersDeath() =>
        PlayerDeathEvent.Invoke();

    public static void InvokeTransferHeart(int heartCount) =>
        TransferHeartCountEvent.Invoke(heartCount);

    public static void InvokeTransferBullets(int bulletsCount) =>
        TransferBulletsCountEvent.Invoke(bulletsCount);
}
