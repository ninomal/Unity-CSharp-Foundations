using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform alvo; //player
    public Vector3 offset = new Vector3(0, 3, -6);// Distância: 3 para cima, 6 para trás
    public float suavidade = 10.0f; // para a camera nao dar "trancos"

    void LateUpdate()
    {
        if (alvo == null) return;

        // 1 DEFINIMOS PARA ONDE A CAMERA DEVE IR
        Vector3 posicaoDejada = alvo.position + (alvo.rotation * offset);

        // 2 SUAVIZAMOS O MOVIMENTO
        Vector3 posicaoSuave = Vector3.Lerp(transform.position, posicaoDejada, suavidade * Time.deltaTime);

        transform.position = posicaoSuave;

        // 3 Fazemos a camera sempre "olhar" para o cubo
        transform.LookAt(alvo);
    }
}
