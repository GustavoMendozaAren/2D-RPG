using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Decisiones/Detectar Personaje")]
public class DecisionDetectarPersonaje : IADecision
{
    public override bool Decidir(IAController controller)
    {
        return DetectarPersonaje(controller);
    }

    private bool DetectarPersonaje(IAController controller)
    {
        Collider2D perosnajeDetectado = Physics2D.OverlapCircle(controller.transform.position, controller.RangoDeteccion, controller.PeronsajeLayerMask);

        if(perosnajeDetectado != null)
        {
            controller.PerosnajeReferencia = perosnajeDetectado.transform;
            return true;
        }

        controller.PerosnajeReferencia = null;
        return false;
    }
}
