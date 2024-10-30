using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PersonajeFX : MonoBehaviour
{
    [Header("POOLER")]
    [SerializeField] private ObjectPuller pooler;

    [Header("CONFIGURACION")]
    [SerializeField] private GameObject canvasTextoAnimacionPrefab;
    [SerializeField] private Transform canvasTextoPosicion;

    private void Start()
    {
        pooler.CrearPooler(canvasTextoAnimacionPrefab);
    }

    private IEnumerator IEMostarTexto(float cantidad)
    {
        GameObject nuevoTextoGO = pooler.ObtenerInstancia();
        TextoAnimacion texto = nuevoTextoGO.GetComponent<TextoAnimacion>();
        texto.EstablecerTexto(cantidad);
        nuevoTextoGO.transform.SetParent(canvasTextoPosicion);
        nuevoTextoGO.transform.position = canvasTextoPosicion.position;
        nuevoTextoGO.SetActive(true);

        yield return new WaitForSeconds(1f);
        nuevoTextoGO.SetActive(false);
        nuevoTextoGO.transform.SetParent(pooler.ListaContenedor.transform);
    }

    private void RespuestaDanioRecibido(float danio)
    {
        StartCoroutine(IEMostarTexto(danio));
    }

    private void OnEnable()
    {
        IAController.EventoDaniorealizado += RespuestaDanioRecibido;
    }

    private void OnDisable()
    {
        IAController.EventoDaniorealizado -= RespuestaDanioRecibido;
    }
}
