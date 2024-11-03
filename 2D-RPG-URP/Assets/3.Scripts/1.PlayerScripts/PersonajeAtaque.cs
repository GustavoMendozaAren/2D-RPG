using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeAtaque : MonoBehaviour
{
    [Header("STATS")]
    [SerializeField] private PersonajeStats stats;

    [Header("POOLER")]
    [SerializeField] private ObjectPuller pooler;

    public Arma ArmaEquipada { get; private set; }

    public EnemigoInteraccion EnemigoObjetivo { get; private set; }

    public void EquiparArma(ItmeArma armaPorEquipar)
    {
        ArmaEquipada = armaPorEquipar.Arma;
        if (ArmaEquipada.Tipo == TipoArma.Magia)
        {
            pooler.CrearPooler(ArmaEquipada.ProyectilPrefab.gameObject);
        }

        stats.AniadirBonusPorArma(ArmaEquipada);
    }

    public void RemoverArma()
    {
        if (ArmaEquipada == null)
        {
            return;
        }

        if (ArmaEquipada.Tipo == TipoArma.Magia)
        {
            pooler.DestruirPooler();
        }

        stats.RemoverBonusPorArma(ArmaEquipada);
        ArmaEquipada = null;
    }

    private void EnemigoRangoSeleccionado(EnemigoInteraccion enemigoSeleccionado)
    {
        if (ArmaEquipada == null)
            return;

        if (ArmaEquipada.Tipo != TipoArma.Magia)
            return;

        if (EnemigoObjetivo == enemigoSeleccionado)
            return;

        EnemigoObjetivo = enemigoSeleccionado;
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true, TipoDeteccion.Rango);
    }

    private void EnemigoNoSeleccionado()
    {
        if (EnemigoObjetivo == null)
            return;

        EnemigoObjetivo.MostrarEnemigoSeleccionado(false, TipoDeteccion.Rango);
        EnemigoObjetivo = null;
    }

    private void EnemigoMeleeDetectado(EnemigoInteraccion enemigoDetectado)
    {
        if (ArmaEquipada == null)
            return;

        if (ArmaEquipada.Tipo != TipoArma.Melee)
            return;

        EnemigoObjetivo = enemigoDetectado;
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true, TipoDeteccion.Melee);
    }

    private void EnemigoMeleePerdido()
    {
        if (ArmaEquipada == null)
            return;

        if (EnemigoObjetivo == null)
            return;

        if (ArmaEquipada.Tipo != TipoArma.Melee)
            return;

        EnemigoObjetivo.MostrarEnemigoSeleccionado(false, TipoDeteccion.Melee);
        EnemigoObjetivo = null;
    }

    private void OnEnable()
    {
        SeleccionManager.EventoEnemigoSeleciconado += EnemigoRangoSeleccionado;
        SeleccionManager.EventoObjetoNoSeleccionado += EnemigoNoSeleccionado;

        PersonajeDetector.EventoEnemigoDetectado += EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido += EnemigoMeleePerdido;
    }

    private void OnDisable() 
    {
        SeleccionManager.EventoEnemigoSeleciconado -= EnemigoRangoSeleccionado;
        SeleccionManager.EventoObjetoNoSeleccionado -= EnemigoNoSeleccionado;

        PersonajeDetector.EventoEnemigoDetectado -= EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido -= EnemigoMeleePerdido;
    }

}
