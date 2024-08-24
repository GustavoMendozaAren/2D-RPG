using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonajeQuestDescripcion : QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI tareaObjetivo;
    [SerializeField] private TextMeshProUGUI recompensaOro;
    [SerializeField] private TextMeshProUGUI recompensaExp;

    [Header("ITEM")]
    [SerializeField] private Image recompensaItemIcono;
    [SerializeField] private TextMeshProUGUI recompensaItemCantidad;
    public override void ConfigurarQuestUI(Quest questPorCargar)
    {
        base.ConfigurarQuestUI(questPorCargar);
        recompensaOro.text = questPorCargar.RecompensaOro.ToString();
        recompensaExp.text = questPorCargar.RecompensaExp.ToString();
        tareaObjetivo.text = $"{questPorCargar.CantidadACtual}/{questPorCargar.CantidadObjetivo}";

        recompensaItemIcono.sprite = questPorCargar.RecompensaItem.Item.Icono;
        recompensaItemCantidad.text = questPorCargar.RecompensaItem.Cantidad.ToString();

    }

}
