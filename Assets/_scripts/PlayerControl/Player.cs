using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : PlayerController, IDeath

{
#region Variables

public Unit Unit;
public UnitCommon UnitCommon;
private Transform _spawnPoint;
private int _unitIndex;
private bool _hasEnoughResources; 
private Harvester harvester;
private Unit unit;
private float _maxHealth;

public GameObject SpawnedUnit;

[SerializeField] Unit.Faction _playerFaction;
[SerializeField] TMP_Text _resourcesTXT;
[SerializeField] TMP_Text _notEnoughResourcesText;
[SerializeField] TMP_Text _myHealth;
[SerializeField] Image _myHealthBar;

public event Action OnPlayerDies;
public Action OnPlayerHit;
public Action OnCrysralDelivery;

public Unit.Faction PlayerFaction
{
    get { return _playerFaction; }
}

#endregion

public virtual void Awake() 
{
    Unit = Prefabs[_unitIndex].GetComponent<Unit>();
    UnitCommon = Prefabs[_unitIndex].GetComponent<UnitCommon>();
    _maxHealth = Health;
    IsDead = false;
}
private void OnEnable() 
{
    OnPlayerDies += Die;
}

private void OnDisable() 
{
    OnPlayerDies -= Die;     
}


public virtual void Start() 
{
    _notEnoughResourcesText.enabled = false;
}

void FixedUpdate()
    {
        if(!IsDead)
        {
            if (!GameManager.Instance.GameIsPaused)
            {
                _resourcesTXT.text = ((int)ResourcesValue).ToString();
                ResourcesValue += ResourcesIncreasedPerSecond * Time.fixedDeltaTime; 
            }
        }
    }

private void LateUpdate() 
{
    _myHealth.text = (((int)Health).ToString());
}

public bool HasEnoughResources(float resourcesValue, Unit unitIndexed) => _hasEnoughResources = resourcesValue >= unitIndexed.Price ? true : false;


private void PayforUnit(float price)
{
    ResourcesValue -= price;
}

public void DeployUnit(int unitIndex)
{
    Unit = Prefabs[unitIndex].GetComponent<Unit>();
    _spawnPoint = Unit.SpawnPoint;

    if(!IsDead && HasEnoughResources(ResourcesValue, Unit) && !GameManager.Instance.GameIsPaused)
    {
        PayforUnit(Unit.Price);
        SpawnUnit(unitIndex);
        _notEnoughResourcesText.enabled = false;
        }
    else
    {
        _notEnoughResourcesText.enabled = true;
        StartCoroutine(HideNotEnoughResourcesText());
    }
}

IEnumerator HideNotEnoughResourcesText()
    {
        yield return new WaitForSeconds(1f);
        _notEnoughResourcesText.enabled = false;
    }



private void UpdateHealthBar(float maxHealth, float health)
{
    _myHealthBar.fillAmount = health / maxHealth;
}

public override void SpawnUnit(int unitIndex)
    {
        Vector3 spawn = new Vector3 (_spawnPoint.transform.position.x, _spawnPoint.transform.position.y, _spawnPoint.transform.position.z);
        SpawnedUnit = Instantiate(Prefabs[unitIndex], spawn, Quaternion.identity);
    }

private bool bIsPressed = false;
private bool bIsReadyToAction = true;

    public void PressKey(KeyCode key, Action action)
    {
         if(Input.GetKey(key))
        {
            bIsPressed = true;
        }
        if(Input.GetKeyUp(key))
        {
            bIsPressed = false;
            bIsReadyToAction = true;
        }
        if(bIsPressed && bIsReadyToAction)
        {
            action.Invoke();
            bIsReadyToAction = false;
        }
    }    
private void OnTriggerEnter(Collider other) 
{
    // pokud koliduje s harvesterem, přičti crystal
    if(other.gameObject.TryGetComponent<Harvester>(out harvester))

    {     
        if (harvester.IsLoaded)
        {
            ResourcesValue += harvester.CrystalValue;
            OnCrysralDelivery?.Invoke();
        }
    }

    if (other.gameObject.TryGetComponent<Unit>(out unit))
    {
        //pokud má jednotka jinou frakci
        if (unit.MyFaction != _playerFaction)
            {
                Health -= unit.Strenght;
                UpdateHealthBar(_maxHealth, Health);
                OnPlayerHit?.Invoke();
                Destroy(unit.gameObject);
                PlayerDeathCondition();
            }
        }
}
private void PlayerDeathCondition()
    {
        if (Health <= 0)
        {
            OnPlayerDies?.Invoke();
        }
    }
public void Die()
    {
        IsDead = true;
    }
}