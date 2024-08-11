using UnityEngine;
public class Personaje : MonoBehaviour
{
    public PersonajeVida PersonajeVida { get; private set; }
    public PlayerAnimations PlayerAnimations { get; private set; }

    private void Awake()
    {
        PersonajeVida = GetComponent<PersonajeVida>();
        PlayerAnimations = GetComponent<PlayerAnimations>();
    }

    public void RestaurarPersonaje() 
    {
        PersonajeVida.RestaurarPersonaje();
        PlayerAnimations.RevivirPersonaje();
    }
}
