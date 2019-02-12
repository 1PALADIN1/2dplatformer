using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerStrike : MonoBehaviour
{
    [SerializeField]
    private GameObject _bullet;         //пуля
    [SerializeField]
    private GameObject _mina;           //мина
    [SerializeField]
    private Transform _startPosition;   //откуда будут появлятся пули
    [SerializeField]
    private LayerMask _hitMask;         //какие слои мы можем повреждать
    [SerializeField]
    private AudioClip _shotAudio;       //звук выстрела
    [SerializeField]
    private GameObject _shotParticleGO;
    [SerializeField]
    private bool _mobileControl = true; //управление с мобилки?

    private AudioManager _audioManager; //менеджер звуков
    private ParticleSystem _shotParticle;
    private PlayerMove _playerMove;     //компонент, отвечающий за перемещение игрока

    private void Start()
    {
        //получаем аудиоменеджер
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        //получаем компонент частиц
        if (_shotParticleGO != null)
            _shotParticle = _shotParticleGO.GetComponent<ParticleSystem>();

        _playerMove = GetComponent<PlayerMove>();
    }

    private void Update ()
    {
        //выстрелы
        bool isShooting = false;

        if (_mobileControl) isShooting = CrossPlatformInputManager.GetButtonDown("Fire");
        else
            isShooting = Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0);


        if (isShooting)
        {
            if (_bullet != null && !_playerMove.BlockControl)
            {
                var b = Instantiate(_bullet, _startPosition.position, Quaternion.identity);
                BulletController bulletController = b.GetComponent<BulletController>();
                //в каком направлении будет двигаться пуля
                bulletController.Direction = gameObject.GetComponent<PlayerMove>().Direction;
                //с каким слоями будет взаимодействовать пуля
                bulletController.HitMask = _hitMask;

                //звук выстрела
                if (_audioManager != null)
                    _audioManager.AddSound(_shotAudio);

                //дым из ствола при выстреле
                if (_shotParticle != null)
                    _shotParticle.Play();
            }
        }

        //мина
        if ((Input.GetKeyDown(KeyCode.I) || Input.GetMouseButtonDown(1)) && !_mobileControl)
        {
            if (_mina != null && !_playerMove.BlockControl)
                Instantiate(_mina, _startPosition.position, Quaternion.identity);
        }
	}
}
