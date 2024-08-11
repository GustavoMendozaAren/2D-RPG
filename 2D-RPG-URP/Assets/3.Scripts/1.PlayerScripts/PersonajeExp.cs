using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeExp : MonoBehaviour
{
    [SerializeField] private int nivelMax;
    [SerializeField] private int expBase;
    [SerializeField] private int valorIncremental;

    public int Nivel { get; set; }

    private float expActualTemp;
    private float expRequeridaNextLevel;

    void Start()
    {
        Nivel = 1;
        expRequeridaNextLevel = expBase;

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
        if (expObtenida > 0f) 
        {
            float expRestanteNuevoNivel = expRequeridaNextLevel - expActualTemp;
            if (expObtenida >= expRestanteNuevoNivel)
            {
                expObtenida -= expRestanteNuevoNivel;
                ActualizarNivel();
                AniadirExp(expObtenida);
            }
            else 
            {
                expActualTemp += expObtenida;
                if (expActualTemp == expRequeridaNextLevel) 
                {
                    ActualizarNivel();
                }
            }
        }
        ActualizarBarraExp();
    }

    private void ActualizarNivel() 
    {
        if (Nivel < nivelMax) 
        {
            Nivel++;
            expActualTemp = 0f;
            expRequeridaNextLevel *= valorIncremental;
        }
    }

    private void ActualizarBarraExp() 
    {
        UiManager.Instance.ActualizarExpPersonaje(expActualTemp, expRequeridaNextLevel);
    }
}
