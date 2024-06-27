using System;
using System.Collections.Generic;
using UnityEngine;

public class RequarementToUse : MonoBehaviour
{
    [SerializeField] private GameObject _startBTN;
    [SerializeField] private DeadZombiesCounter _zombiesCounter;
    [SerializeField] private List<RequarementToUseObject> _listOfRequirementText;

    private void Awake()
    {
        EventManager.TransferTotalDeadZombieCountEvent.AddListener(CheckEqupAbility);
        
    }

    private void OnEnable()
    {
        FillList();
    }
   
    private void OnValidate()
    {
        if (_startBTN == null)
            _startBTN = GetComponent<GameObject>();
    }

    public void CheckEqupAbility(int selectedGunIndex)
    {
        RequarementToUseObject requirementTextToSelectedGun = _listOfRequirementText[selectedGunIndex]; 
        CloseList();
        ObjectOn(requirementTextToSelectedGun.gameObject);
        ObjectOff(_startBTN);
        int totalDeathZombies = _zombiesCounter.GetTotalDeadZombiesCount();
        if (totalDeathZombies >= requirementTextToSelectedGun.GetRequirementDeadZombiesCount())
        {
            ObjectOn(_startBTN);
            ObjectOff(requirementTextToSelectedGun.gameObject);
        }
  
    }

    private void CloseList()
    {
        foreach (var item in _listOfRequirementText)
            ObjectOff(item.gameObject);
    }

    private void FillList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            RequarementToUseObject childObj = transform.GetChild(i).GetComponent<RequarementToUseObject>();
            _listOfRequirementText.Add(childObj);
            print($"Добавлен {childObj}");
        }
    }

    private void ObjectOff(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void ObjectOn(GameObject obj)
    {
        obj.SetActive(true);
    }
}
