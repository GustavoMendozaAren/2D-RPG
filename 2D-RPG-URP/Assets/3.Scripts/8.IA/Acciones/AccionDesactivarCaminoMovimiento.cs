using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/Desactivar Camino Movimiento")]
public class AccionDesactivarCaminoMovimiento : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        if (controller.EnemigoMovimientoS == null)
        {
            return;
        }

        controller.EnemigoMovimientoS.enabled = false;
    }
}
