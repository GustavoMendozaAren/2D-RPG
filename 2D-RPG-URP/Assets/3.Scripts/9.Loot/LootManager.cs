using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : Sinlgeton<LootManager>
{
    [Header("CONFIGURACION")]
    [SerializeField] private GameObject panelLoot;
    [SerializeField] private LootBoton lootBotonPrefab;
    [SerializeField] private Transform lootContenedor;

    public void MostrarLoot(EnemigoLoot enemigoLoot)
    {
        panelLoot.SetActive(true);
        if (ContenedorOcupado())
        {
            foreach (Transform hijo in lootContenedor.transform)
            {
                Destroy(hijo.gameObject);
            }
        }

        for (int i = 0; i< enemigoLoot.LootSeleccionado.Count; i++)
        {
            CargarLootAlPanel(enemigoLoot.LootSeleccionado[i]);
        }
    }

    public void CerrarPanel()
    {
        panelLoot.SetActive(false);
    }

    private void CargarLootAlPanel(DropItem dropItem)
    {
        if (dropItem.ItemRecogido)
        {
            return;
        }

        LootBoton loot = Instantiate(lootBotonPrefab, lootContenedor);
        loot.ConfigurarLootItem(dropItem);
        loot.transform.SetParent(lootContenedor);
    }

    private bool ContenedorOcupado()
    {
        LootBoton[] hijos = lootContenedor.GetComponentsInChildren<LootBoton>();
        if (hijos.Length>0)
        {
            return true;
        }

        return false;
    }
}
