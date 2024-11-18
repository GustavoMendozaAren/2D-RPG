using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PersonajeAtaque : MonoBehaviour
{
    public static Action<float, EnemigoVida> EventoEnemigoDaniado;

    [Header("STATS")]
    [SerializeField] private PersonajeStats stats;

    [Header("POOLER")]
    [SerializeField] private ObjectPuller pooler;

    [Header("ATAQUE")]
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private Transform[] posicionesDeDisparo;

    public Arma ArmaEquipada { get; private set; }

    public EnemigoInteraccion EnemigoObjetivo { get; private set; }

    public bool Atacando { get; set; }

    private PersonajeMana _personajeMana;
    private int indexDireccionDeDisparo;
    private float tiempoParaSiguienteAtaque;

    private void Awake()
    {
        _personajeMana = GetComponent<PersonajeMana>();
       
    }

    private void Update()
    {
        ObtenerDireccionDisparo();

        if (Time.time > tiempoParaSiguienteAtaque)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (ArmaEquipada == null || EnemigoObjetivo == null)
                {
                    return;
                }

                UsarArma();
                tiempoParaSiguienteAtaque = Time.time + tiempoEntreAtaques;
                StartCoroutine(IEEstabelecerCondicionAtaque());
            }
        }
    }

    private void UsarArma()
    {
        if (ArmaEquipada.Tipo == TipoArma.Magia)
        {
            if (_personajeMana.ManaActual < ArmaEquipada.ManaRequerida)
            {
                return;
            }

            GameObject nuevoProyectil = pooler.ObtenerInstancia();
            nuevoProyectil.transform.localPosition = posicionesDeDisparo[indexDireccionDeDisparo].position;

            Proyectil proyectil = nuevoProyectil.GetComponent<Proyectil>();
            proyectil.InicializarProyectil(this);

            nuevoProyectil.SetActive(true);
            _personajeMana.UsarMana(ArmaEquipada.ManaRequerida);
        }
        else
        {
            float danio = ObtenerDanio();
            EnemigoVida enemigoVida = EnemigoObjetivo.gameObject.GetComponent<EnemigoVida>();
            enemigoVida.RecibirDamage(danio);
            EventoEnemigoDaniado?.Invoke(danio, enemigoVida);
        }
    }

    public float ObtenerDanio()
    {
        float cantidad = stats.Damage;
        if (UnityEngine.Random.value < stats.PorcentajeCritico / 100)
        {
            cantidad *= 2;
        }

        return cantidad; 
    }

    private IEnumerator IEEstabelecerCondicionAtaque()
    {
        Atacando = true;
        yield return new WaitForSeconds(0.3f);
        Atacando = false;
    }

    public void EquiparArma(ItmeArma armaPorEquipar)
    {
        ArmaEquipada = armaPorEquipar.Arma;
        if (ArmaEquipada.Tipo == TipoArma.Magia)
        {
            pooler.CrearPooler(ArmaEquipada.ProyectilPrefab.gameObject);
        }

        stats.AniadirBonusPorArma(ArmaEquipada);
    }

    public void RemoverArma()
    {
        if (ArmaEquipada == null)
        {
            return;
        }

        if (ArmaEquipada.Tipo == TipoArma.Magia)
        {
            pooler.DestruirPooler();
        }

        stats.RemoverBonusPorArma(ArmaEquipada);
        ArmaEquipada = null;
    }

    private void ObtenerDireccionDisparo()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.x > 0.1f)
        {
            indexDireccionDeDisparo = 1;
        }else if(input.x < 0)
        {
            indexDireccionDeDisparo = 3;
        }else if(input.y > 0.1f)
        {
            indexDireccionDeDisparo = 0;
        }else if (input.y < 0)
        {
            indexDireccionDeDisparo = 2;
        }

    }

    private void EnemigoRangoSeleccionado(EnemigoInteraccion enemigoSeleccionado)
    {
        if (ArmaEquipada == null)
            return;

        if (ArmaEquipada.Tipo != TipoArma.Magia)
            return;

        if (EnemigoObjetivo == enemigoSeleccionado)
            return;

        EnemigoObjetivo = enemigoSeleccionado;
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true, TipoDeteccion.Rango);
    }

    private void EnemigoNoSeleccionado()
    {
        if (EnemigoObjetivo == null)
            return;

        EnemigoObjetivo.MostrarEnemigoSeleccionado(false, TipoDeteccion.Rango);
        EnemigoObjetivo = null;
    }

    private void EnemigoMeleeDetectado(EnemigoInteraccion enemigoDetectado)
    {
        if (ArmaEquipada == null)
            return;

        if (ArmaEquipada.Tipo != TipoArma.Melee)
            return;

        EnemigoObjetivo = enemigoDetectado;
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true, TipoDeteccion.Melee);
    }

    private void EnemigoMeleePerdido()
    {
        if (ArmaEquipada == null)
            return;

        if (EnemigoObjetivo == null)
            return;

        if (ArmaEquipada.Tipo != TipoArma.Melee)
            return;

        EnemigoObjetivo.MostrarEnemigoSeleccionado(false, TipoDeteccion.Melee);
        EnemigoObjetivo = null;
    }

    private void OnEnable()
    {
        SeleccionManager.EventoEnemigoSeleciconado += EnemigoRangoSeleccionado;
        SeleccionManager.EventoObjetoNoSeleccionado += EnemigoNoSeleccionado;

        PersonajeDetector.EventoEnemigoDetectado += EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido += EnemigoMeleePerdido;
    }

    private void OnDisable() 
    {
        SeleccionManager.EventoEnemigoSeleciconado -= EnemigoRangoSeleccionado;
        SeleccionManager.EventoObjetoNoSeleccionado -= EnemigoNoSeleccionado;

        PersonajeDetector.EventoEnemigoDetectado -= EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido -= EnemigoMeleePerdido;
    }

}
