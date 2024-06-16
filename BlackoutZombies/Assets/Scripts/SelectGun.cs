using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectGun : MonoBehaviour
{
    [SerializeField] private List<Image> gunImagesList;
    [SerializeField] private List<GameObject> playerTypesList;
    [Header("To Start References")]
    [SerializeField] private GameRoot _gameRoot;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private UIVisibilityController _selectGunCanvasController;
    [SerializeField] private UIVisibilityController _gameCanvasController;
    [SerializeField] private ZombiesSpawner _zombiesSpawner;
    [SerializeField] private Image _currentGunImage;

    private int _selectedGunIndex = 0;

    private int SelectedGunIndex
    {
        get
        {
            if (_selectedGunIndex > gunImagesList.Count - 1)
                _selectedGunIndex = 0;
            if (_selectedGunIndex < 0)
                _selectedGunIndex = gunImagesList.Count - 1;
            return _selectedGunIndex;
        }
        set { _selectedGunIndex = value; }
    }

    private void Start()
    {
        ShowSelectedGun();
    }

    public void SelectNextGun(bool _toRight)
    {
        SelectedGunIndex += _toRight ? 1 : -1; 
        ShowSelectedGun();
    }

    public void StartToPlay()
    {
        _selectGunCanvasController.ObjectOff();
        _gameCanvasController.ObjectOn();
        _currentGunImage.sprite = gunImagesList[SelectedGunIndex].sprite;
        GameObject currentPlayerType = playerTypesList[SelectedGunIndex].gameObject;
        Instantiate(currentPlayerType, _spawnPoint.transform);
        _gameRoot.FindPlayerShootingObject();
        _gameRoot.OpenPlayerControll();
        _zombiesSpawner.StartZombiesSpawn();
    }

    private void ShowSelectedGun()
    {
        for (int i = 0; i < gunImagesList.Count; i++)
        {
            if (i == SelectedGunIndex)
                gunImagesList[i].enabled = true;
            else
                gunImagesList[i].enabled = false;
        }
    }
}
