using UnityEngine;

public class Mina : MonoBehaviour
{
    [SerializeField]
    private int _damage = 5;                        //урон
    [SerializeField]
    private float _pushForce = 10.0f;               //сила отталкивания
    [SerializeField]
    private float _delay = 2.0f;                    //задержка при создании мины (активируется только через это время)

    private BoxCollider2D _boxCollider;             //коллайдер для мины (если игрок/противник наступит)
    private CircleCollider2D _circleCollider;       //радиус поражения
    
	void Start ()
    {
        _circleCollider = gameObject.GetComponent<CircleCollider2D>();
        _boxCollider = gameObject.GetComponent<BoxCollider2D>();
	}

    /// <summary>
    /// Обработка взрыва (мина активировалась)
    /// </summary>
    /// <param name="collision">Объект Collision2D того, кто находится в радиусе поражения</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy") || collision.gameObject.tag.Equals("Player"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            //здоровье
            Health health = collision.GetComponent<Health>();

            if (rb != null)
            {
                //обнуляем скорость перемещения
                rb.velocity = Vector2.zero;
                //отталкиваем объекты
                Vector3 tmp = (collision.transform.position - transform.position).normalized;
                rb.AddForce(new Vector2(tmp.x * _pushForce, tmp.y * _pushForce), ForceMode2D.Impulse);

                print(tmp + " " + collision.gameObject.tag);
            }
            //наносим урон
            if (health != null)
                health.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// Проверка, кто наступил на мину
    /// </summary>
    /// <param name="collision">Объект Collision2D того, кто наступил</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            _circleCollider.enabled = true;
        }
    }
}
