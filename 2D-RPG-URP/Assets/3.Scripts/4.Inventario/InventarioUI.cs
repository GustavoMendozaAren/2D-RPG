using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventarioUI : Sinlgeton<InventarioUI>
{
    [Header("Panel Inventario Descripcion")]
    [SerializeField] private GameObject panelInvenarioDescripcion;
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;
    [SerializeField] private TextMeshProUGUI itemDescripcion;

    [SerializeField] private InventarioSlot slotPrefab;
    [SerializeField] private Transform contenedor;

    public int IndexSlotInicialPorMover { get; private set; }

    public InventarioSlot SlotSeleccionado { get; private set; }
    List<InventarioSlot> slotsDisponibles = new List<InventarioSlot>();

    void Start()
    {
        InicializarInventario();
        IndexSlotInicialPorMover = -1;
    }

    private void Update()
    {
        ActualizarSlotSeleccionado();
        if (Input.GetKeyDown(KeyCode.M))
        {
            if(SlotSeleccionado != null)
            {
                IndexSlotInicialPorMover = SlotSeleccionado.Index;
            }
        }
    }
    private void InicializarInventario() 
    {
        for (int i = 0; i < Inventario.Instance.NumeroDeSlots; i++)
        {
            InventarioSlot nuevoSlot = Instantiate(slotPrefab, contenedor);
            nuevoSlot.Index = i;
            slotsDisponibles.Add(nuevoSlot);
        }
    }

    private void ActualizarSlotSeleccionado()
    {
        GameObject goSeleccionado = EventSystem.current.currentSelectedGameObject;
        if(goSeleccionado == null)
            return;

        InventarioSlot slot = goSeleccionado.GetComponent<InventarioSlot>();
        if(slot != null)
        {
            SlotSeleccionado = slot;
        }
    }

    public void DibujarItemEnInventario(InventarioItem itemPorAniadir, int cantidad, int itemIndex)
    {
        InventarioSlot slot = slotsDisponibles[itemIndex];
        if(itemPorAniadir != null)
        {
            slot.ActivarSlotUI(true);
            slot.ActualizarSlot(itemPorAniadir, cantidad);
        }
        else
        {
            slot.ActivarSlotUI(false);
        }
    }

    private void ActualizarInventarioDescripcion(int index)
    {
        if (Inventario.Instance.ItemsInventario[index] != null)
        {
            itemIcono.sprite = Inventario.Instance.ItemsInventario[index].Icono;
            itemNombre.text = Inventario.Instance.ItemsInventario[index].Nombre;
            itemDescripcion.text = Inventario.Instance.ItemsInventario[index].Descripcion;
            panelInvenarioDescripcion.SetActive(true);
        }
        else
        {
            panelInvenarioDescripcion.SetActive(false);
        }
    }

    public void UsarItem() 
    {
        if(SlotSeleccionado != null)
        {
            SlotSeleccionado.SlotUsarItem();
            SlotSeleccionado.SeleccionarSlot();
        }
    }

    #region Evento

    private void SlotInteraccionRespuesta(TipoDeInteraccion tipo, int index)
    {
        if (tipo == TipoDeInteraccion.Click)
        {
            ActualizarInventarioDescripcion(index);
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
