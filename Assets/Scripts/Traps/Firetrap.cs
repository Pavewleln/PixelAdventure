using UnityEngine;
using System.Collections;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage; // Урон, который наносит ловушка

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay; // Задержка перед активацией ловушки
    [SerializeField] private float activeTime; // Время активности ловушки
    private Animator anim; // Компонент аниматора
    private SpriteRenderer spriteRend; // Компонент отображения спрайта

    [Header("SFX")]
    [SerializeField] private AudioClip firetrapSound; // Звук ловушки

    private bool triggered; // Флаг, указывающий на активацию ловушки
    private bool active; // Флаг, указывающий на активное состояние ловушки

    private Health playerHealth; // Компонент здоровья игрока

    private void Awake()
    {
        anim = GetComponent<Animator>(); // Получаем компонент аниматора
        spriteRend = GetComponent<SpriteRenderer>(); // Получаем компонент отображения спрайта
    }

    private void Update()
    {
        if (playerHealth != null && active)
            playerHealth.TakeDamage(damage); // Наносим урон игроку, если ловушка активна
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>(); // Получаем компонент здоровья игрока

            if (!triggered)
                StartCoroutine(ActivateFiretrap()); // Запускаем активацию ловушки

            if (active)
                collision.GetComponent<Health>().TakeDamage(damage); // Наносим урон игроку, если ловушка активна
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerHealth = null; // Сбрасываем компонент здоровья игрока
    }

    private IEnumerator ActivateFiretrap()
    {
        triggered = true; // Устанавливаем флаг активации ловушки в `true`
        spriteRend.color = Color.red; // Меняем цвет спрайта на красный для оповещения игрока

        yield return new WaitForSeconds(activationDelay); // Ждем задержку перед активацией

        SoundManager.instance.PlaySound(firetrapSound); // Воспроизводим звук ловушки
        spriteRend.color = Color.white; // Возвращаем цвет спрайта к исходному
        active = true; // Устанавливаем флаг активности ловушки в `true`
        anim.SetBool("Activated", true); // Включаем анимацию активации ловушки

        yield return new WaitForSeconds(activeTime); // Ждем время активности ловушки

        active = false; // Устанавливаем флаг активности ловушки в `false`
        triggered = false; // Сбрасываем флаг активации ловушки в `false`
        anim.SetBool("Activated", false); // Выключаем анимацию активации ловушки
    }
}