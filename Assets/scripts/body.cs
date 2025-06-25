using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body : MonoBehaviour
{
    public float speed = 5f;

    private Animation anim;

    void Start()
    {
        // Obtener el componente Animation del personaje
        anim = GetComponent<Animation>();
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;

        // Detectar entrada del jugador
        if (Input.GetKey(KeyCode.W)) direction = Vector3.forward;
        else if (Input.GetKey(KeyCode.S)) direction = Vector3.back;
        else if (Input.GetKey(KeyCode.A)) direction = Vector3.left;
        else if (Input.GetKey(KeyCode.D)) direction = Vector3.right;

        // Si se está presionando una tecla de movimiento
        if (direction != Vector3.zero)
        {
            // Girar al personaje en la dirección del movimiento
            transform.rotation = Quaternion.LookRotation(direction);

            // Mover al personaje
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            // Reproducir la animación de caminar si no se está reproduciendo ya
            if (!anim.IsPlaying("walk"))
            {
                anim.Play("walk");
            }
        }
    }
}