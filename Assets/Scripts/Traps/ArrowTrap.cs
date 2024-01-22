using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown; // Время между атаками ловушки
    [SerializeField] private Transform firePoint; // Место, откуда будет выпущена стрела
    [SerializeField] private GameObject[] arrows; // Массив стрел
    private float cooldownTimer; // Таймер для отслеживания времени до следующей атаки

    [Header("SFX")]
    [SerializeField] private AudioClip arrowSound; // Звук стрелы

    private void Attack()
    {
        cooldownTimer = 0; // Сброс таймера

        SoundManager.instance.PlaySound(arrowSound); // Воспроизведение звука стрелы

        int arrowIndex = FindArrow(); // Находим неактивную стрелу в массиве
        arrows[arrowIndex].transform.position = firePoint.position; // Устанавливаем позицию стрелы равной позиции точки выстрела
        arrows[arrowIndex].GetComponent<EnemyProjectile>().ActivateProjectile(); // Активируем стрелу для полета
    }

    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy) // Если стрела неактивна
                return i; // Возвращаем индекс стрелы
        }
        return 0; // Если все стрелы активны, возвращаем 0
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime; // Увеличиваем таймер на время, прошедшее с последнего кадра

        if (cooldownTimer >= attackCooldown) // Если прошло достаточно времени для атаки
            Attack(); // Производим атаку
    }
}