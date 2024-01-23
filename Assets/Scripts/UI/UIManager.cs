using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;  // Экран "Game Over"
    [SerializeField] private AudioClip gameOverSound;  // Звук "Game Over"

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;  // Экран паузы

    private void Awake()
    {
        gameOverScreen.SetActive(false);  // Изначально отключаем экран "Game Over"
        pauseScreen.SetActive(false);  // Изначально отключаем экран паузы
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Если экран паузы уже активен, снимаем паузу, и наоборот
            PauseGame(!pauseScreen.activeInHierarchy);
        }
    }

    #region Game Over
    // Активируем экран "Game Over"
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);  // Проигрываем звук "Game Over"
    }

    // Перезапуск уровня
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    // Главное меню
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    // Выход из игры/выход из режима игры, если в режиме редактора
    public void Quit()
    {
        Application.Quit();  // Завершаем игру (работает только в сборке)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Выходим из режима игры (будет выполнено только в режиме редактора)
#endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        // Если status == true, активируем паузу; если status == false, снимаем паузу
        pauseScreen.SetActive(status);

        // Когда статус паузы равен true, изменяем Time.timeScale на 0 (время останавливается);
        // когда статус паузы равен false, возвращаем Time.timeScale на 1 (время идет нормально)
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    #endregion
}