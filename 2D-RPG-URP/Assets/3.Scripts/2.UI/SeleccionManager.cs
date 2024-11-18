using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionManager : MonoBehaviour
{
    public static Action<EnemigoInteraccion> EventoEnemigoSeleciconado;
    public static Action EventoObjetoNoSeleccionado;

    public EnemigoInteraccion EnemigoSeleciconado { get; set; }

    private Camera camara;

    private void Start()
    {
        camara = Camera.main;
    }

    private void Update()
    {
        SeleccionarEnemigo();
    }

    private void SeleccionarEnemigo()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(camara.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Enemigo"));

            if (hit.collider != null)
            {
                EnemigoSeleciconado = hit.collider.GetComponent<EnemigoInteraccion>();
                EnemigoVida enemigoVida = EnemigoSeleciconado.GetComponent<EnemigoVida>();
                if (enemigoVida.Salud > 0f)
                {
                    EventoEnemigoSeleciconado?.Invoke(EnemigoSeleciconado);
                }
                else
                {
                    LootManager.Instance.MostrarLoot();
                }
            }
            else
            {
                EventoObjetoNoSeleccionado?.Invoke();
            }
        }
    }
}
