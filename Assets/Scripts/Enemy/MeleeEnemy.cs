using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown; // Время между атаками
    [SerializeField] private float range; // Дальность атаки
    [SerializeField] private int damage; // Урон от атаки

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance; // Дистанция коллайдера
    [SerializeField] private BoxCollider2D boxCollider; // Компонент прямоугольного коллайдера

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer; // Слой игрока
    private float cooldownTimer = Mathf.Infinity; // Таймер отката атаки

    // References
    private Animator anim; // Компонент аниматора
    private Health playerHealth; // Компонент здоровья игрока
    private EnemyPatrol enemyPatrol; // Компонент патрулирования врага

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        // Атаковать только при видимости игрока?
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack"); // Запускаем анимацию ближней атаки
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight(); // Включаем/выключаем патрулирование врага в зависимости от видимости игрока
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer); // Проверяем, есть ли игрок в поле зрения

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>(); // Получаем компонент здоровья игрока

        return hit.collider != null; // Возвращаем true, если есть столкновение с игроком
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)); // Рисуем границы области видимости в редакторе
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
            playerHealth.TakeDamage(damage); // Наносим урон игроку, если он находится в области видимости
    }
}