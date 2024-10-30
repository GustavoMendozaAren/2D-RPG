using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
public class Personaje : MonoBehaviour
{
    [SerializeField] private PersonajeStats stats;

    public PersonajeAtaque PersonajeAtaque { get; private set; }
    public PersonajeExp PersonajeExp { get; private set; }
    public PersonajeVida PersonajeVida { get; private set; }
    public PlayerAnimations PlayerAnimations { get; private set; }
    public PersonajeMana PersonajeMana { get; private set; }

    private void Awake()
    {
        PersonajeAtaque = GetComponent<PersonajeAtaque>();
        PersonajeVida = GetComponent<PersonajeVida>();
        PlayerAnimations = GetComponent<PlayerAnimations>();
        PersonajeMana = GetComponent<PersonajeMana>();
        PersonajeExp = GetComponent<PersonajeExp>();
    }

    public void RestaurarPersonaje() 
    {
        PersonajeVida.RestaurarPersonaje();
        PlayerAnimations.RevivirPersonaje();
        PersonajeMana.RestablecerMana();
    }

    private void AtributoRespuesta(TipoAtributo tipo) 
    {
        if (stats.PuntosDisponibles <= 0)
            return;

        switch (tipo) 
        {
            case TipoAtributo.Fuerza:
                stats.Fuerza++;
                stats.AniadirBonusPorAtributoFuerza();
                break;
            case TipoAtributo.Inteligencia:
                stats.Inteligencia++;
                stats.AniadirBonusPorAtributoInteligencia();
                break;
            case TipoAtributo.Destreza:
                stats.Destreza++;
                stats.AniadirBonusPorAtributoDestreza();
                break;
        }

        stats.PuntosDisponibles -= 1;
    }

    private void OnEnable()
    {
        AtributteButton.EventoAgregarAtributo += AtributoRespuesta;
    }

    private void OnDisable()
    {
        AtributteButton.EventoAgregarAtributo -= AtributoRespuesta;
    }
}
