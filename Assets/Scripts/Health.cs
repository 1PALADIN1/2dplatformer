using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {

    [SerializeField]
    private int _maxHealth = 10;        //максимальный уровень здоровья

    private int _health;                //текущий уровень здоровья
    private int _healthPercent;         //текущий уровень здоровья в процентах

    private void Start()
    {
        _health = _maxHealth;
        CountHealth();
    }

    /// <summary>
    /// Получение урона
    /// </summary>
    /// <param name="damage">Урон</param>
    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            //если игрок умирает, то перезагружаем уровень
            if (gameObject.tag.Equals("Player"))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            else
                Destroy(gameObject);
        }
        CountHealth();
    }

    /// <summary>
    /// Вывод жизней в процентах
    /// </summary>
    private void OnGUI()
    {
        //жизни игрока
        if (gameObject.tag.Equals("Player"))
        {
            GUI.Box(new Rect(0, 0, 110, 40), "Player");
            GUI.Label(new Rect(10, 20, 100, 20), "HP: " + _healthPercent + "%");
        }
            
    }

    /// <summary>
    /// Вычисляем жизни в процентах
    /// </summary>
    private void CountHealth()
    {
        float fhealth = _health;
        float fmaxHealth = _maxHealth;

        _healthPercent = (int)(fhealth / fmaxHealth * 100);
    }
}
