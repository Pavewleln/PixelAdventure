using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage; // Урон, который враг наносит игроку

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") // Если враг столкнулся с игроком
            Debug.Log("Damage");
            collision.GetComponent<Health>()?.TakeDamage(damage); // Наносим урон игроку, если у него есть компонент Health
    }
}