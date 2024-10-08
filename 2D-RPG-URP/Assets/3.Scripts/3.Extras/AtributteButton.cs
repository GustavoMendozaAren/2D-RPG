using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoAtributo 
{
    Fuerza,
    Inteligencia,
    Destreza
}

public class AtributteButton : MonoBehaviour
{
    public static Action<TipoAtributo> EventoAgregarAtributo;
    [SerializeField] private TipoAtributo tipo;

    public void AgregarAtributo() 
    {
        EventoAgregarAtributo?.Invoke(tipo);
    }


}
