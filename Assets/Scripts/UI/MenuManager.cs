using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform arrow;  // Ссылка на RectTransform стрелки
    [SerializeField] private RectTransform[] buttons;  // Массив ссылок на RectTransform кнопок
    [SerializeField] private AudioClip changeSound;  // Звук при переключении позиции
    [SerializeField] private AudioClip interactSound;  // Звук при взаимодействии
    private int currentPosition;  // Текущая позиция

    private void Awake()
    {
        ChangePosition(0);  // Устанавливаем начальную позицию
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);  // Перемещаемся вверх
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(1);  // Перемещаемся вниз

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetButtonDown("Submit"))
            Interact();  // Взаимодействуем с текущей позицией
    }

    public void ChangePosition(int _change)
    {
        currentPosition += _change;  // Изменяем текущую позицию на указанное значение

        if (_change != 0)
            SoundManager.instance.PlaySound(changeSound);  // Проигрываем звук переключения

        if (currentPosition < 0)
            currentPosition = buttons.Length - 1;  // Если вышли за пределы массива, перемещаемся в конец
        else if (currentPosition > buttons.Length - 1)
            currentPosition = 0;  // Если вышли за пределы массива, перемещаемся в начало

        AssignPosition();  // Применяем новую позицию
    }

    private void AssignPosition()
    {
        arrow.position = new Vector3(arrow.position.x, buttons[currentPosition].position.y);  // Устанавливаем позицию стрелки над текущей кнопкой
    }

    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);  // Проигрываем звук взаимодействия

        if (currentPosition == 0)
        {
            // Начать игру
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level", 1));  // Загружаем сцену с уровнем, сохраненным в PlayerPrefs
        }
        else if (currentPosition == 1)
        {
            // Открыть настройки
        }
        else if (currentPosition == 2)
        {
            // Открыть кредиты
        }
        else if (currentPosition == 3)
            Application.Quit();  // Выход из приложения
    }
}