namespace NPC
{
    namespace Ally
    {
        using UnityEngine;
        using System.Collections;
        using NPC.Enemy;
        public struct VillagerStruct // ESTRUCTURA DEL ALDEANO
        {
            // Datos del aldeano
            public enum nombresAldeano
            {
                Alex, Bernardo, Carlos, David, Fernando, Gustavo, Jose, Maria, Jesus, Pedro,
                Camilo, Alberto, Leon, John, Camila, Layla, Emily, Claire, Marta, Sofia
            };
            public nombresAldeano nombreAldeano;
            public int edadAldeano;
            public enum estadosAldeano { Idle, Moving, Rotating, Running};
            public estadosAldeano estadoAldeano;
            public float velocidadAldeano;

            public static explicit operator ZombieStruct(VillagerStruct md1) // FUNCION QUE PERMITE LA CONVERSION EXPLICITA DE DATOS DE ALDEANO A ZOMBIE
            {
                ZombieStruct despuesStruct = new ZombieStruct();
                despuesStruct.edadZombi = md1.edadAldeano;
                despuesStruct.velocidadZombi = md1.velocidadAldeano;
                despuesStruct.colorZombi = Random.Range(0, 3);
                despuesStruct.gustoZombi = (ZombieStruct.gustosZombi)Random.Range(0, 5);
                
                return despuesStruct;
            }
        }
        public class MyVillager : NPCRegulator
        {
            public VillagerStruct datosAldeano; // ESTRUCTURA DE DATOS PARA EL ALDEANO
            float y;
            int frames;
            int frameActor;
            public GameObject zombieP;


            void Awake() // ASIGNAMOS LOS DATOS CORRESPONDIENTES AL ALDEANO
            {
                datosAldeano.nombreAldeano = (VillagerStruct.nombresAldeano)Random.Range(0, 20); // SELECTOR DE NOMBRES
                datosAldeano.edadAldeano = Random.Range(15, 101); // SELECTOR DE EDAD
                datosAldeano.velocidadAldeano = 3.7f;
                edad = datosAldeano.edadAldeano; // VARIABLE HEREDADA QUE AYUDA AL FUNCIONAMIENTO DE LAS FUNCIONES IGUALES DE AMBOS NPC
                velocidad = datosAldeano.velocidadAldeano; // VARIABLE HEREDADA QUE AYUDA AL FUNCIONAMIENTO DE LAS FUNCIONES IGUALES DE AMBOS NPC
                gameObject.GetComponentInChildren<TextMesh>().text = "Hola soy " + gameObject.GetComponent<MyVillager>().datosAldeano.nombreAldeano.ToString() + " y tengo " + gameObject.GetComponent<MyVillager>().datosAldeano.edadAldeano.ToString() + " años";
                
            }

            void Start()
            {
                StartCoroutine(EstadosComunes()); // CORRUTINA QUE ACTUALIZA LOS ESTADOS COMUNES DEL NPC 
                heroObject = GameObject.Find("Heroe");
                VerificarAgresor(); // PRIMER CALCULO DE OBJETOS EN LA ESCENA
                frameActor = Random.Range(60,200);
            }

            void OnDrawGizmos()
            {
                Gizmos.DrawLine(transform.localPosition, transform.localPosition + direction); // MUESTRA UNA PEQUEÑA LINEA QUE APUNTA A SU OBJETIVO MAS CERCANO (ZOMBIE)
            }

            void Update()
            {
                frames++;
                if(frames > frameActor)
                {
                    frames = 0;
                    VerificarAgresor(); // PRIMER CALCULO DE OBJETOS EN LA ESCENA
                }

                ActualizadorDeEstadoAldeano();
                mostrarMensaje();


                if (distanciaAZombi <= distanciaEntreObjetos) // CONDICIONAL QUE DETERMINA LA HUIDA
                {
                    HuirAgresor(datosAldeano);
                }
                    else // CONDICION POR DEFECTO PARA CUANDO SE COMPORTE NORMAL
                    {
                        ComportamientoNormal();
                    }
            }

            public void ActualizadorDeEstadoAldeano() // AYUDA ACTUALIZAR EL ESTADO DEL ALDEANO EN EL INSPECTOR
            {
                datosAldeano.estadoAldeano = (VillagerStruct.estadosAldeano)estadoActual;
            }

            public void mostrarMensaje() // MUESTRA EL MENSAJE CORRESPONDIENTE AL ALDEANO
            {
                if (distanciaAJugador <= distanciaEntreObjetos) // Muestra el mensaje del aldeano con el heroe cerca
                {
                    // MUESTRA EL MENSAJE COMO ALDEANO
                    gameObject.GetComponentInChildren<TextMesh>().text = "Hola soy " + gameObject.GetComponent<MyVillager>().datosAldeano.nombreAldeano.ToString() + " y tengo " + gameObject.GetComponent<MyVillager>().datosAldeano.edadAldeano.ToString() + " años";

                    gameObject.GetComponentInChildren<TextMesh>().transform.rotation = heroObject.transform.rotation; // PERMITE MOSTRAR DE FRENTE EL MENSAJE AL HEROE EN TODO MOMENTO

                    transform.eulerAngles = (new Vector3(0,180,0))+(heroObject.transform.eulerAngles);

                }
                else
                {
                    gameObject.GetComponentInChildren<TextMesh>().text = ""; // ELIMINA EL MENSAJE A GRAN DISTANCIA SIN TENER QUE DESACTIVAR EL GAMEOBJECT
                }
            }

            private void OnCollisionEnter(Collision collision) // CONTACTO QUE CONVIERTE ALDEANO A ZOMBI
            {
                if (collision.transform.name == "Zombie") // SI DETECTA COLISION COMO ZOMBI HACE EL CAST PARA VOLVERLO COMPLETAMENTE ZOMBIE
                {
                    ZombieStruct zombieStruct = gameObject.AddComponent<MyZombie>().datosZombie;
                    zombieStruct = (ZombieStruct)gameObject.GetComponent<MyVillager>().datosAldeano;


                    gameObject.name = "Zombie"; // LO RENOMBRA COMO ZOMBIE
                    StopAllCoroutines(); // DETIENE LAS CORRUTINAS
                    Destroy(gameObject.GetComponent<MyVillager>());
                }
            }
        }
    }
}