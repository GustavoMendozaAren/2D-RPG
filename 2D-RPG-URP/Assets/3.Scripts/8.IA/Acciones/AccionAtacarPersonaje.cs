using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "IA/Acciones/Atacar Peronaje")]
public class AccionAtacarPersonaje : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        Atacar(controller);
    }

    private void Atacar(IAController controller)
    {
        if(controller.PerosnajeReferencia == null)
        {
            return;
        }

        if (controller.EsTiempoDeAtacar() == false)
        {
            return;
        }

        if (controller.PersonajeEnRangoDeAtaque(controller.RangoDeAtaque))
        {
            controller.AtaqueMelee(controller.Danio);
            controller.ActualizarTiempoEntreAtaques();
        }
    }
}
