using UnityEngine;

public class PlayerStrike : MonoBehaviour {

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

    private AudioManager _audioManager; //менеджер звуков

    private void Start()
    {
        //получаем аудиоменеджер
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void Update () {
        //выстрелы
		if (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0))
        {
            if (_bullet != null)
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
            }
        }

        //мина
        if (Input.GetKeyDown(KeyCode.I) || Input.GetMouseButtonDown(1))
        {
            if (_mina != null)
            {
                Instantiate(_mina, _startPosition.position, Quaternion.identity);
            }
        }
	}
}
