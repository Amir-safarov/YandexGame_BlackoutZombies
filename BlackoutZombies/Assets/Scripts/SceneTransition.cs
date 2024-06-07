using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    private enum ScenesCollection
    {
        MainMenuScene,
        GameScene
    };

    [SerializeField] private ScenesCollection selectedScene;

    public  void LoadNewScene() =>
        SceneManager.LoadScene(selectedScene.ToString());
}
