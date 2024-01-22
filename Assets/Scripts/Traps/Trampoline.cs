using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float jumpForce; // Сила прыжка
    [SerializeField] private AudioClip jumpSound; // Звук прыжка

    private Animator anim; // Компонент аниматора

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Если столкнулся с игроком
        {
            Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce); // Применяем силу прыжка к игроку
            }

            SoundManager.instance.PlaySound(jumpSound); // Воспроизводим звук прыжка
            anim.SetTrigger("Jump");
        }
    }
}