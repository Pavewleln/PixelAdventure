using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _lerpSpeed = 1.0f;

    private Vector3 offset;

    private Vector3 targetPos;

    private void Start()
    {
        if (_target == null) return;

        offset = transform.position - _target.position;
    }

    private void Update()
    {
        if (_target == null) return;

        targetPos = _target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, _lerpSpeed * Time.deltaTime);
    }
}
