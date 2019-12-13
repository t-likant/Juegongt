using UnityEngine;
using System;
using NPC.Enemy;
using NPC.Ally;
using UnityEngine.UI;


public class MyHero : MonoBehaviour
{
    public float velHeroe = 4.0f; // VELOCIDAD ALEATORIA DEL HEROE

    VillagerStruct datosAldeano;
    ZombieStruct datosZombie;
    bool contactoZombi;
    bool contactoAldeano;
    public Text mensajito;
    public float force = 450f;
    public bool canJump = false;

    private void Start()
    {
        var mensajitos = FindObjectsOfType<Text>(); // LISTA PARA DETECTAR EL GAME OVER
        foreach (var item in mensajitos)
        {
            if (item.name == "GAME OVER")
            {
                mensajito = item; // ASIGNA EL TEXTO EN EL CANVAS CON EL GAME OVER
                mensajito.text = ""; // DESACTIVA EL TEXTO CAMVAS DEL GAME OVER
            }
        }

    }

    void Update() // CONDICIONES PARA MENSAJES POR CONTACTO
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            canJump = false;
        }

        
    }
    
    void Jump()
    {
        if (canJump)
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * force);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (transform.position.y <= -0.35)
            canJump = true;

        if (collision.transform.name == "Zombie")
        {
            Debug.Log("Game Over");
            mensajito.text = "GAME OVER";
            // aqui saca el game over cuando lo tocan
            Time.timeScale = 0; // EL TIMESCALE LO VUELVE CERO PARA DETENER EL JUEGO CUANDO UN ZOMBIE TOQUE AL HEROE

        }
    }
}