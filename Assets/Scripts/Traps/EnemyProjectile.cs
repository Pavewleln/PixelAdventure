using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed; // Скорость полета снаряда
    [SerializeField] private float resetTime; // Время жизни снаряда, после которого он деактивируется
    private float lifetime; // Прошедшее время с момента активации снаряда
    private Animator anim; // Компонент аниматора
    private BoxCollider2D coll; // Коллайдер снаряда

    private bool hit; // Флаг, указывающий на попадание снаряда в цель

    private void Awake()
    {
        anim = GetComponent<Animator>(); // Получаем компонент аниматора
        coll = GetComponent<BoxCollider2D>(); // Получаем коллайдер снаряда
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true); // Активируем снаряд
        coll.enabled = true; // Включаем коллайдер снаряда
    }

    private void Update()
    {
        if (hit) return; // Если снаряд уже попал в цель, прекращаем его движение

        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0); // Перемещаем снаряд по направлению

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false); // Если время жизни снаряда истекло, деактивируем его
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true; // Устанавливаем флаг попадания в `true`
        base.OnTriggerEnter2D(collision); // Вызываем логику родительского класса

        coll.enabled = false; // Отключаем коллайдер снаряда

        if (anim != null)
            anim.SetTrigger("explode"); // Если снаряд - огненный шар, запускаем анимацию взрыва
        else
            gameObject.SetActive(false); // Если снаряд - стрела, деактивируем ее
    }

    private void Deactivate()
    {
        gameObject.SetActive(false); // Деактивируем снаряд
    }
}