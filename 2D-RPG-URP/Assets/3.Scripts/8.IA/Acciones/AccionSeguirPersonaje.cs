using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/Seguir Peronaje")]
public class AccionSeguirPersonaje : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        SeguirPersonaje(controller);
    }

    private void SeguirPersonaje(IAController controller)
    {
        if(controller.PerosnajeReferencia == null)
        {
            return;
        }

        Vector3 dirHacianPersonaje = controller.PerosnajeReferencia.position - controller.transform.position;
        Vector3 direccion = dirHacianPersonaje.normalized;

        float distancia = dirHacianPersonaje.magnitude;

        if(distancia >= 1.30f)
        {
            controller.transform.Translate(direccion * controller.VelocidadMovimiento * Time.deltaTime);
        }
    }
}
