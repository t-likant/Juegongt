using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantallaBalas : MonoBehaviour
{
    public Text texto;
    public LogicaArma logicaArma;

    void Awake()
    {
        texto = GameObject.Find("Balas").GetComponent<Text>();
    }

    void Update()
    {
        texto.text = logicaArma.balasEnCartucho + "/" + logicaArma.tamañoDeCartucho
            + "\n" + logicaArma.balasRestantes;
    }
}
