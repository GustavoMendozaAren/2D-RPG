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

        if (controller.PerosnajeReferencia.GetComponent<PersonajeVida>().Derrotado)
        {
            return;
        }

        if (controller.EsTiempoDeAtacar() == false)
        {
            return;
        }

        if (controller.PersonajeEnRangoDeAtaque(controller.RangoDeAtaqueDetermindo))
        {
            if (controller.TipoAtaque == TiposDeAtaque.Embestida)
            {
                controller.AtaqueEmbestida(controller.Danio);
            }
            else
            {
                controller.AtaqueMelee(controller.Danio);
            }
            
            controller.ActualizarTiempoEntreAtaques();
        }
    }
}
