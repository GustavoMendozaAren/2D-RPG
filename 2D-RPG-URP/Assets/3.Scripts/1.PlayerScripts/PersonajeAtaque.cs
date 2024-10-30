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
        {
            return;
        }

        if (ArmaEquipada.Tipo != TipoArma.Magia)
        {
            return;
        }

        if (EnemigoObjetivo == enemigoSeleccionado)
        {
            return;
        }

        EnemigoObjetivo = enemigoSeleccionado;
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true);
    }

    private void EnemigoNoSeleccionado()
    {
        if (EnemigoObjetivo == null)
        {
            return;
        }

        EnemigoObjetivo.MostrarEnemigoSeleccionado(false);
        EnemigoObjetivo = null;
    }

    private void OnEnable()
    {
        SeleccionManager.EventoEnemigoSeleciconado += EnemigoRangoSeleccionado;
        SeleccionManager.EventoObjetoNoSeleccionado += EnemigoNoSeleccionado;

    }

    private void OnDisable() 
    {
        SeleccionManager.EventoEnemigoSeleciconado -= EnemigoRangoSeleccionado;
        SeleccionManager.EventoObjetoNoSeleccionado -= EnemigoNoSeleccionado;
    }

}
