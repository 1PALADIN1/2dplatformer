using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;         //куда смотрит игрок? по умолчанию вправо
    [HideInInspector]
    public bool jump = false;               //нажата ли клавиша прыжка

    [SerializeField]
    private float moveSpeed = 5.0f;         //скорость перемещения игрока
    [SerializeField]
    private float jumpForce = 1000f;         //сила прыжка

    private Rigidbody2D rigidbody;          //компонент Rigidbody объекта
    private Transform groundCheck;
    private bool grounded = false;          //стоит ли игрок на земле
    private Animator animator;              //компонент Animator объекта

    public int Direction
    {
        get
        {
            if (facingRight) return 1;
            else
                return -1;
        }
    }

    
    void Awake ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("groundCheck");
        animator = GetComponent<Animator>();
    }

	void Update ()
    {
        //чтение ввода
        var axisHor = Input.GetAxis("Horizontal");

        //передаём в аниматор текущее значение axis по модулю (для смены анимации с Idle -> Run и наоборот)
        animator.SetFloat("speed", Mathf.Abs(axisHor));

        //перемещение объекта
        transform.Translate(axisHor * moveSpeed * Time.deltaTime, 0, 0, Space.Self);

        if (axisHor > 0 && !facingRight)
            Flip();
        else if (axisHor < 0 && facingRight)
            Flip();

        //проверка соприкасается ли игрок с землёй
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;
    }

    private void FixedUpdate()
    {
        //обработка прыжка
        if (jump)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }

    /// <summary>
    /// Зеркальное отражение спрайта (разворот)
    /// </summary>
    void Flip()
    {
        facingRight = !facingRight;
        
        //получаем вектор масштабирования
        Vector3 theScale = transform.localScale;
        //зеркально отражаем вектор по оси X
        theScale.x *= -1;
        //присваиваем новое значение для масштабирования
        transform.localScale = theScale;
    }
}
