using UnityEngine;
using TMPro;
public class InspectorQuestDescripcion : QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI questRecompensa;
    public override void ConfigurarQuestUI(Quest quest)
    {
        base.ConfigurarQuestUI(quest);
        QuestCargado = quest;
        questRecompensa.text = $"-{quest.RecompensaOro} oro" + 
                               $"\n-{quest.RecompensaExp} exp" + 
                               $"\n-{quest.RecompensaItem.Item.Nombre} x{quest.RecompensaItem.Cantidad}";
    }

    public void AceptarQuest()
    {
        if (QuestCargado == null)
            return;

        QuestManager.Instance.AniadirQuest(QuestCargado);
        gameObject.SetActive(false);
    }
}
