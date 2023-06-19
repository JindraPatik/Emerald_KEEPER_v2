using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CrystalSpawner : MonoBehaviour
{
    private static CrystalSpawner _instance;
    private float _randomSpawnRange;
    [SerializeField] GameObject _crystalPrefab;
    [SerializeField] Transform[] _spawnRangePoint;
    [SerializeField] float[] _spawnIntervalBetween; 
    private bool _crystalSpawningEnabled;
    [SerializeField] float minCrystalSize;
    [SerializeField] float maxCrystalSize;
    private Player _player;
    GameManager _gameManager;


    public static CrystalSpawner Instance
    {
        get {return _instance; }
    }

    public bool CrystalSpawningEnabled
    {
        get { return _crystalSpawningEnabled; }
        set { _crystalSpawningEnabled = value; }
    }

    
    private void Awake() 
        {
            _player = FindAnyObjectByType<Player>();
            // Singleton
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }

            _instance = this;

        }

    private void OnEnable() 
    {
        _crystalSpawningEnabled = enabled;    
        _player.OnPlayerDies += StopAllCoroutines;
    }

    private void Start() 
        {
            StartCoroutine (SpawnCrystalInRandomRange());
        }


    //Generate random SpawnPoint on X axis
    private float GetRandomSpawnRange()
        {
            float left = _spawnRangePoint[0].transform.position.x;
            float right = _spawnRangePoint[1].transform.position.x;
            _randomSpawnRange = UnityEngine.Random.Range(left, right);
            return _randomSpawnRange;
        }
   
    private void SpawnCrystals()
        {
            Vector3 spawnPoint = new Vector3 (GetRandomSpawnRange(), 2f, 0f);
            GameObject crystalInstance = Instantiate(_crystalPrefab, spawnPoint, GenerateRandomQuaternionInRange());
            crystalInstance.transform.localScale = GetRandomScale();
        }

    private Quaternion GenerateRandomQuaternionInRange()
    {
        float randomAngle = Random.Range(0f, 360f);
        return Quaternion.Euler(0f, randomAngle, 0f);
    }    

    private Vector3 GetRandomScale()
    {
        float randomScale = Random.Range(minCrystalSize, maxCrystalSize);
        return new Vector3 (randomScale, randomScale, randomScale);
    }

    private IEnumerator SpawnCrystalInRandomRange()
    {

        while (!GameManager.Instance.GameIsPaused)
        {
            float spawnIntervalTime = UnityEngine.Random.Range(_spawnIntervalBetween[0], _spawnIntervalBetween[1]);

            if (CrystalSpawningEnabled)
            {
                SpawnCrystals();
            }
            yield return new WaitForSeconds(spawnIntervalTime);
        }
    }


    private void OnDisable() 
    {
        _crystalSpawningEnabled = false;
        _player.OnPlayerDies -= StopAllCoroutines;
        StopAllCoroutines();
    }
    

}
