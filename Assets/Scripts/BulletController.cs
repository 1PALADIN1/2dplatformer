using UnityEngine;

public class BulletController : MonoBehaviour {

    public float MoveSpeed = 5.0f;      //скорость перемещения
    public int Direction = 1;           //направление движения
    public int Damage = 4;              //наносимый урон
    public LayerMask HitMask;           //с какими слоями должна взаимодействовать пуля

    private Rigidbody2D _rigidbody2d;   //компонент Rigidbody объекта
    private Vector2 _verticalMove;      //темповый вектор для перемещения (чтобы не плодить тысячу объектов типа Vector2)
    private RaycastHit2D _raycastHit2d; 

    private void Start () {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _verticalMove = new Vector2();
        //уничтожение объекта через 3 секунды после появления
        Destroy(gameObject, 3.0f);
	}

    private void FixedUpdate()
    {
        //проверка на столкновения пули со слоями HitMask
        //TODO убрать магические числа!
        _raycastHit2d = Physics2D.CircleCast(transform.position, 0.09f, Vector2.right * Direction, 1.0f, HitMask);

        //если есть столкновения с кем-то из списка слоёв
        if (_raycastHit2d.collider != null)
        {
            if (_raycastHit2d.collider.tag.Equals("Enemy"))
            {
                Health enemyHealth = _raycastHit2d.collider.GetComponent<Health>();
                //если у противника есть компонента здоровья, то наносим противнику урон
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(Damage);
                }
            }
            Destroy(gameObject);
        }

        //перемещение пули
        _verticalMove.Set(Direction * MoveSpeed, _rigidbody2d.velocity.y);
        _rigidbody2d.velocity = _verticalMove;
    }
}
