using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogoManager : Sinlgeton<DialogoManager>
{
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private Image npcIcono;
    [SerializeField] private TextMeshProUGUI npcNombreTMP;
    [SerializeField] private TextMeshProUGUI npcConversacionTMP;

    public NPCInteraccion NPCDisponible { get; set; }

    private Queue<string> dialogosSecuencia;
    private bool dialogoAnimado;
    private bool despedidaMostrar;

    private void Start()
    {
        dialogosSecuencia = new Queue<string>();
    }

    private void Update()
    {
        if (NPCDisponible == null)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            ConfigurarPanel(NPCDisponible.Dialogo);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (despedidaMostrar)
            {
                AbrirCerrarPanelDialogo(false);
                despedidaMostrar = false;
                return;
            }

            if (NPCDisponible.Dialogo.ContieneInteraccionExtre)
            {
                UiManager.Instance.AbrirPanelInteraccion(NPCDisponible.Dialogo.InteraccionExtra);
                AbrirCerrarPanelDialogo(false);
                return;
            }

            if (dialogoAnimado)
            {
                ContinuarDialogo();
            }
        }
    }
    public void AbrirCerrarPanelDialogo(bool estado)
    {
        panelDialogo.SetActive(estado);
    }

    private void ConfigurarPanel(NPCDialogo npcDialogo)
    {
        AbrirCerrarPanelDialogo(true);
        CargarDialogosSecuencia(npcDialogo);

        npcIcono.sprite = npcDialogo.Icono;
        npcNombreTMP.text = $"{npcDialogo.Nombre}:";
        MostrarTextoConAnimacion(npcDialogo.Saludo);
    }

    private void CargarDialogosSecuencia(NPCDialogo npcDialogo)
    {
        if(npcDialogo.Conversacion == null || npcDialogo.Conversacion.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < npcDialogo.Conversacion.Length; i++) 
        {
            dialogosSecuencia.Enqueue(npcDialogo.Conversacion[i].Oracion);
        }
    }

    private void ContinuarDialogo()
    {
        if (NPCDisponible == null)
            return;

        if (despedidaMostrar)
            return;

        if(dialogosSecuencia.Count == 0)
        {
            string despedida = NPCDisponible.Dialogo.Despedida;
            MostrarTextoConAnimacion(despedida);
            despedidaMostrar = true;
            return;
            
        }

        string siguienteDialogo = dialogosSecuencia.Dequeue();
        MostrarTextoConAnimacion(siguienteDialogo);
    }

    private IEnumerator AnimarTexto(string oracion)
    {
        dialogoAnimado = false;
        npcConversacionTMP.text = "";
        char[] letras= oracion.ToCharArray();
        for (int i = 0; i < letras.Length; i++) 
        { 
            npcConversacionTMP.text += letras[i];
            yield return new WaitForSeconds(0.03f);
        }


        dialogoAnimado = true;
    }

    private void MostrarTextoConAnimacion(string oracion)
    {
        StartCoroutine(AnimarTexto(oracion));
    }

}
