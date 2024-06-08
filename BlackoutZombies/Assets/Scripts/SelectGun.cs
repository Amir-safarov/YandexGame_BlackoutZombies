using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectGun : MonoBehaviour
{
    [SerializeField] private List<Image> gunsList;
    [SerializeField] private GameRoot _gameRoot;

    private int _selectedGunIndex = 0;

    private int SelectedGunIndex
    {
        get
        {
            if (_selectedGunIndex > gunsList.Count - 1)
                _selectedGunIndex = 0;
            if (_selectedGunIndex < 0)
                _selectedGunIndex = gunsList.Count - 1;
            return _selectedGunIndex;
        }
        set { _selectedGunIndex = value; }
    }

    private void Start()
    {
        ShowSelectedGun();
    }


    public void SelectNextRightGun()
    {
        SelectedGunIndex++;
        ShowSelectedGun();
    }
    public void SelectNextLeftGun()
    {
        SelectedGunIndex--;
        ShowSelectedGun();
    }

    private void ShowSelectedGun()
    {
        for (int i = 0; i < gunsList.Count; i++)
        {
            if (i == SelectedGunIndex)
                gunsList[i].enabled = true;
            else
                gunsList[i].enabled = false;
        }
    }
}
