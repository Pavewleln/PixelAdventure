using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue; // Значение здоровья, которое будет восстановлено при подборе
    [SerializeField] private AudioClip pickupSound; // Звук подбора

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") // Если столкнулся с игроком
        {
            SoundManager.instance.PlaySound(pickupSound); // Воспроизводим звук подбора
            collision.GetComponent<Health>().AddHealth(healthValue); // Увеличиваем здоровье игрока
            gameObject.SetActive(false); // Отключаем объект, чтобы скрыть его
        }
    }
}