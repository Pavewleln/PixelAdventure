using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown; // Время между атаками
    [SerializeField] private float range; // Дальность атаки
    [SerializeField] private int damage; // Урон от атаки

    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint; // Точка, откуда вылетают снаряды
    [SerializeField] private GameObject[] fireballs; // Префабы снарядов

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance; // Дистанция коллайдера
    [SerializeField] private BoxCollider2D boxCollider; // Компонент прямоугольного коллайдера

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer; // Слой игрока
    private float cooldownTimer = Mathf.Infinity; // Таймер отката атаки

    [Header("Fireball Sound")]
    [SerializeField] private AudioClip fireballSound; // Звук снаряда

    // References
    private Animator anim; // Компонент аниматора
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
                anim.SetTrigger("rangedAttack"); // Запускаем анимацию дальней атаки
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight(); // Включаем/выключаем патрулирование врага в зависимости от видимости игрока
    }

    private void RangedAttack()
    {
        SoundManager.instance.PlaySound(fireballSound); // Воспроизводим звук снаряда
        cooldownTimer = 0;
        fireballs[FindFireball()].transform.position = firepoint.position; // Позиционируем снаряд в точке firepoint
        fireballs[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile(); // Активируем снаряд
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i; // Возвращаем индекс первого неактивного снаряда
        }
        return 0;
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer); // Проверяем, есть ли игрок в поле зрения

        return hit.collider != null; // Возвращаем true, если есть столкновение с игроком
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)); // Рисуем границы области видимости в редакторе
    }
}