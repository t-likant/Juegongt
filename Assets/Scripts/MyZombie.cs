namespace NPC
{
    namespace Enemy
    {
        using UnityEngine;
        using System.Collections;
        
        public struct ZombieStruct // ESTRUCTURA DEL ZOMBIE
        {
            // Datos del zombi
            public int colorZombi;
            public int edadZombi;
            public enum estadosZombi { Idle, Moving, Rotating, Pursuing };
            public enum gustosZombi { cerebro, corazon, higado, nariz, lengua };
            public gustosZombi gustoZombi;           
            public estadosZombi estadoZombi;
            public float velocidadZombi;
        }
        
        public class MyZombie : NPCRegulator
        {
            public ZombieStruct datosZombie; // SE CREA LA ESTRUCTURA DE LA CLASE ZOMBIE
            float y; // Para rotar mirando al personaje
            int frames;
            int frameActor;
            int vidaZombie = 3;

            public void Awake() // ASIGNAMOS UN VALOR A LAS VARIABLES DE LA ESTRUCTURA
            {
                datosZombie.gustoZombi = (ZombieStruct.gustosZombi)Random.Range(0, 5);
                datosZombie.colorZombi = Random.Range(0, 3);
                datosZombie.edadZombi = Random.Range(15, 101);
                datosZombie.velocidadZombi = 2.8f;
                edad = datosZombie.edadZombi; // VARIABLE HEREDADA QUE AYUDA AL FUNCIONAMIENTO DE LAS FUNCIONES IGUALES DE AMBOS NPC
                velocidad = datosZombie.velocidadZombi; // VARIABLE HEREDADA QUE AYUDA AL FUNCIONAMIENTO DE LAS FUNCIONES IGUALES DE AMBOS NPC
            }

            public void ActualizadorDeEstadoZombie() // FUNCION QUE AYUDA ACTUALIZAR EL ESTADO ZOMBIE EN EL INSPECTOR
            {
                datosZombie.estadoZombi = (ZombieStruct.estadosZombi)estadoActual;
            }

            public void mostrarMensaje() // FUNCION QUE MUESTRA EL MENSAJE CORRESPONDIENTE AL ZOMBIE
            {
                if (distanciaAJugador <= distanciaEntreObjetos) // Muestra el mensaje del aldeano con el heroe cerca
                {
                    gameObject.GetComponentInChildren<TextMesh>().text = "Waaaarrrr quiero comer " + gameObject.GetComponent<MyZombie>().datosZombie.gustoZombi.ToString(); // MUESTRA EL MENSAJE COMO ZOMBIE

                    gameObject.GetComponentInChildren<TextMesh>().transform.rotation = heroObject.transform.rotation; // PERMITE MOSTRAR DE FRENTE EL MENSAJE AL HEROE EN TODO MOMENTO

                    transform.eulerAngles = (new Vector3(0, 180, 0)) + (heroObject.transform.eulerAngles);

                }
                else
                {
                    gameObject.GetComponentInChildren<TextMesh>().text = ""; // ELIMINA EL MENSAJE A GRAN DISTANCIA SIN TENER QUE DESACTIVAR EL GAMEOBJECT
                }
            }

            void Start()
            {
                StartCoroutine(EstadosComunes()); // CORRUTINA QUE ACTUALIZA LOS ESTADOS COMUNES DEL NPC      
                heroObject = GameObject.Find("Heroe");
                VerificarVictima(); // PRIMER CALCULO DE OBJETOS EN LA ESCENA
                frameActor = Random.Range(60, 150);
                vidaMostro = vidaZombie;

            }

            void OnDrawGizmos()
            {
                Gizmos.DrawLine(transform.localPosition, transform.localPosition + direction); // MUESTRA UNA PEQUEÑA LINEA QUE APUNTA A SU OBJETIVO MAS CERCANO (ALDEANO) O (HEROE)
            }
            
            void Update()
            {
                if (Time.timeScale == 0) return; // DETIENE EL JUEGO SI ALCANZA AL HEROE

                frames++;
                if (frames > frameActor)
                {
                    frames = 0;
                    VerificarVictima(); // PRIMER CALCULO DE OBJETOS EN LA ESCENA
                }

                ActualizadorDeEstadoZombie();
                mostrarMensaje();


                if (distanciaAldeano <= distanciaEntreObjetos) // CONDICIONAL QUE DETERMINA EL COMPORTAMIENTO DE PERSECUSION HACIA ALDEANOS
                {
                    PerseguirVictima(datosZombie);
                }
                    else if (distanciaAJugador <= distanciaEntreObjetos) // CONDICIONAL QUE DETERMINA EL COMPORTAMIENTO DE PERSECUSION HACIA EL HEROE
                    {
                        PerseguirVictima(datosZombie);
                    }
                        else // CONDICIONAL QUE DETERMINA EL COMPORTAMIENTO NORMAL
                        {
                            ComportamientoNormal();
                        }
                if (vidaMostro == 0)
                    Destroy(gameObject);
            }
        }
    }
}