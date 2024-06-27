using UnityEngine;

public class InputKeyboardUI : MonoBehaviour
{
    [SerializeField] private SceneTransfer _loadSceneController;
    [SerializeField] private UIVisibilityController _restartCanvasController;
    [SerializeField] private UIVisibilityController _gameCanvasController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RestartScene();
        if (Input.GetKeyDown(KeyCode.Escape))
            _loadSceneController.LoadNewScene();
    }

    public void RestartScene(bool isRevive = false)
    {
        EventManager.InvokeRestartScene(isRevive);
        _restartCanvasController.ObjectOff();
        _gameCanvasController.ObjectOn();
    }
}
