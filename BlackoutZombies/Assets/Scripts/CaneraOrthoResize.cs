using Cinemachine;
using UnityEngine;

public class CaneraOrthoResize : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cam;

    private const float DeathCamSize = 10;
    private const float DeafaultCamSize = 6.1f;

    private void Awake()
    {
        EventManager.PlayerDeathEvent.AddListener(ResizeCamAfterDeafth);
        EventManager.RestartSceneEvent.AddListener(RestartResizeCam);
    }

    private void ResizeCamAfterDeafth()
    {
        _cam.m_Lens.OrthographicSize = DeathCamSize;
    }

    private void RestartResizeCam(bool isReviev)
    {
        _cam.m_Lens.OrthographicSize = DeafaultCamSize;
    }
}
