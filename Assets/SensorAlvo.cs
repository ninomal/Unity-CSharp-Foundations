using System.Runtime.CompilerServices;
using UnityEngine;

public class SensorAlvo : MonoBehaviour {
    private bool colider;

    private void OnTriggerEnter(Collider other)
    {
    // pegamos o pincel do objeto
    MeshRenderer renderer = other.GetComponent<MeshRenderer>();

        //verefica se ele realmente tem um renderer evitar nullreference

        if (other.CompareTag("Player"))
        {
            if (renderer != null)
            {
                renderer.material.color = Color.blue;
                Debug.Log("Sucesso! O " + other.name + " agora é azul.");
            }
        }
        else
        {
            Debug.Log("Algo entrou no sensor, mas não era o Player.");
        }
    }

}
