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
    public static Action<float> EventoDaniorealizado;

    [Header("STATS")]
    [SerializeField] private PersonajeStats stats;

    [Header ("ESTADOS")]
    [SerializeField] private IAEstado estadoInicial;
    [SerializeField] private IAEstado estadoDefault;

    [Header("CONFIGURACION")]
    [SerializeField] private float rangoDeteccion;
    [SerializeField] private float rangoDeAtaque;
    [SerializeField] private float rangoDeEmbestida;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float velocidadEmbestida;
    [SerializeField] private LayerMask personajeLayerMask;

    [Header("ATAQUE")]
    [SerializeField] private float danio;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private TiposDeAtaque tipoAtaque;

    [Header("DEBUG")]
    [SerializeField] private bool mostrarDeteccion;
    [SerializeField] private bool mostrarRangoDeAtaque;
    [SerializeField] private bool mostrarRangoEmbestida;

    private float tiempoParaSiguienteAtaque;
    private BoxCollider2D _boxCollider2D;

    public Transform PerosnajeReferencia { get; set; }
    public IAEstado EstadoActual { get; set; }
    public EnemigoMovimiento EnemigoMovimientoS { get; set; }

    public float RangoDeteccion => rangoDeteccion;

    public float Danio => danio;
    public TiposDeAtaque TipoAtaque => tipoAtaque;
    public float VelocidadMovimiento => velocidadMovimiento;
    public LayerMask PeronsajeLayerMask => personajeLayerMask;
    public float RangoDeAtaqueDetermindo => tipoAtaque == TiposDeAtaque.Embestida ? rangoDeEmbestida : rangoDeAtaque;

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
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

    public void AtaqueEmbestida(float cantidad)
    {
        StartCoroutine(IEEmbestida(cantidad));
    }

    private IEnumerator IEEmbestida(float cantidad)
    {
        Vector3 personajePosicion = PerosnajeReferencia.position;
        Vector3 posicionInicial = transform.position;
        Vector3 direccionHaciaPersonaje = (personajePosicion - posicionInicial).normalized;
        Vector3 posicionDeAtaque = personajePosicion - direccionHaciaPersonaje * 0.5f;
        _boxCollider2D.enabled = false;

        float transicionDeAtaque = 0f;
        while (transicionDeAtaque <= 1f) 
        {
            transicionDeAtaque += Time.deltaTime * velocidadMovimiento;
            float interpolacion = (-Mathf.Pow(transicionDeAtaque, 2) + transicionDeAtaque) * 4f;
            transform.position = Vector3.Lerp(posicionInicial, posicionDeAtaque, interpolacion);
            yield return null;
        }

        if (PerosnajeReferencia != null)
        {
            AplicarDanioAlPersonaje(cantidad);
        }

        _boxCollider2D.enabled = true;
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
        EventoDaniorealizado?.Invoke(danioPorRealizar);
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
        if (mostrarRangoEmbestida)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, rangoDeEmbestida);
        }
    }
}
