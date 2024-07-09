using UnityEngine;
using YG;

public class ReviveInvoke : MonoBehaviour
{
    [SerializeField] private InputKeyboardUI _inputKeyboard;

    private void OnEnable() => YandexGame.RewardVideoEvent += RevivePlayer;

    private void OnDisable() => YandexGame.RewardVideoEvent -= RevivePlayer;

    private void RevivePlayer(int id)
    {
        _inputKeyboard.RestartScene(true);
    }

    public void OpenRewardAd(int id)
    {
        YandexGame.RewVideoShow(id);
    }
}
