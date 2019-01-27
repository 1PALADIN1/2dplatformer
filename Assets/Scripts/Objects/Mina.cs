using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mina : MonoBehaviour
{
    [SerializeField]
    private int damage = 5;                         //урон
    [SerializeField]
    private float pushForce = 10.0f;                //сила отталкивания
    [SerializeField]
    private float delay = 2.0f;                     //задержка при создании мины (активируется только через это время)

    private BoxCollider2D boxCollider;              //коллайдер для мины (если игрок/противник наступит)
    private CircleCollider2D circleCollider;        //радиус поражения
    
	void Start ()
    {
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
	}

    /// <summary>
    /// Обработка взрыва (мина активировалась)
    /// </summary>
    /// <param name="collision">Объект Collision2D того, кто находится в радиусе поражения</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            Health health = collision.GetComponent<Health>();
            if (rb != null)
            {
                //обнуляем скорость перемещения
                rb.velocity = Vector2.zero;
                //отталкиваем объекты
                rb.AddForce((collision.transform.position - transform.position).normalized * pushForce, ForceMode2D.Impulse);
            }
            //наносим урон
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// Проверка, кто наступил на мину
    /// </summary>
    /// <param name="collision">Объект Collision2D того, кто наступил</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            circleCollider.enabled = true;
        }
    }
}
