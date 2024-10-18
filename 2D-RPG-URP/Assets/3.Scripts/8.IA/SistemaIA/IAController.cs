using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAController : MonoBehaviour
{
    [Header ("ESTADOs")]
    [SerializeField] private IAEstado estadoInicial;
    [SerializeField] private IAEstado estadoDefault;

    [Header("CONFIGURACION")]
    [SerializeField] private float rangoDeteccion;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private LayerMask personajeLayerMask;

    [Header("DEBUG")]
    [SerializeField] private bool mostrarDeteccion;
    public Transform PerosnajeReferencia { get; set; }

    public IAEstado EstadoActual { get; set; }

    public EnemigoMovimiento EnemigoMovimientoS { get; set; }

    public float RangoDeteccion => rangoDeteccion;
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

    private void OnDrawGizmos()
    {
        if (mostrarDeteccion)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
        }
    }
}
