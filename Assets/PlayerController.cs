using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5.0f;
    public float moveHorizontalAxis;
    public float moveVerticalAxis;
    public float rotationSpeed = 1.0f;
    private Rigidbody rb;


    void Start() {
        rb = GetComponent<Rigidbody>();
    }


     void Update()
    {
        moveHorizontalAxis = Input.GetAxis("Horizontal");
        moveVerticalAxis = Input.GetAxis("Vertical");

    }

    private void FixedUpdate()
    {
        //calculo de movimento frente e traz 
        Vector3 movimento = transform.forward * moveVerticalAxis * speed;

        // Mantemos o Y da gravidade para não flutuar
        movimento.y = rb.linearVelocity.y;

        rb.linearVelocity = movimento;

        //calculo de rotacao 
        Vector3 rotacao = new Vector3(0, moveHorizontalAxis * rotationSpeed , 0);
        rb.angularVelocity = rotacao;
    }

}
