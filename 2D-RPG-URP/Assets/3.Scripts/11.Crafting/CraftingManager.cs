using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : Sinlgeton<CraftingManager>
{
    [Header("CONFIGURACION")]
    [SerializeField] private RecetaTarjeta recetaTarjetaPrefab;
    [SerializeField] private Transform recetaContenedor;

    [Header("RECETA INFO")]
    [SerializeField] private Image primerMaterialIcono;
    [SerializeField] private Image segundoMaterialIcono;
    [SerializeField] private TextMeshProUGUI primerMaterialNombre;
    [SerializeField] private TextMeshProUGUI segundoMaterialNombre;
    [SerializeField] private TextMeshProUGUI primerMaterialCantidad;
    [SerializeField] private TextMeshProUGUI segundoMaterialCantidad;
    [SerializeField] private TextMeshProUGUI recetaMendaje;
    [SerializeField] private Button buttonCraftear;

    [Header("RECETAS")]
    [SerializeField] private RecetaLista recetas;

    public Receta RecetaSeleccionada { get; set; }

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

    public void MostrarReceta(Receta receta)
    {
        RecetaSeleccionada = receta;
        primerMaterialIcono.sprite = receta.Item1.Icono;
        segundoMaterialIcono.sprite = receta.Item2.Icono;
        primerMaterialNombre.text = receta.Item1.Nombre;
        segundoMaterialNombre.text = receta.Item2.Nombre;
        primerMaterialCantidad.text = $"{Inventario.Instance.ObtenerCantidadDeItems(receta.Item1.ID)}/{receta.Item1CantidadRequerida}";
        segundoMaterialCantidad.text = $"{Inventario.Instance.ObtenerCantidadDeItems(receta.Item2.ID)}/{receta.Item2CantidadRequerida}";
    }
}
