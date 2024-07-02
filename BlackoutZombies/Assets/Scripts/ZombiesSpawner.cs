using System;
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
            if (zombieSpawnCounter >= _zombiesList.Count - 1)
                zombieSpawnCounter = 0;
            return zombieSpawnCounter;
        }
        set { zombieSpawnCounter = value; }
    }

    [SerializeField] private GameObject _zombiePrefabType1;
    [SerializeField] private GameObject _zombiePrefabType2;
    [SerializeField] private Transform _zombiesOnScene;
    [SerializeField] private BoxCollider2D _spawnCollider;
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

    public void StartZombiesSpawn(bool isRevive = false) =>
        StartCoroutine(FirstWaveSpawn());

    private void DisableZombies(bool isRevive)
    {
        foreach (var zombie in _zombiesList)
            zombie.gameObject.SetActive(false);
    }

    private IEnumerator FirstWaveSpawn()
    {
        while (true)
        {
            if (_zombiesList[ZombieSpawnCounter].gameObject.activeSelf)
                continue;
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
        _zombie.transform.position = GetSpawnPoint();
        _zombie.gameObject.SetActive(true);
        _zombie.OpenZombieMove();
    }

    private Vector3 GetSpawnPoint()
    {
        while (true)
        {
            int spawnPointIndex = Random.Range(0, _spawnPointList.Count);
            Vector3 spawnPos = _spawnPointList[spawnPointIndex].position;
            if (_spawnCollider.bounds.Contains(spawnPos))
                return spawnPos;
            else continue;
        }
    }
}
