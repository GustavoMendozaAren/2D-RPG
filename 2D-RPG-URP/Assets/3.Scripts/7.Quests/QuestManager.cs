using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Sinlgeton<QuestManager>
{
    [Header("QUEST")]
    [SerializeField] private Quest[] questDisponibles;

    [Header("INSPECTOR QUEST")]
    [SerializeField] private InspectorQuestDescripcion inspectorQuestPrefab;
    [SerializeField] private Transform inspectorQuestContenedor;

    [Header("PERONAJE QUEST")]
    [SerializeField] private PersonajeQuestDescripcion personajeQuestPrefab;
    [SerializeField] private Transform personajeQuestContenedor;

    private void Start()
    {
        CargarQuestEnInspector();
    }

    private void CargarQuestEnInspector()
    {
        for (int i = 0; i < questDisponibles.Length; i++)
        {
            InspectorQuestDescripcion nuevoQuest = Instantiate(inspectorQuestPrefab, inspectorQuestContenedor);
            nuevoQuest.ConfigurarQuestUI(questDisponibles[i]);
        }
    }

    private void AniadirQuestPorCompletar(Quest questPorCompletar)
    {
        PersonajeQuestDescripcion nuevoQuest = Instantiate(personajeQuestPrefab, personajeQuestContenedor);
        nuevoQuest.ConfigurarQuestUI(questPorCompletar);
    }

    public void AniadirQuest(Quest questPorCompletar)
    {
        AniadirQuestPorCompletar(questPorCompletar);
    }
}
