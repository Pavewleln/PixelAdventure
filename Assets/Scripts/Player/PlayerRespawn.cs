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
            SoundManager.instance.PlaySound(checkpoint); // ¬оспроизводим звук точки сохранени€
            collision.GetComponent<Collider2D>().enabled = false; // ќтключаем коллайдер точки сохранени€, чтобы избежать повторных срабатываний
            collision.GetComponent<Animator>().SetTrigger("Activated"); // «апускаем анимацию активации точки сохранени€
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}