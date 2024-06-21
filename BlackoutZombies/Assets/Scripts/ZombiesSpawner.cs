using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombiesSpawner : MonoBehaviour
{
    public int ZombieSpawnCounter
    {
        get
        {
            if (zombieSpawnCounter >= _zombiesList.Count)
                zombieSpawnCounter = 0;
            return zombieSpawnCounter;
        }
        set { zombieSpawnCounter = value; }
    }

    [SerializeField] private GameObject _zombiePrefabType1;
    [SerializeField] private GameObject _zombiePrefabType2;
    [SerializeField] private Transform _zombiesOnScene;
    [SerializeField] private List<ZombieMovement> _zombiesList;
    [SerializeField] private List<Transform> _spawnPointList;
    [SerializeField] private float _spawnInterval;

    int zombieSpawnCounter = 0;

    private void Awake()
    {
        EventManager.PlayerDeathEvent.AddListener(DestroyZombiesList);
        EventManager.RestartSceneEvent.AddListener(StartZombiesSpawn);
        EventManager.RestartSceneEvent.AddListener(DisableZombies);
    }

    private void Start()
    {
        for (int i = 0; i < _spawnPointList.Count; i++)
            _spawnPointList[i] = transform.GetChild(i);
        for (int i = 0; i < _zombiesList.Count; i++)
            _zombiesList[i] = _zombiesOnScene.GetChild(i).GetComponent<ZombieMovement>();
    }

    public void DestroyZombiesList()
    {
        foreach (var zombie in _zombiesList)
            zombie.CloseZombieMove();
        StopAllCoroutines();
    }

    public void StartZombiesSpawn() =>
        StartCoroutine(FirstWaveSpawn());

    private void DisableZombies()
    {
        foreach (var zombie in _zombiesList)
            zombie.gameObject.SetActive(false);
    }

    private IEnumerator FirstWaveSpawn()
    {
        while (true)
        {
            if (_zombiesList[ZombieSpawnCounter].gameObject.activeSelf)
                yield return null;
            else
            {
                SpawnZombies(_zombiesList[ZombieSpawnCounter]);
                ZombieSpawnCounter++;
                yield return new WaitForSeconds(_spawnInterval);
            }
        }
    }

    private void SpawnZombies(ZombieMovement _zombie)
    {
        int spawnPointIndex = Random.Range(0, _spawnPointList.Count);
        _zombie.transform.position = _spawnPointList[spawnPointIndex].position;
        _zombie.OpenZombieMove();
        _zombie.gameObject.SetActive(true);
    }
}
