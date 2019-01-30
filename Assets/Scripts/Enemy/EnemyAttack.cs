using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject _bullet;             //префаб пули
    [SerializeField]
    private Transform _startPosition;       //откуда будут появлятся пули
    [SerializeField]
    private LayerMask _hitMask;             //какие слои мы можем повреждать
    [SerializeField]
    private float _waitTime = 2.0f;         //промежуток времени между выстрелами
    [SerializeField]
    private int _damage = 2;                //сила атаки
    
    private bool _canShoot = false;         //может ли противник стрелять

    /// <summary>
    /// Свойство для управления стрельбой извне
    /// </summary>
    public bool CanShoot
    {
        get
        {
            return _canShoot;
        }
        set
        {
            _canShoot = value;
        }
    }
    
    void Start ()
    {
        InvokeRepeating("Shoot", _waitTime, _waitTime);
	}

    /// <summary>
    /// Разрешить стрелять противнику
    /// </summary>
    public void StartShooting()
    {
        _canShoot = true;
    }

    /// <summary>
    /// Запретить стрелять противнику
    /// </summary>
    public void StopShooting()
    {
        _canShoot = false;
    }

    //TODO переделать на корутину
    /// <summary>
    /// Метод стрельбы противника
    /// </summary>
    private void Shoot()
    {
        if (_canShoot)
        {
            var b = Instantiate(_bullet, _startPosition.position, Quaternion.identity);
            BulletController bulletController = b.GetComponent<BulletController>();
            //в каком направлении будет двигаться пуля
            bulletController.Direction = gameObject.GetComponent<EnemyMove>().Direction;
            //урон, наносимый игроку
            bulletController.Damage = _damage;
            //с каким слоями будет взаимодействовать пуля
            bulletController.HitMask = _hitMask;
        }
    }
}
