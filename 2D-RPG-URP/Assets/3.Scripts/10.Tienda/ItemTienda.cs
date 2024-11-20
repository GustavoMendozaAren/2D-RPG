using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTienda : MonoBehaviour
{
    [Header("CONFIGURACION")]
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;
    [SerializeField] private TextMeshProUGUI itemCosto;
    [SerializeField] private TextMeshProUGUI cantidadPorComprar;

    public ItemVenta ItemCargado { get; set; }

    public void ConfigurarItemEnVenta(ItemVenta itemVenta)
    {
        ItemCargado = itemVenta;
        itemIcono.sprite = itemVenta.item.Icono;
        itemNombre.text = itemVenta.item.Nombre;
        itemCosto.text = itemVenta.Costo.ToString();

    }
}
