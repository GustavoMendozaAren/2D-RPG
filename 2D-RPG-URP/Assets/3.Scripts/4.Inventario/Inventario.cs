using BayatGames.SaveGameFree;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : Sinlgeton<Inventario>
{
    [Header("Items")]
    [SerializeField] private InventarioAlmacen inventarioAlmacen;
    [SerializeField] private InventarioItem[] itemsInventario;
    [SerializeField] private Personaje personaje;
    [SerializeField] private int numeroDeSlots;

    public Personaje Personaje => personaje;
    public int NumeroDeSlots => numeroDeSlots;
    public InventarioItem[] ItemsInventario => itemsInventario;

    private readonly string INVENTARIO_KEY = "MiJuegoInventario";

    private void Start()
    {
        itemsInventario = new InventarioItem[numeroDeSlots];
        //SaveGame.DeleteAll();
        CargarInventario();
    }

    public void AniadirItem(InventarioItem itemPorAniadir, int cantidad) 
    {
        if (itemPorAniadir == null)
            return;

        // Verificacion en caso de tener un item similar en el inventario
        List<int> indexes = VereficarExistencias(itemPorAniadir.ID);
        if (itemPorAniadir.EsAcumulable) 
        {
            if (indexes.Count > 0) 
            {
                for (int i = 0; i < indexes.Count; i++)
                {
                    if (itemsInventario[indexes[i]].Cantidad < itemPorAniadir.AcumulacionMax)
                    {
                        itemsInventario[indexes[i]].Cantidad += cantidad;
                        if (itemsInventario[indexes[i]].Cantidad > itemPorAniadir.AcumulacionMax)
                        {
                            int diferencia = itemsInventario[indexes[i]].Cantidad - itemPorAniadir.AcumulacionMax;
                            itemsInventario[indexes[i]].Cantidad = itemPorAniadir.AcumulacionMax;
                            AniadirItem(itemPorAniadir, diferencia);
                        }

                        InventarioUI.Instance.DibujarItemEnInventario(itemPorAniadir, itemsInventario[indexes[i]].Cantidad, indexes[i]);
                        return;
                    }
                }
            }
        }

        if(cantidad<=0)
            { return; }

        if(cantidad > itemPorAniadir.AcumulacionMax)
        {
            AniadirItemEnSlotDisponible(itemPorAniadir, itemPorAniadir.AcumulacionMax);
            cantidad -= itemPorAniadir.AcumulacionMax;
            AniadirItem(itemPorAniadir, cantidad);
        }
        else
        {
            AniadirItemEnSlotDisponible(itemPorAniadir, cantidad);
        }

        GuardarInventario();

    }

    private List<int> VereficarExistencias(string itemID) 
    {
        List<int> indexDelItem = new List<int>();
        for (int i = 0; i < itemsInventario.Length; i++)
        {
            if (itemsInventario[i] != null)
            {
                if (itemsInventario[i].ID == itemID)
                {
                    indexDelItem.Add(i);
                }
            }
        }
        return indexDelItem;
    }

    public int ObtenerCantidadDeItems(string itemID)
    {
        List<int> indexes = VereficarExistencias(itemID);
        int cantidadTotal = 0;
        foreach (int index in indexes)
        {
            if (itemsInventario[index].ID == itemID)
            {
                cantidadTotal += itemsInventario[index].Cantidad;
            }
        }

        return cantidadTotal;
    }

    public void ConsumirItem(string itemID)
    {
        List<int> indexes = VereficarExistencias(itemID);
        if (indexes.Count > 0)
        {
            ElimiarItem(indexes[indexes.Count - 1]);
        }
    }

    private void AniadirItemEnSlotDisponible(InventarioItem item, int cantidad)
    {
        for (int i = 0; i < itemsInventario.Length; i++)
        {
            if (itemsInventario[i] == null)
            {
                itemsInventario[i] = item.CopiarItem();
                itemsInventario[i].Cantidad = cantidad;
                InventarioUI.Instance.DibujarItemEnInventario(item, cantidad, i);
                return;
            }
        }
    }

    private void ElimiarItem(int index)
    {
        itemsInventario[index].Cantidad--;
        if (itemsInventario[index].Cantidad <= 0)
        {
            itemsInventario[index].Cantidad = 0;
            itemsInventario[index] = null;
            InventarioUI.Instance.DibujarItemEnInventario(null, 0, index);
        }
        else
        {
            InventarioUI.Instance.DibujarItemEnInventario(itemsInventario[index], itemsInventario[index].Cantidad, index);
        }

        GuardarInventario();
    }

    public void MoverItem(int indexInicial, int indexFinal)
    {
        if(itemsInventario[indexInicial] == null || itemsInventario[indexFinal] != null)
            return;

        // Copiar item en slot final
        InventarioItem itemPorMover = itemsInventario[indexInicial].CopiarItem();
        itemsInventario[indexFinal] = itemPorMover;
        InventarioUI.Instance.DibujarItemEnInventario(itemPorMover, itemPorMover.Cantidad, indexFinal);

        // Borramos Item de Slot inicial
        itemsInventario[indexInicial] = null;
        InventarioUI.Instance.DibujarItemEnInventario(null, 0, indexInicial);

        GuardarInventario();
    }

    private void UsarItem(int index)
    {
        if (itemsInventario[index] == null)
            return;

        if (itemsInventario[index].UsarItem())
        {
            ElimiarItem(index);
        }
    }

    private void EquiparItem(int index)
    {
        if (ItemsInventario[index] == null)
        {
            return;
        }

        if (itemsInventario[index].Tipo != TiposDeItem.Armas)
        {
            return;
        }

        itemsInventario[index].EquiparItem();
    }

    private void RemoverItem(int index)
    {
        if (ItemsInventario[index] == null)
        {
            return;
        }

        if (itemsInventario[index].Tipo != TiposDeItem.Armas)
        {
            return;
        }

        itemsInventario[index].RemoverItem();
    }

    #region Guardado

    private  InventarioItem ItemExisteEnAlmacen(string ID)
    {
        for (int i = 0; i < inventarioAlmacen.Items.Length; i++)
        {
            if (inventarioAlmacen.Items[i].ID == ID)
            {
                return inventarioAlmacen.Items[i];
            }
        }

        return null;
    }

    private void GuardarInventario()
    {
        InventarioData dataGuardado = new InventarioData();
        dataGuardado.ItemsDatos = new string[numeroDeSlots];
        dataGuardado.ItemsCantidad = new int[numeroDeSlots];

        for (int i = 0; i < numeroDeSlots; i++)
        {
            if (itemsInventario[i] == null || string.IsNullOrEmpty(itemsInventario[i].ID))
            {
                dataGuardado.ItemsDatos[i] = null;
                dataGuardado.ItemsCantidad[i] = 0;
            }
            else
            {
                dataGuardado.ItemsDatos[i] = itemsInventario[i].ID;
                dataGuardado.ItemsCantidad[i] = itemsInventario[i].Cantidad;
            }
        }

        SaveGame.Save(INVENTARIO_KEY, dataGuardado);
    }

    private void CargarInventario()
    {
        if (SaveGame.Exists(INVENTARIO_KEY))
        {
            InventarioData dataCargado = SaveGame.Load<InventarioData>(INVENTARIO_KEY);
            for (int i = 0; i < numeroDeSlots; i++)
            {
                if (dataCargado.ItemsDatos[i] != null)
                {
                    InventarioItem itemAlamcen = ItemExisteEnAlmacen(dataCargado.ItemsDatos[i]);
                    if (itemAlamcen != null)
                    {
                        itemsInventario[i] = itemAlamcen.CopiarItem();
                        itemsInventario[i].Cantidad = dataCargado.ItemsCantidad[i];
                        InventarioUI.Instance.DibujarItemEnInventario(itemsInventario[i], itemsInventario[i].Cantidad, i);
                    }
                }
                else
                {
                    itemsInventario[i] = null;
                }
            }
        }
    }

    #endregion

    #region Eventos

    private void SlotInteraccionRespuesta(TipoDeInteraccion tipo, int index)
    {
        switch (tipo)
        {
            case TipoDeInteraccion.USar:
                UsarItem(index);
                break;
            case TipoDeInteraccion.Equipar:
                EquiparItem(index);
                break;
            case TipoDeInteraccion.Remover:
                RemoverItem(index);
                break;
        }
    }


    private void OnEnable()
    {
        InventarioSlot.EventoSlotInteraccion += SlotInteraccionRespuesta;
    }

    
    private void OnDisable()
    {
        InventarioSlot.EventoSlotInteraccion -= SlotInteraccionRespuesta;
    }

    #endregion
}
