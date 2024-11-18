using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

[Serializable]
public class DropItem
{
    [Header("INFORMACION")]
    public string Nombre;
    public InventarioItem Item;
    public int Cantidad;

    [Header("DROP")]
    [Range (0,100)] public float PorcentajeDrop;

    public bool ItemRecogido { get; set; }
}
