using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : Sinlgeton<LootManager>
{
    [Header("CONFIGURACION")]
    [SerializeField] private GameObject panelLoot;

    public void MostrarLoot()
    {
        panelLoot.SetActive(true);
    }
}
