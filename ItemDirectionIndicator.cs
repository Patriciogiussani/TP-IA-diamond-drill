using UnityEngine;
using UnityEngine.UI;

public class ItemDirectionIndicator : MonoBehaviour
{
    public Transform player;       // Jugador
    public Transform item;         // Objeto a buscar (ítem)
    public RectTransform arrowUI;  // Flecha en UI
    public Camera mainCamera;      // Cámara principal

    void Update()
    {
        if (player == null || item == null || arrowUI == null || mainCamera == null)
            return;

        // 1. Calculamos la dirección del jugador al ítem
        Vector3 dir = item.position - player.position;
        dir.y = 0; // ignorar altura

        // 2. Rotamos la flecha para que apunte hacia el ítem
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        arrowUI.rotation = Quaternion.Euler(0, 0, -angle);

        // 3. Convertimos la posición del ítem a coordenadas de pantalla
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(item.position);

        // 4. Verificamos si el ítem está dentro de la pantalla
        bool isOnScreen = screenPoint.z > 0 &&
                          screenPoint.x > 0 && screenPoint.x < 1 &&
                          screenPoint.y > 0 && screenPoint.y < 1;

        // 5. Activamos o desactivamos la flecha
        arrowUI.gameObject.SetActive(!isOnScreen);
    }
}
