using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float moveSpeed = 5.0f;      //скорость перемещения
    public int direction = 1;           //направление движения
    public int damage = 4;              //наносимый урон

    private Rigidbody2D rigidbody2d;    //компонент Rigidbody объекта
    private Vector2 verticalMove;       //темповый вектор для перемещения (чтобы не плодить тысячу объектов типа Vector2)

    private void Start () {
        rigidbody2d = GetComponent<Rigidbody2D>();
        verticalMove = new Vector2();
        //уничтожение объекта через 3 секунды после появления
        Destroy(gameObject, 3.0f);
	}

    private void FixedUpdate()
    {
        //перемещение пули
        verticalMove.Set(direction * moveSpeed, rigidbody2d.velocity.y);
        rigidbody2d.velocity = verticalMove;
    }

    /// <summary>
    /// Проверка на столкновения с другими объектами
    /// </summary>
    /// <param name="collision">Компонент Collision2D объекта, с которым столкнулись</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collisionGO = collision.gameObject;

        //если сталкиваемся с объектом, помеченным тэгом Enemy
        if (collision.gameObject.tag == "Enemy")
        {
            //уничтожаем пулю
            Destroy(gameObject);
            //берём компоненту здоровья у противника
            Health enemyHealth = collisionGO.GetComponent<Health>();
            //если у противника есть компонента, то наносим противнику урон
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
        //если сталкиваемся с землёй, то пуля уничтожается
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
