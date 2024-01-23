using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpoint;
    // private UIManager uiManager;

    private void Awake()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            SoundManager.instance.PlaySound(checkpoint); // ������������� ���� ����� ����������
            collision.GetComponent<Collider2D>().enabled = false; // ��������� ��������� ����� ����������, ����� �������� ��������� ������������
            collision.GetComponent<Animator>().SetTrigger("Activated"); // ��������� �������� ��������� ����� ����������
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}