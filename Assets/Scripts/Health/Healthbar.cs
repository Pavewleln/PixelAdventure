using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth; // Ссылка на компонент Health персонажа
    [SerializeField] private Image totalhealthBar; // Ссылка на компонент Image для отображения полного здоровья
    [SerializeField] private Image currenthealthBar; // Ссылка на компонент Image для отображения текущего здоровья

    private void Start()
    {
        totalhealthBar.fillAmount = playerHealth.currentHealth / 10; // Устанавливаем начальное значение полосы здоровья
    }

    private void Update()
    {
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10; // Обновляем значение текущего здоровья на полосе здоровья
    }
}