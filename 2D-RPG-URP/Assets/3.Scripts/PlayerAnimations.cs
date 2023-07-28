using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    private Animator _animator;
    private PlayerMovement _playerMovement;

    private readonly int direccionX = Animator.StringToHash("X");
    private readonly int direccionY = Animator.StringToHash("Y");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (!_playerMovement.EnMovimiento)
        {
            return;
        }

        _animator.SetFloat(direccionX, _playerMovement.DireccionMovimiento.x);
        _animator.SetFloat(direccionY, _playerMovement.DireccionMovimiento.y);
    }
}
