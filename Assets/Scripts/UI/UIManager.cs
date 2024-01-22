using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;  // ����� "Game Over"
    [SerializeField] private AudioClip gameOverSound;  // ���� "Game Over"

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;  // ����� �����

    private void Awake()
    {
        gameOverScreen.SetActive(false);  // ���������� ��������� ����� "Game Over"
        pauseScreen.SetActive(false);  // ���������� ��������� ����� �����
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ���� ����� ����� ��� �������, ������� �����, � ��������
            PauseGame(!pauseScreen.activeInHierarchy);
        }
    }

    #region Game Over
    // ���������� ����� "Game Over"
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);  // ����������� ���� "Game Over"
    }

    // ���������� ������
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ������� ����
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    // ����� �� ����/����� �� ������ ����, ���� � ������ ���������
    public void Quit()
    {
        Application.Quit();  // ��������� ���� (�������� ������ � ������)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // ������� �� ������ ���� (����� ��������� ������ � ������ ���������)
#endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        // ���� status == true, ���������� �����; ���� status == false, ������� �����
        pauseScreen.SetActive(status);

        // ����� ������ ����� ����� true, �������� Time.timeScale �� 0 (����� ���������������);
        // ����� ������ ����� ����� false, ���������� Time.timeScale �� 1 (����� ���� ���������)
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

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