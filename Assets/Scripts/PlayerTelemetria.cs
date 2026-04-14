using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public  class PlayerTelemetria : MonoBehaviour 
{
    public TextMeshProUGUI textoVelocidade;
    private Rigidbody rb;

    void Start()
    {
        // Pega o Rigidbody do propio objeto que esta alocado
        rb = GetComponent<Rigidbody>();

        // Boa prática: avisar se esqueceu de arrastar a UI no Inspector
        if (textoVelocidade == null)
            Debug.LogWarning("Texto de Velocidade não atribuído no Cubo!");
    }

    public static string FormatarVelocidadeKmH(float velocidadeMs)
    {
        float velocidadeKmh = velocidadeMs * 3.6f;
        return $"{velocidadeKmh:F2} km/h";
    }

    void Update()
    {
        if (rb != null && textoVelocidade != null)
        {
            float velMS = rb.linearVelocity.magnitude;

            //usamos nosso metodo utilitario para formatar o texto
            textoVelocidade.text = "Velocidade: " + FormatarVelocidadeKmH(velMS);
        }
    }
}
