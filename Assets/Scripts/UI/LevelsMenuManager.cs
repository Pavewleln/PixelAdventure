using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsMenuManager : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;  // Экран паузы

    [Header("Array Buttons Level")]
    [SerializeField] private Button[] levelButtons; // Массив кнопок уровней

    [Header("Reset Button")]
    [SerializeField] private Button resetButton; // Кнопка сброса уровней

    private void Awake()
    {
        pauseScreen.SetActive(true);  // Изначально отключаем экран паузы
    }

    private void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;

            bool isUnlocked = levelIndex <= unlockedLevel;
            levelButtons[i].gameObject.SetActive(isUnlocked);

            levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex));
        }
    }

    private void LoadLevel(int levelIndex)
    {
        string sceneName = "Level" + levelIndex;
        SceneManager.LoadScene(sceneName);
    }

    public void ResetUnlockedLevels()
    {
        PlayerPrefs.DeleteKey("UnlockedLevel");
        SceneManager.LoadScene("LevelsMenu");
    }
}