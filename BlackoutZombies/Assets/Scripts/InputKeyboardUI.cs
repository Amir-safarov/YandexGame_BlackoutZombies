using UnityEngine;

public class InputKeyboardUI : MonoBehaviour
{
    [SerializeField] private SceneTransfer _loadSceneController;
    [SerializeField] private UIVisibilityController _restartCanvasController;
    [SerializeField] private UIVisibilityController _gameCanvasController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            EventManager.InvokeRestartScene();
            _restartCanvasController.ObjectOff();
            _gameCanvasController.ObjectOn();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            _loadSceneController.LoadNewScene();
    }
}
