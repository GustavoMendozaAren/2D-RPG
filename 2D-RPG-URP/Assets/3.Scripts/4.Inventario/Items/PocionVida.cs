using UnityEngine;

[CreateAssetMenu(menuName = "Items/Pocion Vida")]
public class PocionVida : InventarioItem
{
    [Header("Pocion Info")]
    public float HpRestauracion;

    public override bool UsarItem()
    {
        if (Inventario.Instance.Personaje.PersonajeVida.PuedeSerCurado) 
        {
            Inventario.Instance.Personaje.PersonajeVida.RestaurarSalud(HpRestauracion);
            return true;
        }
        return false;
    }

    public override string DescripcionItemCrafting()
    {
        string descripcion = $"Restaura {HpRestauracion} de Salud";
        return descripcion;
    }
}
