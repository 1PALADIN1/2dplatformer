using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

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
    [SerializeField]
    private bool _mobileControl = true;      //управление с мобилки?

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
        if (!_blockControl)
        {
            bool isJumped = false;

            if (_mobileControl) isJumped = CrossPlatformInputManager.GetButtonDown("Jump") ;
            else
                isJumped = Input.GetButtonDown("Jump");

            if (isJumped && _grounded)
            {
                Jump = true;
                //_animator.SetTrigger("toJump");
                _animator.SetBool("jump", Jump);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!_blockControl)
        {
            //чтение ввода
            float axisHor = 0.0f;
            if (_mobileControl) axisHor = CrossPlatformInputManager.GetAxis("Horizontal");
            else
                axisHor  = Input.GetAxis("Horizontal");

            //проверка соприкасается ли игрок с землёй
            _grounded = Physics2D.Linecast(transform.position, _groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

            //передаём в аниматор текущее значение axis по модулю (для смены анимации с Idle -> Run и наоборот)
            _animator.SetFloat("speed", Mathf.Abs(axisHor));
            //передаём скорость игрока по вертикали, чтобы сделать переходы анимации прыжка
            _animator.SetFloat("vspeed", Mathf.Abs(_rigidbody2d.velocity.y));
            _animator.SetBool("grounded", _grounded);

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
            Invoke("UnsetJump", 0.5f);
        }
    }

    /// <summary>
    /// Сброс прыжка в аниматоре
    /// </summary>
    private void UnsetJump()
    {
        _animator.SetBool("jump", false);
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
