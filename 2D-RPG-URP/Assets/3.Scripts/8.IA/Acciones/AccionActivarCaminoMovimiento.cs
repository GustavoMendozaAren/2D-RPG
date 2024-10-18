using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/Activar Camino Movimiento")]
public class AccionActivarCaminoMovimiento : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        if (controller.EnemigoMovimientoS == null)
        {
            return;
        }

        controller.EnemigoMovimientoS.enabled = true;
    }
}
