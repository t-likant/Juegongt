using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Enemy;
using NPC.Ally;
using UnityEngine.UI;

public class CharactersGenerator : MonoBehaviour
{
    static System.Random r = new System.Random(); // VARIABLE AUXILIAR PARA DECLARAR E INICIALIZAR EL READONLY
    public readonly int limiteMinimo = r.Next(5,10); // LINEA NATIVA PARA ASIGNAR UN LIMITE ALEATORIO AL READONLY
    const int limiteMaximo = 15; // CONSTANTE PARA LA GENERACION MAXIMA DE CUBOS
    int nAlly = 0, nEnemy = 0, limiteGenerado,generadorRandom; // VARIABLES PARA LA GENERACION DE CUBOS
    // HEROE VARIABLES Y FUNCION GENERADORA
    public GameObject cuboHeroe;
    int selector;
    public GameObject char_1;
    public GameObject char_2;
    public GameObject char_3;

    public GameObject heroe;
    public GameObject camaraHeroe;
    GameObject camara;
    Vector3 posHero;
    Vector3 camPos;
    GameObject enemys;
    GameObject allys;


    // VARIABLES DEL TEXTO DEL CANVAS
    public Text nEnemigos; 
    public Text nAliados;
    

    public void CreacionHeroe() // FUNCION GENERADORA DEL HEROE
    {
        // CREACION DEL HEROE
        posHero = new Vector3(Random.Range(-333.0f, -318.0f), 0.0f, Random.Range(-90.0f, -4.0f)); // CALCULA UNA POSICION
        heroe = GameObject.Instantiate(cuboHeroe, posHero, Quaternion.identity); // INSTANCIA AL HEROE EN ESCENA
        heroe.name = "Heroe"; // LO NOMBRA EN LA JERARQUIA DE UNITY
        heroe.AddComponent<MyHero>(); // LE AÑADE EL COMPONENTE DE HEROE CON SUS DATOS
        heroe.AddComponent<HeroMove>(); // LE AÑADE EL COMPONENTE DE MOVIMIENTO
        heroe.AddComponent<Rigidbody>().useGravity = true;
        heroe.GetComponent<Rigidbody>().drag = 4.0f;
    }

    // ZOMBIE VARIABLES Y FUNCION GENERADORA
    int colorZombie;
    public GameObject zombie;
    public GameObject zombieP;
    public GameObject mensaje;
    public GameObject mensajeZombi;

    public void CreacionZombie(GameObject enemigos) // FUNCION GENERADORA DE LOS ZOMBIES
    {
        Vector3 posZombi = new Vector3(Random.Range(-333.0f, -318.0f), 0.1f, Random.Range(-90.0f, -40.0f)); // CALCULA LA POSICION INICIAL DEL ZOMBIE EN ESCENA

        zombie = GameObject.Instantiate(zombieP, posZombi, Quaternion.identity); // INSTANCIA UN CUBO COMO ZOMBIE EN LA ESCENA
        zombie.name = "Zombie"; // LO NOMBRA EN LA JERARQUIA DE UNITY
        zombie.transform.SetParent(enemigos.transform); // ASIGNA AL ZOMBIE COMO HIJO DE UN GRUPO QUE CONTIENE SOLO A LOS ZOMBIES
        zombie.AddComponent<Rigidbody>().useGravity = true; // AÑADE UN COMPONENTE DE CUERPO RIGIDO AL GAMEOBJECT
        zombie.GetComponent<Rigidbody>().freezeRotation = true; // CONGELA LA ROTACION POR INERCIA DE FISICAS DE UNITY
        zombie.GetComponent<Rigidbody>().drag = 2.0f;
        zombie.AddComponent<BoxCollider>().size = new Vector3 (0.7f, 2f, 0.7f);
        zombie.GetComponent<BoxCollider>().center = new Vector3(0f, 0.96f, 0f);
        zombie.GetComponent<Transform>().localScale = new Vector3(1.0f, 1.2f, 1.0f); // ASIGNA UN COLOR PARA IDENTIFICAR A LOS ALDEANOS


        mensajeZombi = Instantiate(mensaje); // CREA UN OBJETO MENSAJE
        mensajeZombi.name = "Mensaje"; // LO NOMBRA EN LA JERARQUIA DE UNITY
        mensajeZombi.transform.SetParent(zombie.transform); // EL MENSAJE SE VUELVE HIJO DE ZOMBIE
        mensajeZombi.transform.localPosition = Vector3.zero; // UBICA EN EL ORIGEN DE ZOMBIE
        mensajeZombi.transform.localPosition = Vector3.up; // LO SUBE UNA UNIDAD EN Y       
        mensajeZombi.transform.localPosition += Vector3.up; // LO SUBE UNA UNIDAD EN Y       
        zombie.AddComponent<MyZombie>(); // AÑADA EL COMPONENTE ZOMBIE CON SUS DATOS
        
        
    }

    // ALDEANO VARIABLES Y FUNCION GENERADORA
    public GameObject aldeano;
    public GameObject mensajeAldeano;
    public void CreacionAldeano(GameObject aliados)
    {
        Vector3 posAldeano = new Vector3(Random.Range(-333.0f, -318.0f), 0.1f, Random.Range(-90.0f, -40.0f)); // ELIGE UNA POSICION ALEATORIA

        selector = Random.Range(0, 3);
        switch (selector)
        {
            case 0:
                aldeano = Instantiate(char_1, posAldeano, Quaternion.identity); // CREA LA FIGURA SOLICITADA
                break;
            case 1:
                aldeano = Instantiate(char_2, posAldeano, Quaternion.identity); // CREA LA FIGURA SOLICITADA
                break;
            case 2:
                aldeano = Instantiate(char_3, posAldeano, Quaternion.identity); // CREA LA FIGURA SOLICITADA
                break;
            default:
                break;
        }
        aldeano.GetComponent<Transform>().localScale = new Vector3(1.0f, 1.2f, 1.0f); // ASIGNA UN COLOR PARA IDENTIFICAR A LOS ALDEANOS
        aldeano.AddComponent<Rigidbody>().useGravity = true; // AÑADE UN COMPONENTE DE CUERPO RIGIDO AL GAMEOBJECT
        aldeano.GetComponent<Rigidbody>().freezeRotation = true; // CONGELA LA ROTACION POR INERCIA DE FISICAS DE UNITY
        aldeano.GetComponent<Rigidbody>().drag = 2.0f;
        aldeano.AddComponent<BoxCollider>().size = new Vector3(0.7f, 2f, 0.7f);
        aldeano.GetComponent<BoxCollider>().center = new Vector3(0f, 0.96f, 0f);
        aldeano.name = "Aldeano"; // NOMBRE DEL ALDEANO EN LA JERARQUIA
        aldeano.transform.SetParent(aliados.transform); // ALDEANO SE VUELVE HIJO DEL GRUPO DE ALIADOS

        mensajeAldeano = Instantiate(mensaje); // CREA UN OBJETO MENSAJE
        mensajeAldeano.name = "Mensaje"; // LO NOMBRA EN LA JERARQUIA DE UNITY
        mensajeAldeano.transform.SetParent(aldeano.transform); // LO VUELVE HIJO DE ALDEANO
        mensajeAldeano.transform.localPosition = Vector3.zero; // UBICA EN EL ORIGEN DE ALDEANO
        mensajeAldeano.transform.localPosition = Vector3.up; // LO SUBE UNA UNIDAD EN Y   
        mensajeAldeano.transform.localPosition += Vector3.up; // LO SUBE UNA UNIDAD EN Y   
        aldeano.AddComponent<MyVillager>(); // AÑADA EL COMPONENTE ALDEANO CON SUS DATOS
    }

    void Start()
    {
        limiteGenerado = Random.Range(limiteMinimo, limiteMaximo); // GENERA ALEATORIAMENTE EL LIMITE DE OBJETOS A CREAR
        for (int i = 0; i < limiteGenerado; i++) // CALCULA ALEATORIAMENTE EL NUMERO DE ZOMBIES Y ALDEANOS
        {
            generadorRandom = Random.Range(0, 2);
            if (generadorRandom == 0)
                nEnemy++;
            if (generadorRandom == 1)
                nAlly++;
        }
        // CREACION DEL HEROE
        CreacionHeroe();

        // CREACION DE LOS ZOMBIS
        enemys = new GameObject();
        enemys.name = "Enemys";
        for (int i = 0; i < nEnemy; i++) // CICLO QUE CREA UN ZOMBIE POR CADA ITERACION
        {
            CreacionZombie(enemys);
        }

        // CREACION DE LOS ALDEANOS
        allys = new GameObject();
        allys.name = "Allys";
        for (int i = 0; i < nAlly; i++) // CICLO QUE CREA UN ALDEANO POR CADA ITERACION
        {
            CreacionAldeano(allys);
        }
    }

    void Update() // ACTUALIZA EL CONTEO DE ALIADOS Y ENEMIGOS EN LA ESCENA
    {
        MyZombie[] zombieList = FindObjectsOfType<MyZombie>();
        foreach (var item in zombieList)
        {
            nEnemigos.text = zombieList.Length.ToString();
        }
        if (zombieList.Length == 0)
        {
            nEnemigos.text = "0";
        }

        MyVillager[] villagerList = FindObjectsOfType<MyVillager>();
        foreach (var item in villagerList)
        {
            nAliados.text = villagerList.Length.ToString();
        }
        if(villagerList.Length == 0)
        {
            nAliados.text = "0";
        }
    }
}