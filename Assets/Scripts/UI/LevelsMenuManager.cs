using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsMenuManager : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;  // Экран паузы

    [SerializeField] private Text[] levelTexts; // Массив текстовых полей

    private void Awake()
    {
        pauseScreen.SetActive(true);  // Изначально отключаем экран паузы
    }


    private void Start()
    {
        for (int i = 0; i < levelTexts.Length; i++)
        {
            int levelIndex = i + 1; // Индекс уровня (начиная с 1)

            // Добавляем обработчик события для каждого текстового поля
            levelTexts[i].GetComponent<Button>().onClick.AddListener(() => LoadLevel(levelIndex));
        }
    }

    private void LoadLevel(int levelIndex)
    {
        // Здесь можешь добавить свой код для загрузки уровня по его индексу
        // Например:
        SceneManager.LoadScene("Level" + levelIndex);
    }
}
