using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiendaManager : MonoBehaviour
{
    [Header("CONFIGURACION")]
    [SerializeField] private ItemTienda itemTiendaPrefab;
    [SerializeField] private Transform panelContenedor;

    [Header("ITEMS")]
    [SerializeField] private ItemVenta[] itemsDisponibles;

    private void Start()
    {
        CargarItemsEnVenta();
    }

    private void CargarItemsEnVenta()
    {
        for (int i = 0; i < itemsDisponibles.Length; i++)
        {
            ItemTienda itemTienda = Instantiate(itemTiendaPrefab, panelContenedor);
            itemTienda.ConfigurarItemEnVenta(itemsDisponibles[i]);
        }
    }
}
