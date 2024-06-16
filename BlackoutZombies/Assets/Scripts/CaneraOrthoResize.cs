using Cinemachine;
using UnityEngine;

public class CaneraOrthoResize : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cam;

    private void Awake()
    {
        EventManager.PlayerDeathEvent.AddListener(ResizeCam);
    }

    private void ResizeCam()
    {
        _cam.m_Lens.OrthographicSize = 10;
    }
}
