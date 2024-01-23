using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsMenuManager : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;  // ����� �����

    [SerializeField] private Text[] levelTexts; // ������ ��������� �����

    private void Awake()
    {
        pauseScreen.SetActive(true);  // ���������� ��������� ����� �����
    }


    private void Start()
    {
        for (int i = 0; i < levelTexts.Length; i++)
        {
            int levelIndex = i + 1; // ������ ������ (������� � 1)

            // ��������� ���������� ������� ��� ������� ���������� ����
            levelTexts[i].GetComponent<Button>().onClick.AddListener(() => LoadLevel(levelIndex));
        }
    }

    private void LoadLevel(int levelIndex)
    {
        // ����� ������ �������� ���� ��� ��� �������� ������ �� ��� �������
        // ��������:
        SceneManager.LoadScene("Level" + levelIndex);
    }
}
