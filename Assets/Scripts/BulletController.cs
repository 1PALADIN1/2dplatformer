using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float moveSpeed = 5.0f;      //скорость перемещения
    public int direction = 1;           //направление движения
    public int damage = 4;              //наносимый урон

	void Start () {
        //уничтожение объекта через 3 секунды после появления
        Destroy(gameObject, 3.0f);
	}
	
	void Update () {
        //перемещение по оси X
        transform.Translate(direction * moveSpeed * Time.deltaTime, 0, 0, Space.Self);
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
    }
}
