using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float jumpForce; // ���� ������
    [SerializeField] private AudioClip jumpSound; // ���� ������

    private Animator anim; // ��������� ���������

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ���� ���������� � �������
        {
            Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce); // ��������� ���� ������ � ������
            }

            SoundManager.instance.PlaySound(jumpSound); // ������������� ���� ������
            anim.SetTrigger("Jump");
        }
    }
}