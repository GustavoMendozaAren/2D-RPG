using UnityEngine;
public class Personaje : MonoBehaviour
{
    public PersonajeVida PersonajeVida { get; private set; }
    public PlayerAnimations PlayerAnimations { get; private set; }
    public PersonajeMana PersonajeMana { get; private set; }

    private void Awake()
    {
        PersonajeVida = GetComponent<PersonajeVida>();
        PlayerAnimations = GetComponent<PlayerAnimations>();
        PersonajeMana = GetComponent<PersonajeMana>();
    }

    public void RestaurarPersonaje() 
    {
        PersonajeVida.RestaurarPersonaje();
        PlayerAnimations.RevivirPersonaje();
        PersonajeMana.RestablecerMana();
    }
}
