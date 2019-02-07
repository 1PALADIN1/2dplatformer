using UnityEngine;
using UnityEngine.UI;

public class HelperScript : MonoBehaviour
{
    [TextArea]
    [SerializeField]
    private string _textMessage;        //текст, который будет отображаться на панели
    [SerializeField]
    private Text _textComponent;        //компонента с текстом из канваса
    [SerializeField]
    private Canvas _canvas;             //сам канвас
    [SerializeField]
    private float _touchRadius;         //радиус столкновения с игроком (внутри него становится видимым канвас)
    [SerializeField]
    private LayerMask _layerMask;       //с кем сталкиваясь отображаем подсказки
    [SerializeField]
    private AudioClip _msgSound;        //звук при сообщении

    private RaycastHit2D _raycastHit2D;
    private AudioManager _audioManager; //менеджер звуков

    private void Start ()
    {
        _textComponent.text = _textMessage;
        if (_msgSound != null)
            _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void FixedUpdate()
    {
        //проверяем столкновение с игроком
        _raycastHit2D = Physics2D.CircleCast(transform.position, _touchRadius, Vector2.right, 0.0f, _layerMask);

        //если игрок в поле зрения
        if (_raycastHit2D.collider != null)
        {
            //активируем канвас
            if (!_canvas.enabled)
            {
                _canvas.enabled = true;
                //TODO убрать этот дикий костыль! Господи, как же мне стыдно!!!
                _textComponent.resizeTextForBestFit = true;
                _textComponent.resizeTextForBestFit = false;

                //проигрываем звук
                if (_msgSound != null)
                    _audioManager.AddSound(_msgSound);
            }
        }
        else
        {
            //в противном случае прячем канвас
            if (_canvas.enabled)
                _canvas.enabled = false;
        }
    }
}
