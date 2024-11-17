using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public enum TipoPersonaje
{
    Player,
    IA
}
public class PersonajeFX : MonoBehaviour
{
    [Header("POOLER")]
    [SerializeField] private ObjectPuller pooler;

    [Header("CONFIGURACION")]
    [SerializeField] private GameObject canvasTextoAnimacionPrefab;
    [SerializeField] private Transform canvasTextoPosicion;

    [Header("TIPO")]
    [SerializeField] private TipoPersonaje tipoPersonaje;

    private void Start()
    {
        pooler.CrearPooler(canvasTextoAnimacionPrefab);
    }

    private IEnumerator IEMostarTexto(float cantidad, Color color)
    {
        GameObject nuevoTextoGO = pooler.ObtenerInstancia();
        TextoAnimacion texto = nuevoTextoGO.GetComponent<TextoAnimacion>();
        texto.EstablecerTexto(cantidad, color);
        nuevoTextoGO.transform.SetParent(canvasTextoPosicion);
        nuevoTextoGO.transform.position = canvasTextoPosicion.position;
        nuevoTextoGO.SetActive(true);

        yield return new WaitForSeconds(1f);
        nuevoTextoGO.SetActive(false);
        nuevoTextoGO.transform.SetParent(pooler.ListaContenedor.transform);
    }

    private void RespuestaDanioRecibidoHaciaPlayer(float danio)
    {
        if(tipoPersonaje == TipoPersonaje.Player)
        {
            StartCoroutine(IEMostarTexto(danio, Color.black));
        }
    }

    private void RespuestaDanioHaciaEnemigo(float danio)
    {
        if(tipoPersonaje == TipoPersonaje.IA)
        {
            StartCoroutine(IEMostarTexto(danio, Color.red));
        }
    }

    private void OnEnable()
    {
        IAController.EventoDaniorealizado += RespuestaDanioRecibidoHaciaPlayer;
        PersonajeAtaque.EventoEnemigoDaniado += RespuestaDanioHaciaEnemigo;
    }

    private void OnDisable()
    {
        IAController.EventoDaniorealizado -= RespuestaDanioRecibidoHaciaPlayer;
        PersonajeAtaque.EventoEnemigoDaniado -= RespuestaDanioHaciaEnemigo;
    }
}
