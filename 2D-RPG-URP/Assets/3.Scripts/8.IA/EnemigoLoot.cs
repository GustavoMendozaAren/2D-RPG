using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoLoot : MonoBehaviour
{
    [Header("LOOT")]
    [SerializeField] private DropItem[] lootDisponible;

    private List<DropItem> lootSeleccionado = new List<DropItem>();

    public List<DropItem> LootSeleccionado => lootSeleccionado;

    private void Start()
    {
        SeleccionarLott();
    }

    private void SeleccionarLott()
    {
        foreach (DropItem item in lootDisponible)
        {
            float probabilidad = Random.Range(0,100);
            if (probabilidad <= item.PorcentajeDrop)
            {
                lootSeleccionado.Add(item);
            }
        }
    }
}
