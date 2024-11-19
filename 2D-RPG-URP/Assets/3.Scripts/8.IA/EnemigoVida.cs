using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoVida : VidaBase
{
    public static Action<float> EventoEnemigoDerrotado;

    [Header("VIDA")]
    [SerializeField] private EnimgoBarraVida barraVidaPrefab;
    [SerializeField] private Transform barraVidaPosicion;

    [Header("RASTROS")]
    [SerializeField] private GameObject rastros;

    private EnimgoBarraVida _enemigoBarraVidaCreada;
    private EnemigoInteraccion _enemigoInteraccion;
    private EnemigoMovimiento _enemigoMovimiento;
    private EnemigoLoot _enemigoLoot;
    private SpriteRenderer _spriteRederer;
    private BoxCollider2D _boxCollider2D;
    private IAController _controller;

    private void Awake()
    {
        _enemigoInteraccion = GetComponent<EnemigoInteraccion>();
        _enemigoMovimiento = GetComponent<EnemigoMovimiento>();
        _spriteRederer = GetComponent<SpriteRenderer>();
        _enemigoLoot = GetComponent<EnemigoLoot>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _controller = GetComponent<IAController>();
    }

    protected override void Start()
    {
        base.Start();
        CrearBarraVida();
    }

    private void CrearBarraVida()
    {
        _enemigoBarraVidaCreada = Instantiate(barraVidaPrefab, barraVidaPosicion);
        ActualizarBarraVida(Salud, saludMax);
    }

    protected override void ActualizarBarraVida(float vidaActual, float vidaMaxima)
    {
        _enemigoBarraVidaCreada.ModificarSalud(vidaActual, vidaMaxima);
    }

    protected override void PersonajeDerrotado()
    {
        DesatcivarEnemigo();
        EventoEnemigoDerrotado?.Invoke(_enemigoLoot.ExpGanada);
        QuestManager.Instance.AniadirProgreso("Mata10", 1);
        QuestManager.Instance.AniadirProgreso("Mata25", 1);
        QuestManager.Instance.AniadirProgreso("Mata50", 1);
    }

    private void DesatcivarEnemigo()
    {
        rastros.SetActive(true);
        _enemigoBarraVidaCreada.gameObject.SetActive(false);
        _spriteRederer.enabled = false;
        _enemigoMovimiento.enabled = false;
        _controller.enabled = false;
        _boxCollider2D.isTrigger = true;
        _enemigoInteraccion.DesactivarSpriteSeleccion();
    }
}
