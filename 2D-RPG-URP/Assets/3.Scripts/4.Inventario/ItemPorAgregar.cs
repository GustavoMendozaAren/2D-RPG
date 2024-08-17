using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPorAgregar : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private InventarioItem inventarioItemReferencia;
    [SerializeField] private int cantidadPorAgregar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventario.Instance.AniadirItem(inventarioItemReferencia, cantidadPorAgregar);
            Destroy(gameObject);
        }
    }

}
