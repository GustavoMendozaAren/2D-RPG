using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Quest : ScriptableObject
{
    public static Action<Quest> EventoQuestCompletado;

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
    [HideInInspector] public bool QuestCompletadoCheck;
    [HideInInspector] public bool QuestAceptado;

    public void AniadirProgreso(int cantidad)
    {
        CantidadACtual += cantidad;
        VerificarQuestCompletada();
    }

    private void VerificarQuestCompletada()
    {
        if(CantidadACtual >= CantidadObjetivo)
        {
            CantidadACtual = CantidadObjetivo;
            QuestCompletado();
        }
    }

    private void QuestCompletado()
    {
        if(QuestCompletadoCheck)
            return;

        QuestCompletadoCheck = true;
        EventoQuestCompletado?.Invoke(this);
    }

    public void ResetQuest()
    {
        QuestCompletadoCheck = false;
        CantidadACtual = 0;
    }
}

[Serializable]
public class QuestRecompensaItem
{
    public InventarioItem Item;
    public int Cantidad;
}
