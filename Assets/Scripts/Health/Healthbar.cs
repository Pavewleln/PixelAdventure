using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth; // ������ �� ��������� Health ���������
    [SerializeField] private Image totalhealthBar; // ������ �� ��������� Image ��� ����������� ������� ��������
    [SerializeField] private Image currenthealthBar; // ������ �� ��������� Image ��� ����������� �������� ��������

    private void Start()
    {
        totalhealthBar.fillAmount = playerHealth.currentHealth / 10; // ������������� ��������� �������� ������ ��������
    }

    private void Update()
    {
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10; // ��������� �������� �������� �������� �� ������ ��������
    }
}