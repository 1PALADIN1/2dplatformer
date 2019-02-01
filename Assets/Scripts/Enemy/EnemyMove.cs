using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5.0f;                //скорость перемещения
    [SerializeField]
    private float _observeFront = 3.0f;             //дистанция спереди, на которой враг обнаруживает игрока
    [SerializeField]
    private float _observeBack = 1.0f;              //дистанция сзади, на которой враг обнаруживает игрока
    [SerializeField]
    private Transform _startPosition;               //из какой точки испускаются лучи
    [SerializeField]
    private LayerMask _checkMask;                   //какие слои нужно проверять
    [SerializeField]
    private float _attackDistance = 2.0f;           //на какую дистанцию нужно подойти к игроку, чтобы остановиться и начать атаку
    [SerializeField]
    private float _deltaStartX = 1.0f;              //погрешность начальной позиции, на которую необходимо возвращаться противнику

    private bool _isFasingRight = true;             //смотрит ли персонаж вправо
    private RaycastHit2D _raycastFront;             //луч, бросаемый перед противником
    private RaycastHit2D _raycastBack;              //луч, бросаемый за противником
    private Rigidbody2D _rigidbody2d;               //компонент Rigidbody
    private Vector2 _moveTemp;                      //темповый вектор для перемещения
    private float _startX;                          //начальная координата по оси X
    private EnemyAttack _enemyAttack;               //объект для атаки игрока

    /// <summary>
    /// Куда смотрит враг: 1 - вправо, -1 - влево
    /// </summary>
    public int Direction
    {
        get
        {
            if (_isFasingRight) return 1;
            else
                return -1;
        }
    }

    void Start ()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _moveTemp = new Vector2();
        //запоминаем стартовую позицию
        _startX = transform.position.x;
	}

    private void FixedUpdate()
    {
        //пускаем лучи 
        _raycastFront = Physics2D.Raycast(_startPosition.position, Vector2.right, _observeFront * Direction, _checkMask);
        _raycastBack = Physics2D.Raycast(_startPosition.position, Vector2.left, _observeBack * Direction, _checkMask);

        if (_raycastFront.collider != null)
        {
            //если видим перед собой игрок, пытаемся догнать его
            if (_raycastFront.collider.gameObject.tag.Equals("Player"))
            {
                if (_raycastFront.distance > _attackDistance)
                {
                    Move();
                }
                //пытаемся атаковать игрока
                if (!_enemyAttack.CanShoot) _enemyAttack.CanShoot = true;
            }
            else
            {
                TryReturnToStartPosition();
                //больше не атакуем игрока
                if (_enemyAttack.CanShoot) _enemyAttack.CanShoot = false;
            }
        }
        else
        {
            TryReturnToStartPosition();
            //больше не атакуем игрока
            if (_enemyAttack.CanShoot) _enemyAttack.CanShoot = false;
        }


        if (_raycastBack.collider != null)
        {
            //если чувствуем сзади тяжёлое дыхане игрока
            if (_raycastBack.collider.gameObject.tag.Equals("Player"))
            {
                //разворачиваемся
                Flip();
            }
        }

        //DEBUG
        Debug.DrawRay(_startPosition.position, Vector2.right * _observeFront * Direction, Color.red);
        Debug.DrawRay(_startPosition.position, Vector2.left * _observeBack * Direction, Color.yellow);
    }

    /// <summary>
    /// Перемещение противника
    /// </summary>
    private void Move()
    {
        _moveTemp.Set(_moveSpeed * Direction, _rigidbody2d.velocity.y);
        _rigidbody2d.velocity = _moveTemp;
    }

    /// <summary>
    /// Разворот противника
    /// </summary>
    private void Flip()
    {
        _isFasingRight = !_isFasingRight;

        //получаем вектор масштабирования
        Vector3 theScale = transform.localScale;
        //зеркально отражаем вектор по оси X
        theScale.x *= -1;
        //присваиваем новое значение для масштабирования
        transform.localScale = theScale;
    }

    /// <summary>
    /// Попытка вернуться на начальную позицию
    /// </summary>
    private void TryReturnToStartPosition()
    {
        //если ушли вправо
        if (transform.position.x > _startX + _deltaStartX)
        {
            if (_isFasingRight) Flip();
            Move();
        }
        //если ушли влево
        if (transform.position.x < _startX - _deltaStartX)
        {
            if (!_isFasingRight) Flip();
            Move();
        }
    }
}
