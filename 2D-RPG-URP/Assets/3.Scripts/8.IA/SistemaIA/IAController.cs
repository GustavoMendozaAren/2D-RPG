using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public enum TiposDeAtaque
{
    Melee,
    Embestida
}

public class IAController : MonoBehaviour
{
    [Header("STATS")]
    [SerializeField] private PersonajeStats stats;

    [Header ("ESTADOS")]
    [SerializeField] private IAEstado estadoInicial;
    [SerializeField] private IAEstado estadoDefault;

    [Header("CONFIGURACION")]
    [SerializeField] private float rangoDeteccion;
    [SerializeField] private float rangoDeAtaque;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private LayerMask personajeLayerMask;

    [Header("ATAQUE")]
    [SerializeField] private float danio;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private TiposDeAtaque tipoAtaque;

    [Header("DEBUG")]
    [SerializeField] private bool mostrarDeteccion;
    [SerializeField] private bool mostrarRangoDeAtaque;

    private float tiempoParaSiguienteAtaque;

    public Transform PerosnajeReferencia { get; set; }
    public IAEstado EstadoActual { get; set; }
    public EnemigoMovimiento EnemigoMovimientoS { get; set; }

    public float RangoDeteccion => rangoDeteccion;
    public float RangoDeAtaque => rangoDeAtaque;
    public float Danio => danio;
    public TiposDeAtaque TipoAtaque => tipoAtaque;
    public float VelocidadMovimiento => velocidadMovimiento;
    public LayerMask PeronsajeLayerMask => personajeLayerMask;

    private void Start()
    {
        EstadoActual = estadoInicial;
        EnemigoMovimientoS = GetComponent<EnemigoMovimiento>();
    }

    private void Update()
    {
        EstadoActual.EjecutarEstado(this);
    }

    public void CambiarEstado(IAEstado nuevoEstado)
    {
        if(nuevoEstado != estadoDefault)
        {
            EstadoActual = nuevoEstado;
        }
    }

    public void AtaqueMelee(float cantidad)
    {
        if (PerosnajeReferencia != null)
        {
            AplicarDanioAlPersonaje(cantidad);
        }
    }

    public void AplicarDanioAlPersonaje(float cantidad)
    {
        float danioPorRealizar = 0;
        if(UnityEngine.Random.value < stats.PorcentajeBloqueo / 100)
        {
            return;
        }

        danioPorRealizar = Mathf.Max(cantidad - stats.Defensa, 1f);
        PerosnajeReferencia.GetComponent<PersonajeVida>().RecibirDamage(danioPorRealizar);
    }

    public bool PersonajeEnRangoDeAtaque(float rango)
    {
        float distanciaHaciaPersonaje = (PerosnajeReferencia.position - transform.position).sqrMagnitude;
        if (distanciaHaciaPersonaje < Mathf.Pow(rango, 2))
        {
            return true;
        }

        return false;
    }

    public bool EsTiempoDeAtacar()
    {
        if (Time.time > tiempoParaSiguienteAtaque)
        {
            return true;
        }

        return false;
    }

    public void ActualizarTiempoEntreAtaques()
    {
        tiempoParaSiguienteAtaque = Time.time + tiempoEntreAtaques;
    }

    private void OnDrawGizmos()
    {
        if (mostrarDeteccion)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
        }
        if (mostrarRangoDeAtaque)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, rangoDeAtaque);
        }
    }
}
