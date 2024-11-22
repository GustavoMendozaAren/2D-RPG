using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Receta
{
    public string Nombre;
    [Header("PRIMER MATERIAL")]
    public InventarioItem Item1;
    public int Item1CantidadRequerida;

    [Header("SEGUNDO MATERIAL")]
    public InventarioItem Item2;
    public int Item2CantidadRequerida;

    [Header("RESULTADO")]
    public InventarioItem ItemResultado;
    public int ItemResultadoCantidad;
}
