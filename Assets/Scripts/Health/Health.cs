using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth; // ��������� �������� ��������
    public float currentHealth { get; private set; } // ������� �������� ��������
    private Animator anim; // ��������� Animator ��� ���������� ���������
    private bool dead; // ����, �����������, ����� �� ��������

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration; // ������������ ������� ������������ ����� ��������� �����
    [SerializeField] private int numberOfFlashes; // ���������� ������� ��� ������������
    private SpriteRenderer spriteRend; // ��������� SpriteRenderer ��� ���������� ������������ �������

    [Header("Components")]
    [SerializeField] private Behaviour[] components; // ������ �����������, ������� ����� ��������� ��� ������
    private bool invulnerable; // ����, �����������, �������� �� �������� �������� ���������� ����� ��������� �����

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound; // ���� ������
    [SerializeField] private AudioClip hurtSound; // ���� ��������� �����


    private Rigidbody2D body;

    private void Awake()
    {
        currentHealth = startingHealth; // ������������� ������� �������� ������ ����������
        anim = GetComponent<Animator>(); // �������� ��������� Animator
        spriteRend = GetComponent<SpriteRenderer>(); // �������� ��������� SpriteRenderer
        body = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return; // ���� �������� ��������, ������� �� ������
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth); // ��������� �������� �� �������� _damage

        if (currentHealth > 0) // ���� �������� ������ 0
        {
            anim.SetTrigger("Hit"); // ��������� �������� ��������� �����

            // ��������� ���� ������� � ���������
            Vector2 knockbackForce = new Vector2(0f, 15f); // �������� ��������, ���� ����������
            body.velocity = knockbackForce;

            StartCoroutine(Invunerability()); // ��������� ������ ������������
            SoundManager.instance.PlaySound(hurtSound); // ������������� ���� ��������� �����
        }
        else // ���� �������� ������ ��� ����� 0
        {
            if (!dead) // ���� �������� ��� �� �����
            {
                // ��������� ��� ������������� ����������
                foreach (Behaviour component in components)
                    component.enabled = false;

                anim.SetBool("Grounded", true); // ������������� �������� �� ����� � true
                anim.SetTrigger("Dead"); // ��������� �������� ������

                dead = true; // ������������� ���� ������ � true
                SoundManager.instance.PlaySound(deathSound); // ������������� ���� ������
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth); // ����������� �������� �� �������� _value
    }

    private IEnumerator Invunerability()
    {
        invulnerable = true; // ������������� ���� ������������ � true
        Physics2D.IgnoreLayerCollision(10, 11, true); // ���������� �������� � ���������� �������

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f); // ������������� ���� ������� �� �������
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2)); // ���� ��������� �����
            spriteRend.color = Color.white; // ������������� ���� ������� �� �����
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2)); // ���� ��������� �����
        }

        Physics2D.IgnoreLayerCollision(10, 11, false); // �������� �������� � ���������� �������
        invulnerable = false; // ������������� ���� ������������ � false
    }

    private void Deactivate()
    {
        gameObject.SetActive(false); // ��������� ������� ������
    }

    //Respawn
    public void Respawn()
    {
        AddHealth(startingHealth); // ��������������� �������� �� ���������� ��������
        anim.ResetTrigger("Dead"); // ���������� ������� ������ ��������
        anim.Play("Idle"); // ��������� �������� �����������
        // StartCoroutine(Invunerability()); // ��������� ������ ������������
        dead = false; // ������������� ���� ������ � false

        // �������� ��� ������������� ����������
        foreach (Behaviour component in components)
            component.enabled = true;
    }
}