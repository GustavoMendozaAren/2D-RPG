using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoArma
{
    Magia,
    Melee
}

[CreateAssetMenu (menuName = "Personaje/Arma")]
public class Arma : ScriptableObject
{
    [Header("CONFIGURACION")]
    public Sprite ArmaIcono;
    public Sprite IconoSkill;
    public TipoArma Tipo;
    public float Danio;

    [Header("ARMA MAGICA")]
    public Proyectil ProyectilPrefab;
    public float ManaRequerida;

    [Header("STATS")]
    public float ChanceCritico;
    public float ChanceBloqueo;
}
