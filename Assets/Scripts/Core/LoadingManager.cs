using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager instance { get; private set; }

    private void Awake()
    {
        // Сохраняем этот объект при переходе на новую сцену
        if (instance == null)
        {
            instance = this;
        }
        // Уничтожаем дублирующиеся объекты
        else if (instance != null && instance != this)
            Destroy(gameObject);
    }

    public void LoadCurrentLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("currentLevel", 1); // Получаем текущий уровень из PlayerPrefs
        SceneManager.LoadScene(currentLevel); // Загружаем сцену с текущим уровнем
    }

    public void Restart()
    {
        //SceneManager.LoadScene(currentLevel);
    }
}