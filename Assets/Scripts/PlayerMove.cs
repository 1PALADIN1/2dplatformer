using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;         //куда смотрит игрок? по умолчанию вправо
    [HideInInspector]
    public bool jump = false;               //нажата ли клавиша прыжка

    public float moveForce = 365f;          //сила, которая прикладывается для перемещения игрока 
    public float maxSpeed = 5f;				//максимальная скорость перемещения
    public float jumpForce = 1000f;         //сила прыжка

    private Rigidbody2D rigidbody;
    private Transform groundCheck;
    private bool grounded = false;          //стоит ли игрок на земле
    private Animator animator;

    // Use this for initialization
    void Awake ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("groundCheck");
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //проверка соприкасается ли игрок с землёй
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        animator.SetFloat("speed", Mathf.Abs(h));

        //перемещение игрока
        if (h * rigidbody.velocity.x < maxSpeed)
        {
            rigidbody.AddForce(Vector2.right * h * moveForce);
        }

        if (Mathf.Abs(rigidbody.velocity.x) > maxSpeed)
        {
            rigidbody.velocity = new Vector2(Mathf.Sign(rigidbody.velocity.x) * maxSpeed, rigidbody.velocity.y);
        }

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        //обработка прыжка
        if (jump)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }

    /// <summary>
    /// Зеркальное отражение спрайта
    /// </summary>
    void Flip()
    {
        facingRight = !facingRight;
        
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
