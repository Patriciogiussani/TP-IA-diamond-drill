using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public Transform player;         // El jugador a seguir
    public Vector3 offset = new Vector3(0, 10, 0);  // Distancia desde el jugador
    public float smoothSpeed = 5f;   // Suavidad del seguimiento

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
