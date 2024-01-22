using UnityEngine;

public class EnemyFireballHolder : MonoBehaviour
{
    [SerializeField] private Transform enemy; // —сылка на компонент врага

    private void Update()
    {
        transform.localScale = enemy.localScale; // ”станавливаем масштаб этого объекта равным масштабу врага
    }
}