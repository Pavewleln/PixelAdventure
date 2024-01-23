using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;  // Экран паузы

    private void Awake()
    {
        pauseScreen.SetActive(true);  // Изначально отключаем экран паузы
    }

    public void Play()
    {
        SceneManager.LoadScene("Levels");
    }

    // Выход из игры/выход из режима игры, если в режиме редактора
    public void Quit()
    {
        Application.Quit();  // Завершаем игру (работает только в сборке)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Выходим из режима игры (будет выполнено только в режиме редактора)
#endif
    }

    #region Pause

    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
    #endregion
}
