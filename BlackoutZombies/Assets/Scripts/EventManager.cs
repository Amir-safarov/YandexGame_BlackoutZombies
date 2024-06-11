using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent PlayerDeathEvent = new UnityEvent();

    public static void InvokePlayersDeath() =>
        PlayerDeathEvent.Invoke();
}
