using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target; // Цель, за которой следует камера
    [SerializeField] private float _lerpSpeed = 1.0f; // Скорость плавного перемещения камеры

    private Vector3 offset; // Смещение между камерой и целью

    private Vector3 targetPos; // Желаемая позиция камеры

    private void Start()
    {
        if (_target == null) return;

        offset = transform.position - _target.position; // Вычисляем смещение между камерой и целью
    }

    private void Update()
    {
        if (_target == null) return;

        targetPos = _target.position + offset; // Вычисляем желаемую позицию камеры

        // Плавно перемещаем камеру к желаемой позиции с использованием линейной интерполяции
        transform.position = Vector3.Lerp(transform.position, targetPos, _lerpSpeed * Time.deltaTime);
    }
}