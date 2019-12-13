using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Enemy;
using NPC.Ally;


public class NPCRegulator : MonoBehaviour
{
    // VARIABLES REGULADORAS
    public float distanciaEntreObjetos = 5.0f;
    public int seMueve, selectorDireccional, edad, estadoActual;
    public float velocidad;
    public int vidaMostro;
    public GameObject heroObject;
   // public GameObject villagerObject;
   // public GameObject zombiObject;
    public Vector3 direction;
    Vector3 dPlayer;
    Vector3 dZombi;
    Vector3 dAldeano;
    public float distanciaAJugador;
    public float distanciaAZombi;
    public float distanciaAldeano;
    public GameObject[] AllGameObjects;

    public void ComportamientoNormal() // FUNCION QUE EJECUTAN AMBOS NPC
    {
        if (estadoActual == 0) {} // Idle

        else if (estadoActual == 1) // Moving
        {
            transform.position += transform.forward * velocidad * (15 / (float)edad) * Time.deltaTime;

        }

        else if (estadoActual == 2) // Rotating
        {
            if (selectorDireccional == 0) // Rotacion Positiva
            {
                transform.eulerAngles += new Vector3(0, Random.Range(10f, 150f) * Time.deltaTime, 0);
            }
            else if (selectorDireccional == 1) // Rotacion Negativa
            {
                transform.eulerAngles += new Vector3(0, Random.Range(-10f, -150f) * Time.deltaTime, 0);
            }
        }
    }

    public IEnumerator EstadosComunes() // CORRUTINA QUE EJECUTAN AMBOS NPC
    {
        while (true)
        {
            estadoActual = Random.Range(0, 3);
            if (estadoActual == 2) // Rotating
            {
                selectorDireccional = Random.Range(0, 2);
            }
            yield return new WaitForSeconds(3.0f); // Espera 3 segundos y cambia de comportamiento
        }
    }

    public void VerificarVictima() // VERIFICA LA DISTANCIA DE LOS OBJETOS EN LA ESCENA
    {
        dPlayer = heroObject.transform.position - transform.position; // CALCULA LA DISTANCIA ENTRE DOS PUNTOS
        distanciaAJugador = dPlayer.magnitude; // DEVUELVE LA MAGNITUD DE LA DISTANCIA

        foreach (MyVillager villagerObject in FindObjectsOfType<MyVillager>()) 
        { 
            dAldeano = villagerObject.transform.position - transform.position; // CALCULA LA DISTANCIA ENTRE DOS PUNTOS
            distanciaAldeano = dAldeano.magnitude; // DEVUELVE LA MAGNITUD DE LA DISTANCIA
            if (distanciaAldeano <= distanciaEntreObjetos) // DETIENE EL ANALISIS EN EL ALDEANO MAS CERCANO
            {
                break;
            } 
            
        }

    }

    public void PerseguirVictima(ZombieStruct zs) // INICIA LA PERSECUSION SI ENCUENTRA UNA VICTIMA CERCANA
    {
        //if (villagerObject == null) return;

        estadoActual = 3;
        if (distanciaAldeano <= distanciaEntreObjetos) // PERSIGUE PRIMERO A UN ALDEANO
        {
            direction = Vector3.Normalize(dAldeano); // BUSCA EL VECTOR DIRECCION QUE APUNTE AL OBJETO AL QUE SE DESEA LLEGAR
            transform.position += direction * zs.velocidadZombi * (15 / (float)zs.edadZombi) * Time.deltaTime; // TRANSFORMA LA POSICION PARA ACERCARSE A OTRO OBJETO
        }
        else if (distanciaAJugador <= distanciaEntreObjetos) // PERSIGUE AL HEROE SINO ENCUENTRA ALDEANOS CERCA
        {
            direction = Vector3.Normalize(dPlayer); // BUSCA EL VECTOR DIRECCION QUE APUNTE AL OBJETO AL QUE SE DESEA LLEGAR
            transform.position += direction * zs.velocidadZombi * (15 / (float)zs.edadZombi) * Time.deltaTime; // TRANSFORMA LA POSICION PARA ACERCARSE A OTRO OBJETO
        }
    }

    public void VerificarAgresor() // VERIFICA LA DISTANCIA DE LOS OBJETOS EN LA ESCENA
    {
        dPlayer = heroObject.transform.position - transform.position; // CALCULA LA DISTANCIA ENTRE DOS PUNTOS
        distanciaAJugador = dPlayer.magnitude; // DEVUELVE LA MAGNITUD DE LA DISTANCIA
        
        foreach (MyZombie zombiObject in FindObjectsOfType<MyZombie>())
        {
            dZombi = zombiObject.transform.position - transform.position; // CALCULA LA DISTANCIA ENTRE DOS PUNTOS
            distanciaAZombi = dZombi.magnitude; // DEVUELVE LA MAGNITUD DE LA DISTANCIA
            if (distanciaAZombi <= distanciaEntreObjetos)  // DETIENE EL ANALISIS EN EL ZOMBIE MAS CERCANO
            {
                break;
            }          
        }
    }

    public void HuirAgresor(VillagerStruct als) // FUNCION PARA ESCAPAR DEL ZOMBIE MAS CERCANO
    {
        //if (zombiObject == null) return;

        estadoActual = 3;
        direction = Vector3.Normalize(dZombi); // BUSCA EL VECTOR DIRECCION QUE APUNTE AL OBJETO AL QUE SE DESEA LLEGAR
        transform.position += -1 * direction * als.velocidadAldeano * (15 / (float)als.edadAldeano) * Time.deltaTime; // TRANSFORMA LA POSICION PARA ALEJARSE DE OTRO OBJETO
    }
}
