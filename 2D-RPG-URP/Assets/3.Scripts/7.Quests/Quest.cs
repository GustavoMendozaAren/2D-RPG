using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Quest : ScriptableObject
{
    [Header("INFO")]
    public string Nombre;
    public string ID;
    public int CantidadObjetivo;

    [Header("DESCRIPCION")]
    [TextArea] public string Descripcion;

    [Header("RECOMPENSAS")]
    public int RecompensaOro;
    public float RecompensaExp;
    public QuestRecompensaItem RecompensaItem;

    [HideInInspector] public int CantidadACtual;
    [HideInInspector] public bool QuestCompletado;
}

[Serializable]
public class QuestRecompensaItem
{
    public InventarioItem Item;
    public int Cantidad;
}
