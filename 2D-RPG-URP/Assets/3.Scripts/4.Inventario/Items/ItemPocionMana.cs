using UnityEngine;

[CreateAssetMenu(menuName = "Items/Pocion Mana")]
public class ItemPocionMana : InventarioItem
{
    [Header("Pocion Info")]
    public float MpRestauracion;

    public override bool UsarItem()
    {
        if (Inventario.Instance.Personaje.PersonajeMana.SePuedeRestaurar)
        {
            Inventario.Instance.Personaje.PersonajeMana.RestaurarMana(MpRestauracion);
            return true;
        }
        return false;
    }
}

