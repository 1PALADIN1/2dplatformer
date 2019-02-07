using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //если игрок попадает на шипы
        if (collision.gameObject.tag.Equals("Player"))
        {
            Health health = collision.gameObject.GetComponent<Health>();

            //устанавливаем его хп в 0
            if (health != null)
            {
                int currentHP = health.CurrentHP;
                health.TakeDamage(currentHP);
            }
        }
    }

}
