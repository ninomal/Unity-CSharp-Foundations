using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;


public class SensorAlvo : MonoBehaviour {
    private bool colider;
    public TextMeshProUGUI meuTexto;

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
                meuTexto.text = "ALVO ATINGIDO!";
                meuTexto.color = Color.green; // Bônus: mude a cor do texto também!
            }
        }
        else
        {
            meuTexto.text = "ALVO NAO E UM PLAYER!";
            meuTexto.color = Color.blue; // Bônus: mude a cor do texto também!
        }
    }

}
