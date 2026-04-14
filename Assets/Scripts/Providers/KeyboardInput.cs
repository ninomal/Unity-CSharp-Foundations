using UnityEngine;


// Aqui herdamos e assinamos o contrato com o a interface IdriveInput
public class KeyboardInput : MonoBehaviour, IDriveInput
{
    public float Throttle => Input.GetAxis("Vertical");
    public float Steering => Input.GetAxis("Horizontal");
}