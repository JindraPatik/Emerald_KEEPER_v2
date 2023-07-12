using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoosterSpawner : MonoBehaviour
{
    private static BoosterSpawner _instance;
    private static bool _boosterSpawningEnabled;
    [SerializeField] Transform[] _spawnRangePoint;
    private float _randomSpawnRange;
    [SerializeField] GameObject _boosterPrefab;
    [SerializeField] float[] _spawnIntervalBetweenSec;
    [SerializeField] float _timeBeforeStartSpawning;



    public static BoosterSpawner Instance
    { get { return _instance; } }

    public static bool BoosterSpawningEnabled
    {
        get { return _boosterSpawningEnabled; }
        set { _boosterSpawningEnabled = value;}
    }

    private void Awake()
    {
        if(_instance != null && _instance != this)
            DestroyImmediate(_instance);
        else
            _instance = this;

        _boosterSpawningEnabled = true;
    }

    private void Start()
    {
        StartCoroutine(SpawnBoostersInRandomRange());
    }

    private float GetRandomSpawnRange()
    {
        float left = _spawnRangePoint[0].transform.position.x;
        float right = _spawnRangePoint[1].transform.position.x;
        _randomSpawnRange = UnityEngine.Random.Range(left, right);
        return _randomSpawnRange;
    }

    private void SpawnBooster()
    {
        Vector3 spawnPoint = new Vector3(GetRandomSpawnRange(), 115f, 0f);
        GameObject crystalInstance = Instantiate(_boosterPrefab, spawnPoint, Quaternion.identity);
    }

    private IEnumerator SpawnBoostersInRandomRange()
    {


        while (BoosterSpawningEnabled)
        {
            yield return new WaitForSeconds(_timeBeforeStartSpawning);
            float spawnIntervalTime = UnityEngine.Random.Range(_spawnIntervalBetweenSec[0], _spawnIntervalBetweenSec[1]);

            if (!GameManager.Instance.GameIsPaused)
            {
                SpawnBooster();
            }
            yield return new WaitForSeconds(spawnIntervalTime);
        }
    }



}
