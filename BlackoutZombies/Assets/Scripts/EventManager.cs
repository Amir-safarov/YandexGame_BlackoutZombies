using UnityEngine;
using UnityEngine.Events;
using static DeadZombiesCounter;

public class EventManager : MonoBehaviour
{
    public static UnityEvent PlayerDeathEvent = new UnityEvent();
    public static UnityEvent<int> TransferHeartCountEvent = new UnityEvent<int>();
    public static UnityEvent<int> TransferBulletsCountEvent = new UnityEvent<int>();
    public static UnityEvent<ScoringStates> TransferScoreEvent = new UnityEvent<ScoringStates>();
    public static UnityEvent TransferZombieDeathEvent = new UnityEvent();
    public static UnityEvent RestartSceneEvent = new UnityEvent();

    public static void InvokePlayersDeath() =>
        PlayerDeathEvent.Invoke();

    public static void InvokeTransferHeart(int heartCount) =>
        TransferHeartCountEvent.Invoke(heartCount);

    public static void InvokeTransferBullets(int bulletsCount) =>
        TransferBulletsCountEvent.Invoke(bulletsCount);

    public static void InvokeTransferScore(ScoringStates scoringState) =>
        TransferScoreEvent.Invoke(scoringState);

    public static void InvokeTransferZombieDeath() =>
      TransferZombieDeathEvent.Invoke();

    public static void InvokeRestartScene() =>
      RestartSceneEvent.Invoke();
}
