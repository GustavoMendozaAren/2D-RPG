using UnityEngine;

public enum DireccionMov
{
    Horizontal,
    Vertical
}

public class WaypointMovimiento : MonoBehaviour
{
    
    [SerializeField] private float velocidad;

    public Vector3 PuntoPorMoverse => _waypoint.ObtenerPosicionMovimiento(puntoActualIndex);

    protected Waypoint _waypoint;
    protected Animator _animator;
    protected int puntoActualIndex;
    protected Vector3 ultimaPosicion;
    void Start()
    {
        puntoActualIndex = 0;
        _animator = GetComponent<Animator>();
        _waypoint = GetComponent<Waypoint>();
    }


    void Update()
    {
        MoverPersonaje();
        RotarPersonaje();
        RotarVerticla();
        if (ComprobarPuntoActualAlcanzado())
        {
            ActualizaRIndexMovimiento();
        }
    }

    private void MoverPersonaje()
    {
        transform.position = Vector3.MoveTowards(transform.position, PuntoPorMoverse, velocidad * Time.deltaTime);
    }

    private bool ComprobarPuntoActualAlcanzado()
    {
        float distanciaHAciaPuntoActual = (transform.position - PuntoPorMoverse).magnitude;
        if (distanciaHAciaPuntoActual < 0.1f)
        {
            ultimaPosicion = transform.position;
            return true;
        }

        return false;
    }

    private void ActualizaRIndexMovimiento()
    {
        if(puntoActualIndex == _waypoint.Puntos.Length - 1)
        {
            puntoActualIndex = 0;
        }
        else
        {
            if (puntoActualIndex < _waypoint.Puntos.Length - 1)
            {
                puntoActualIndex++;
            }
        }
    }

    protected virtual void RotarPersonaje()
    {
        
    }

    protected virtual void RotarVerticla()
    {

    }
}
