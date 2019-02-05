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

    private RaycastHit2D _raycastHit2D;

	private void Start ()
    {
        _textComponent.text = _textMessage;
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
                _textComponent.resizeTextForBestFit = true;
                _textComponent.resizeTextForBestFit = false;
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
