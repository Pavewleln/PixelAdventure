using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge; // Точка слева, до которой враг будет патрулировать
    [SerializeField] private Transform rightEdge; // Точка справа, до которой враг будет патрулировать

    [Header("Enemy")]
    [SerializeField] private Transform enemy; // Компонент врага

    [Header("Movement parameters")]
    [SerializeField] private float speed; // Скорость перемещения врага
    private Vector3 initScale; // Изначальный масштаб врага
    private bool movingLeft; // Флаг, указывающий на направление движения

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration; // Длительность простоя
    private float idleTimer; // Таймер простоя

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim; // Компонент аниматора врага

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false); // Устанавливаем состояние анимации "движение" в false при отключении скрипта
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1); // Двигаемся влево
            else
                DirectionChange(); // Меняем направление
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1); // Двигаемся вправо
            else
                DirectionChange(); // Меняем направление
        }
    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false); // Устанавливаем состояние анимации "движение" в false
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
            movingLeft = !movingLeft; // Меняем направление движения
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true); // Устанавливаем состояние анимации "движение" в true

        // Изменяем масштаб врага, чтобы он смотрел в указанном направлении
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        // Двигаем врага в указанном направлении со скоростью speed
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }
}