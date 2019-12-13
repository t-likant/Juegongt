using UnityEngine;

public class HeroCam : MonoBehaviour
{
    float mouseX; // Declaracion de mouse X
    float mouseY; // declaracion de mouse Y
    public bool InvertedMouse; // Condiciona la camara invertida
    void Update()
    {
        if (Time.timeScale == 0) return;
        FuncionCamara();
    }

    public void FuncionCamara()
    {
        Vector3 mousePosition = Input.mousePosition; // Nos permite analizar la posicion del mouse en pantalla

        mouseX += Input.GetAxis("Mouse X"); // La actualizacion es mas agradable y sin temblor

        if (mouseY <= 40 && mouseY >= -40) // Condicional para que el cuerpo no se de la vuelta y ponga el mundo de cabeza
        {

            if (InvertedMouse) // Si InvertedMouse es verdadero se invierte la camara en Y
            {
                mouseY += Input.GetAxis("Mouse Y"); // Arriba es Abajo y viceversa
            }
            else
            {
                mouseY -= Input.GetAxis("Mouse Y"); // Arriba es arriba y abajo es abajo en la camara
            }
        }
        else if (mouseY > 40) // Establece a mouseY en el limite positivo
        {
            mouseY = 40; // Limite positivo
        }
        else // Establece a mouseY en el limite negativo
        {
            mouseY = -40; // Limite negativo
        }
        transform.eulerAngles = new Vector3(mouseY, mouseX, 0); // Rotacion en X y Y
    }
}