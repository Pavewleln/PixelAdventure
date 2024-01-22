using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth; // Начальное значение здоровья
    public float currentHealth { get; private set; } // Текущее значение здоровья
    private Animator anim; // Компонент Animator для управления анимацией
    private bool dead; // Флаг, указывающий, мертв ли персонаж

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration; // Длительность периода неуязвимости после получения урона
    [SerializeField] private int numberOfFlashes; // Количество миганий при неуязвимости
    private SpriteRenderer spriteRend; // Компонент SpriteRenderer для управления отображением спрайта

    [Header("Components")]
    [SerializeField] private Behaviour[] components; // Массив компонентов, которые будут выключены при смерти
    private bool invulnerable; // Флаг, указывающий, является ли персонаж временно неуязвимым после получения урона

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound; // Звук смерти
    [SerializeField] private AudioClip hurtSound; // Звук получения урона


    private Rigidbody2D body;

    private void Awake()
    {
        currentHealth = startingHealth; // Устанавливаем текущее здоровье равным начальному
        anim = GetComponent<Animator>(); // Получаем компонент Animator
        spriteRend = GetComponent<SpriteRenderer>(); // Получаем компонент SpriteRenderer
        body = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return; // Если персонаж неуязвим, выходим из метода
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth); // Уменьшаем здоровье на значение _damage

        if (currentHealth > 0) // Если здоровье больше 0
        {
            anim.SetTrigger("Hit"); // Запускаем анимацию получения урона

            // Применяем силу отскока к персонажу
            Vector2 knockbackForce = new Vector2(0f, 15f); // Измените значения, если необходимо
            body.velocity = knockbackForce;

            StartCoroutine(Invunerability()); // Запускаем период неуязвимости
            SoundManager.instance.PlaySound(hurtSound); // Воспроизводим звук получения урона
        }
        else // Если здоровье меньше или равно 0
        {
            if (!dead) // Если персонаж еще не мертв
            {
                // Выключаем все прикрепленные компоненты
                foreach (Behaviour component in components)
                    component.enabled = false;

                anim.SetBool("Grounded", true); // Устанавливаем анимацию на земле в true
                anim.SetTrigger("Dead"); // Запускаем анимацию смерти

                dead = true; // Устанавливаем флаг смерти в true
                SoundManager.instance.PlaySound(deathSound); // Воспроизводим звук смерти
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth); // Увеличиваем здоровье на значение _value
    }

    private IEnumerator Invunerability()
    {
        invulnerable = true; // Устанавливаем флаг неуязвимости в true
        Physics2D.IgnoreLayerCollision(10, 11, true); // Игнорируем коллизии с вражескими атаками

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f); // Устанавливаем цвет спрайта на красный
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2)); // Ждем некоторое время
            spriteRend.color = Color.white; // Устанавливаем цвет спрайта на белый
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2)); // Ждем некоторое время
        }

        Physics2D.IgnoreLayerCollision(10, 11, false); // Включаем коллизии с вражескими атаками
        invulnerable = false; // Устанавливаем флаг неуязвимости в false
    }

    private void Deactivate()
    {
        gameObject.SetActive(false); // Выключаем игровой объект
    }

    //Respawn
    public void Respawn()
    {
        AddHealth(startingHealth); // Восстанавливаем здоровье до начального значения
        anim.ResetTrigger("Dead"); // Сбрасываем триггер смерти анимации
        anim.Play("Idle"); // Запускаем анимацию бездействия
        // StartCoroutine(Invunerability()); // Запускаем период неуязвимости
        dead = false; // Устанавливаем флаг смерти в false

        // Включаем все прикрепленные компоненты
        foreach (Behaviour component in components)
            component.enabled = true;
    }
}