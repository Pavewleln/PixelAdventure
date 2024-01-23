using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header("SpikeHead Attributes")]
    [SerializeField] private float speed; // Скорость передвижения шипастой головы
    [SerializeField] private float range; // Расстояние, на котором шипастая голова обнаруживает игрока
    [SerializeField] private float checkDelay; // Задержка между проверками обнаружения игрока
    [SerializeField] private LayerMask playerLayer; // Слой игрока
    private Vector3[] directions = new Vector3[4]; // Направления, в которых шипастая голова ищет игрока
    private Vector3 destination; // Точка назначения для перемещения
    private float checkTimer; // Таймер для проверки обнаружения игрока
    private bool attacking; // Флаг, указывающий, атакует ли шипастая голова

    [Header("SFX")]
    [SerializeField] private AudioClip impactSound; // Звук столкновения шипастой головы

    private void OnEnable()
    {
        Stop(); // Останавливаем шипастую голову при включении компонента
    }

    private void Update()
    {
        // Перемещаем шипастую голову к точке назначения только во время атаки
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer(); // Проверяем наличие игрока
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections(); // Вычисляем направления, в которых ищем игрока

        // Проверяем, видит ли шипастая голова игрока во всех 4 направлениях
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red); // Отрисовываем лучи для отладки
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range; // Направление вправо
        directions[1] = -transform.right * range; // Направление влево
        directions[2] = transform.up * range; // Направление вверх
        directions[3] = -transform.up * range; // Направление вниз
    }

    private void Stop()
    {
        destination = transform.position; // Устанавливаем точку назначения равной текущей позиции, чтобы остановить перемещение
        attacking = false;
    }
}