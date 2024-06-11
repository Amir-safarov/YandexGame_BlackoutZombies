using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Accessibility;
using Random = UnityEngine.Random;

public class ZombiesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _zombiePrefabType1;
    [SerializeField] private GameObject _zombiePrefabType2;
    [SerializeField] private Transform _zombiesOnScene;
    [SerializeField] private List<GameObject> _zombiesList;
    [SerializeField] private List<Transform> _spawnPointList;
    [SerializeField] private float _spawnInterval;

    private void Awake()
    {
        EventManager.PlayerDeathEvent.AddListener(DestroyZombiesList);
    }

    private void Start()
    {
        for (int i = 0; i < _spawnPointList.Count; i++)
            _spawnPointList[i] = transform.GetChild(i);
        for (int i = 0; i < _zombiesList.Count; i++)
            _zombiesList[i] = _zombiesOnScene.GetChild(i).gameObject;
    }

    public void DestroyZombiesList()
    {
        foreach (var zombie in _zombiesList)
            zombie.SetActive(false);
        StopAllCoroutines();
    }

    public void StartZombiesSpawn() =>
        StartCoroutine(FirstWaveSpawn());
    
    private IEnumerator FirstWaveSpawn()
    {
        for (int i = 0; i < _zombiesList.Count; i++)
        {
            if (i < 2)
            {
                SpawnZombies(_zombiesList[i]);
                continue;
            }
            SpawnZombies(_zombiesList[i]);
            yield return new WaitForSecondsRealtime(_spawnInterval);
        }
        yield return null;
    }

    private void SpawnZombies(GameObject _zombie)
    {
        int spawnPointIndex = Random.Range(0, _spawnPointList.Count);
        _zombie.transform.position = _spawnPointList[spawnPointIndex].position;
        _zombie.gameObject.SetActive(true);
    }
}
