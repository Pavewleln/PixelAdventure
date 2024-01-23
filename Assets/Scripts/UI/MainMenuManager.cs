using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;  // ����� �����

    private void Awake()
    {
        pauseScreen.SetActive(true);  // ���������� ��������� ����� �����
    }

    public void Play()
    {
        SceneManager.LoadScene("Levels");
    }

    // ����� �� ����/����� �� ������ ����, ���� � ������ ���������
    public void Quit()
    {
        Application.Quit();  // ��������� ���� (�������� ������ � ������)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // ������� �� ������ ���� (����� ��������� ������ � ������ ���������)
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
