using TMPro;
using UnityEngine;

public class UIBullets : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bulletsText;

    private void OnValidate()
    {
        if(_bulletsText == null)
            _bulletsText = GetComponent<TextMeshProUGUI>();
    }

    private void Awake()
    {
        EventManager.TransferBulletsCountEvent.AddListener(UpdateBulletsCount);
    }

    private void UpdateBulletsCount(int bulletsCount)
    {
        _bulletsText.text = bulletsCount.ToString();
    }
}
