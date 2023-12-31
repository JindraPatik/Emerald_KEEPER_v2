using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EK.Crystal;
using Unity.VisualScripting;
using UnityEngine;

public class Harvester : UnitCommon, ICollector, IDeath
{
    #region Variables
        
    private bool _isLoaded;
    private float _crystalCollected;
    private float _harvesterHeightMovement = 15f;
    private Crystal _crystal;
    private Player _player;
    private float _current = 0f;
    private float _endTime = 1f;
    private bool _isMovingUp;
    private float _moveUpSpeed = 2f;
    private Vector3 goalRotation;
    private Vector3 _unitCurrentPosition;
    Harvester_Sounds _harvester_Sounds;

    public event Action OnDeliverCrysral;
    public event Action OnCrystalCollect;

    [SerializeField] AnimationCurve _curve;
    [SerializeField] GameObject _deliveryCrystalParticles;
    public float CrystalValue
    {
        get { return _crystalCollected; }
    }
    public bool IsLoaded
    {
        get { return _isLoaded; }
        set { _isLoaded = value; }
    }

    public bool IsMovingUp
    {
        get { return _isMovingUp; }
        set { _isMovingUp = value; }
    }

    public Vector3 UnitCurrentPosition
    {
        get { return _unitCurrentPosition; }
        set { _unitCurrentPosition = value; }
    }
    #endregion
    
    #region InitialMethods
    public override void Awake() 
    {
        base.Awake();
        IsMoving = true;
        _isLoaded = false;
        _isMovingUp = false;
        FlipInitialRotation();
        _harvester_Sounds = GetComponent<Harvester_Sounds>();
        
    }
    public override void OnEnable()
    {
        base.OnEnable();
        OnDeliverCrysral += PlayDeliveryParticles;
        OnCrystalCollect += PlayCollectCrystalSound;
    }
    public override void OnDisable()
    {
        base.OnDisable();
    }
    public override void Start()
    {
        base.Start();
        SetGoalRotationForFactions();
    }
    public virtual void Update()
    {
        if (IsMoving)
        {
            Move(Speed);
        }
        else
        {
            Move(SetSpeedToZero());
                if (_isMovingUp)
                {
                    MoveUp(GetCurrent());
                    RotateOneEighty(GetCurrent());
                }
        }
        // current je timer od 0 do 1
        if (_current >= _endTime)
        {
            IsMoving = true;
            _isMovingUp = false;
            _current = 0;
            MoveAgain();
        }
    }
   public void SetGoalRotationForFactions()
    {
        if (MyFaction == Faction.Player)
        {
            goalRotation = new Vector3(transform.rotation.x, transform.rotation.y - 180f, transform.rotation.z);
        }
        else
        {
            goalRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        }
    }
    #endregion

    #region Interface Methods
    public virtual void Collect()
    {
        _isLoaded = true;
        OnCrystalCollect?.Invoke();
        GetCrystalValue(_crystal);
        StopUnitMovement();
        _isMovingUp = true;
        Speed = MoveDirectionSwitch();
        _crystal.Die();
    }
    #endregion
  
    #region Methods
    public bool unLoad()
    {
        _isLoaded = false;
        return _isLoaded;
    }
    public void GetCrystalValue(Crystal crystal)
    {
        _crystalCollected = crystal.CrystalValue;
    }
    public override void OnTriggerEnter(Collider other) 
    {
        UnitContact(other);
        //Pokud Harvester narazi do crystalu
        if (other.gameObject.TryGetComponent<Crystal>(out _crystal) && !_isLoaded)
        {
            var crystalParticle = _crystal.GetComponent<CrystalParticleSpawner>();
            OnCrystalCollect += crystalParticle.PlayCrystalDeathparticles;
            Collect();
            OnCrystalCollect -= crystalParticle.PlayCrystalDeathparticles;
        }
        //pokud je harvester nalozeny a narazi do hrace
        if (other.gameObject.TryGetComponent<Player>(out _player) && _isLoaded)
        {
            _unitCurrentPosition = transform.position;
            unLoad();
            OnDeliverCrysral?.Invoke();
            Destroy(gameObject);

            OnDeliverCrysral -= PlayDeliveryParticles;
        }
    }
    // Timer
    private float GetCurrent()
    {
        return Mathf.MoveTowards(_current, _endTime, _moveUpSpeed * Time.deltaTime);
    }
    //  Otoci harvestera kolem osy
    private void RotateOneEighty(float current)
    {
        _current = current;
        transform.rotation = Quaternion.Lerp( Quaternion.identity, Quaternion.Euler(goalRotation), _curve.Evaluate(_current));
    }
    //vyjede s harvesterem nahoru
    private void MoveUp(float current)
    {
        _current = current;
        Vector3 moveUpDestination = new Vector3 (transform.position.x,_harvesterHeightMovement, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, moveUpDestination, _current);
    }
    #endregion

    #region Effects
    private void PlayDeliveryParticles()
    {
        Vector3 _deliveryParticlesSpawnPoint = new Vector3(_unitCurrentPosition.x, _unitCurrentPosition.y + 30, _unitCurrentPosition.z);
        GameObject particleInstance;
        particleInstance = Instantiate(_deliveryCrystalParticles, _deliveryParticlesSpawnPoint, Quaternion.identity);
        Destroy(particleInstance, 1.5f);
    }
    private void PlayCollectCrystalSound()
    {
        _harvester_Sounds.PlayCollectCrystalSound();
    }
    #endregion;

}
