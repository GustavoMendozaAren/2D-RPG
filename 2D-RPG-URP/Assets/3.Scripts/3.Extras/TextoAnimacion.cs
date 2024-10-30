using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextoAnimacion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI danioTxt;

    public void EstablecerTexto(float cantidad)
    {
        danioTxt.text = cantidad.ToString();
    }
}
