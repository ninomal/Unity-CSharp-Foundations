using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Cor que o cubo vai assumir ao passar (Feedback visual)
    public Color corAtivada = Color.green;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se quem passou foi o Player
        if (other.CompareTag("Player"))
        {
            Debug.Log(">>> CHECKPOINT ATINGIDO! Enviando sinal...");

            // Aqui mudamos a cor do player para testar
            var renderer = other.GetComponent<Renderer>();
            if (renderer != null) renderer.material.color = corAtivada;

            // TODO: Aqui chamaremos a função que envia o dado para o Arduino
        }
    }

}
