using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour {

    public float speed = 5.0f;
    public float moveHorizontalAxis;
    public float moveVerticalAxis;
    public float rotationSpeed = 0.2f;
    public TextMeshProUGUI textoVelocidade;
    private Rigidbody rb;


    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void AtualizarPainelUIVelocidade()
    {

        if (textoVelocidade != null)
        {
            //magnitude pega O TAMANHO DA VELOCIDADE ATUAL (m/s)
            float vel = rb.linearVelocity.magnitude;

            // "F2" formata para 2 casas decimais (ex: 5.42)
            textoVelocidade.text = "Velocidade: " + vel.ToString("F2") + " m/s";
        }
    }


     void Update()
    {
        moveHorizontalAxis = Input.GetAxis("Horizontal");
        moveVerticalAxis = Input.GetAxis("Vertical");
        AtualizarPainelUIVelocidade();

    }

    private void FixedUpdate()
    {
        //calculo de movimento frente e traz 
        Vector3 movimento = transform.forward * moveVerticalAxis * speed;

        // Mantemos o Y da gravidade para não flutuar
        movimento.y = rb.linearVelocity.y;

        rb.linearVelocity = movimento;

        //calculo de rotacao 
        Vector3 rotacao = new Vector3(0, moveHorizontalAxis * rotationSpeed, 0);
        rb.angularVelocity = rotacao;
    }

}
