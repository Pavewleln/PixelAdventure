using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpoint;
    private Transform currentCheckpoint;
    private Health playerHealth;
    // private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        // uiManager = FindObjectOfType<UIManager>();
    }

    public void RespawnCheck()
    {
        if (currentCheckpoint == null)
        {
            // uiManager.GameOver(); // Если текущая точка сохранения не установлена, вызываем метод для отображения экрана поражения
            return;
        }

        playerHealth.Respawn(); // Восстанавливаем здоровье игрока и сбрасываем анимацию
        transform.position = currentCheckpoint.position; // Перемещаем игрока в положение точки сохранения
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform; // Устанавливаем текущую точку сохранения
            SoundManager.instance.PlaySound(checkpoint); // Воспроизводим звук точки сохранения
            collision.GetComponent<Collider2D>().enabled = false; // Отключаем коллайдер точки сохранения, чтобы избежать повторных срабатываний
            collision.GetComponent<Animator>().SetTrigger("Activated"); // Запускаем анимацию активации точки сохранения
            playerHealth.Respawn();
        }
    }
}