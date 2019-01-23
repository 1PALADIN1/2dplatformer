using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField]
    private int health = 10;

    /// <summary>
    /// Получение урона
    /// </summary>
    /// <param name="damage">Урон</param>
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
