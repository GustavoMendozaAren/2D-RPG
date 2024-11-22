using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeExp : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Config")]
    [SerializeField] private int nivelMax;
    [SerializeField] private int expBase;
    [SerializeField] private int valorIncremental;

    private float expActual;
    private float expRequeridaNextLevel;

    void Start()
    {
        stats.Nivel = 1;
        expRequeridaNextLevel = expBase;
        stats.ExpRequerida = expRequeridaNextLevel;
        ActualizarBarraExp();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) 
        {
            AniadirExp(2f);
        }
    }

    public void AniadirExp(float expObtenida) 
    {
        if (expObtenida <= 0)
            return;

        expActual += expObtenida;
        stats.ExpActual = expActual;

        if (expActual == expRequeridaNextLevel)
        {
            ActualizarNivel();
        }
        else if(expActual > expRequeridaNextLevel)
        {
            float dif = expActual - expRequeridaNextLevel;
            ActualizarNivel();
            AniadirExp(dif);
        }

        stats.ExpTotal += expObtenida;
        ActualizarBarraExp();
    }

    private void ActualizarNivel() 
    {
        if (stats.Nivel < nivelMax) 
        {
            stats.Nivel++;
            stats.ExpActual = 0;
            expActual = 0;
            expRequeridaNextLevel *= valorIncremental;
            stats.ExpRequerida = expRequeridaNextLevel;
            stats.PuntosDisponibles += 3;
        }
    }

    private void ActualizarBarraExp() 
    {
        UiManager.Instance.ActualizarExpPersonaje(expActual, expRequeridaNextLevel);
    }

    private void RespuestaEnemigoDerrotado(float exp)
    {
        AniadirExp(exp);
    }

    private void OnEnable()
    {
        EnemigoVida.EventoEnemigoDerrotado += RespuestaEnemigoDerrotado;
    }

    private void OnDisable()
    {
        EnemigoVida.EventoEnemigoDerrotado -= RespuestaEnemigoDerrotado;
    }
}
