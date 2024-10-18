using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Decisiones/Personaje En Rango de Ataque")]
public class DecisionPersonajeRangoDeAtaque : IADecision
{
    public override bool Decidir(IAController controller)
    {
        return EnRangoDeAtaque(controller);
    }

    private bool EnRangoDeAtaque(IAController controller)
    {
        if (controller.PerosnajeReferencia == null)
        {
            return false;
        }

        float distancia = (controller.PerosnajeReferencia.position - controller.transform.position).sqrMagnitude;

        if (distancia < Mathf.Pow(controller.RangoDeAtaque, 2))
        {
            return true;
        }

        return false;
    }
}
