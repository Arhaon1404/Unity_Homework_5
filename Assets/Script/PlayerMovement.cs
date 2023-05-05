using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private static int LeftMovementBool = Animator.StringToHash("LeftMovementBool");
    private static int RightMovementBool = Animator.StringToHash("RightMovementBool");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    { 
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
            _spriteRenderer.flipX = true;
            _animator.SetBool(LeftMovementBool, true);
        }
        else
        {
            _animator.SetBool(LeftMovementBool, false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
            _spriteRenderer.flipX = false;
            _animator.SetBool(RightMovementBool, true);
        }
        else
        {
            _animator.SetBool(RightMovementBool, false);
        }
    }
}
