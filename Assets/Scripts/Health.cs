using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    [SerializeField]
    private int _maxHealth = 10;        //максимальный уровень здоровья

    private int _health;                //текущий уровень здоровья
    private int _healthPercent;         //текущий уровень здоровья в процентах
    private Slider _hpSlider;           //GUI для жизней игрока

    /// <summary>
    /// Получаем текущий уровень здоровья объекта
    /// </summary>
    public int CurrentHP
    {
        get
        {
            return _health;
        }
    }

    private void Start()
    {
        _health = _maxHealth;
        //получаем слайдер для отрисовки хп
        _hpSlider = GameObject.FindGameObjectWithTag("PlayerHPGUI").GetComponent<Slider>();
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
    /// Вычисляем жизни в процентах
    /// </summary>
    private void CountHealth()
    {
        float fhealth = _health;
        float fmaxHealth = _maxHealth;

        _healthPercent = (int)(fhealth / fmaxHealth * 100);

        //для отрисовки хп
        if (gameObject.tag.Equals("Player"))
        {
            if (fhealth < 0) fhealth = 0;
            if (_hpSlider != null)
                _hpSlider.value = fhealth / fmaxHealth;
        }
    }
}
