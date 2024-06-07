using UnityEngine;

public class InputKeyboardUI : MonoBehaviour
{
    [SerializeField] private SceneTransfer _loadSceneController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Debug.Log("Like the restart");
        if (Input.GetKeyDown(KeyCode.Escape))
            _loadSceneController.LoadNewScene();
    }
}
