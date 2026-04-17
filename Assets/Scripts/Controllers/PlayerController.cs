using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private IDriveInput _input;
    public float speed = 5.0f;
    public float rotationSpeed = 0.2f;

    // Variáveis de cache para os valores do input
    private float _throttle;
    private float _steering;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
        _input = GetComponent<IDriveInput>();
    }

     void Update()
    {
       _throttle = _input.Throttle;
       _steering = _input.Steering;
    }

    private void FixedUpdate()
    {
        //calculo de movimento frente e traz 
        Vector3 movimento = transform.forward * _throttle * speed;

        // Mantemos o Y da gravidade para não flutuar
        movimento.y = rb.linearVelocity.y;

        rb.linearVelocity = movimento;

        //calculo de rotacao 
        Vector3 rotacao = new Vector3(0, _steering * rotationSpeed, 0);
        rb.angularVelocity = rotacao;
    }
}
