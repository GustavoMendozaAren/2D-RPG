using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : Sinlgeton<CraftingManager>
{
    [Header("CONFIGURACION")]
    [SerializeField] private RecetaTarjeta recetaTarjetaPrefab;
    [SerializeField] private Transform recetaContenedor;

    [Header("RECETAS")]
    [SerializeField] private RecetaLista recetas;

    private void Start()
    {
        CargarRecetas();
    }

    private void CargarRecetas()
    {
        for (int i = 0; i < recetas.Recetas.Length; i++)
        {
            RecetaTarjeta receta = Instantiate(recetaTarjetaPrefab, recetaContenedor);
            receta.ConfigurarRecetaTarjeta(recetas.Recetas[i]);
        }
    }
}
