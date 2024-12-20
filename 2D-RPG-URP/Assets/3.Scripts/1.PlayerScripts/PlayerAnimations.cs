using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private string layerIdle;
    [SerializeField] private string layerCaminar;
    [SerializeField] private string layerAtacar;

    private Animator _animator;
    private PlayerMovement _playerMovement;
    private PersonajeAtaque _personajeAtaque;

    private readonly int direccionX = Animator.StringToHash("X");
    private readonly int direccionY = Animator.StringToHash("Y");
    private readonly int derrotado = Animator.StringToHash("Derrotado");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _personajeAtaque = GetComponent<PersonajeAtaque>();
    }

    private void Update()
    {
        ActualizarLayers();

        // Detiene el ciclo
        if (!_playerMovement.EnMovimiento)
        {
            return;
        }

        // Actualiza la animacion
        _animator.SetFloat(direccionX, _playerMovement.DireccionMovimiento.x);
        _animator.SetFloat(direccionY, _playerMovement.DireccionMovimiento.y);
    }

    private void ActivarLayer(string nombreLayer)
    {
        // Desactiva los layers
        for (int i = 0; i < _animator.layerCount; i++)
        {
            _animator.SetLayerWeight(i, 0);
        }

        // Activa el layer necesario en el siguiente metodo
        _animator.SetLayerWeight(_animator.GetLayerIndex(nombreLayer), 1);
    }

    private void ActualizarLayers()
    {
        if (_personajeAtaque.Atacando)
        {
            ActivarLayer(layerAtacar);
        }
        else if (_playerMovement.EnMovimiento)
        {
            ActivarLayer(layerCaminar);
        }
        else
        {
            ActivarLayer(layerIdle);
        }
    }

    public void RevivirPersonaje() 
    {
        ActivarLayer(layerIdle);
        _animator.SetBool(derrotado, false);
    }

    private void PersonajeDerrotadoRespuesta() 
    {
        if (_animator.GetLayerWeight(_animator.GetLayerIndex(layerIdle)) == 1) 
        {
            _animator.SetBool(derrotado, true);
        }
        else
        {
            ActivarLayer(layerIdle);
            _animator.SetBool(derrotado, true);
        }
    }

    private void OnEnable()
    {
        PersonajeVida.EventoPersonajeDerrotado += PersonajeDerrotadoRespuesta;
    }

    private void OnDisable()
    {
        PersonajeVida.EventoPersonajeDerrotado -= PersonajeDerrotadoRespuesta;
    }
}
