using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Stats")]
public class PersonajeStats : ScriptableObject
{
    [Header("Stats")]
    public float Damage = 5f;
    public float Defensa = 2f;
    public float Velocidad = 5f;
    public float Nivel;
    public float ExpActual;
    public float ExpRequerida;
    [Range(0f, 100f)] public float PorcentajeCritico;
    [Range(0f, 100f)] public float PorcentajeBloqueo;

    [Header("Atributos")]
    public int Fuerza;
    public int Inteligencia;
    public int Destreza;

    [HideInInspector] public int PuntosDisponibles;

    public void AniadirBonusPorAtributoFuerza() 
    {
        Damage += 2;
        Defensa += 1;
        PorcentajeBloqueo += .03f;
    }

    public void AniadirBonusPorAtributoInteligencia() 
    { 
        Damage += 3;
        PorcentajeCritico += 0.2f;
    }

    public void AniadirBonusPorAtributoDestreza()
    {
        Velocidad += 0.1f;
        PorcentajeBloqueo = +0.05f;
    }

    public void ResetearValores() 
    {
        Damage = 5f;
        Defensa = 2f;
        Velocidad = 5f;
        Nivel = 1;
        ExpActual = 0f;
        ExpRequerida = 2f;
        PorcentajeCritico = 0f;
        PorcentajeBloqueo = 0f;

        Fuerza = 0;
        Inteligencia = 0;
        Destreza = 0;

        PuntosDisponibles = 0;
    }
}
