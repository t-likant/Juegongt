using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public float valor = 100f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void RecibirDaño(float daño)
    {
        valor -= daño;
        if(valor < 0)
        {
            valor = 0;
        }
    }
}
