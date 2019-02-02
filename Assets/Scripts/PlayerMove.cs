using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [HideInInspector]
    public bool FacingRight = true;         //куда смотрит игрок? по умолчанию вправо
    [HideInInspector]
    public bool Jump = false;               //нажата ли клавиша прыжка

    [SerializeField]
    private float _moveSpeed = 20.0f;        //скорость перемещения игрока
    [SerializeField]
    private float _jumpForce = 1000f;        //сила прыжка

    private Rigidbody2D _rigidbody2d;        //компонент Rigidbody объекта
    private Transform _groundCheck;
    private bool _grounded = false;          //стоит ли игрок на земле
    private Animator _animator;              //компонент Animator объекта
    private Vector2 _verticalMove;           //темповый вектор для перемещения (чтобы не плодить тысячу объектов типа Vector2)
    private bool _blockControl = false;      //заблокировано ли польховательское управление

    /// <summary>
    /// Блокирует пользовательский контроль над персонажем (например, для автоматических сценариев)
    /// </summary>
    public bool BlockControl
    {
        get
        {
            return _blockControl;
        }
        set
        {
            _blockControl = value;
        }
    }

    /// <summary>
    /// Куда смотрит игрок: 1 - вправо, -1 - влево
    /// </summary>
    public int Direction
    {
        get
        {
            if (FacingRight) return 1;
            else
                return -1;
        }
    }
    
    void Awake ()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _groundCheck = transform.Find("groundCheck");
        _animator = GetComponent<Animator>();
        _verticalMove = new Vector2();
    }

	void Update ()
    {
        //проверка соприкасается ли игрок с землёй
        _grounded = Physics2D.Linecast(transform.position, _groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (!_blockControl)
        {
            if (Input.GetButtonDown("Jump") && _grounded)
                Jump = true;
        }
    }

    private void FixedUpdate()
    {
        if (!_blockControl)
        {
            //чтение ввода
            var axisHor = Input.GetAxis("Horizontal");

            //передаём в аниматор текущее значение axis по модулю (для смены анимации с Idle -> Run и наоборот)
            _animator.SetFloat("speed", Mathf.Abs(axisHor));
            //перемещение объекта
            _verticalMove.Set(axisHor * _moveSpeed, _rigidbody2d.velocity.y);
            _rigidbody2d.velocity = _verticalMove; //устанавливаем скорость перемещения

            if (axisHor > 0 && !FacingRight)
                Flip();
            else if (axisHor < 0 && FacingRight)
                Flip();
        }

        //обработка прыжка
        if (Jump)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, _jumpForce));
            Jump = false;
        }
    }

    /// <summary>
    /// Зеркальное отражение спрайта (разворот)
    /// </summary>
    void Flip()
    {
        FacingRight = !FacingRight;
        
        //получаем вектор масштабирования
        Vector3 theScale = transform.localScale;
        //зеркально отражаем вектор по оси X
        theScale.x *= -1;
        //присваиваем новое значение для масштабирования
        transform.localScale = theScale;
    }
}
