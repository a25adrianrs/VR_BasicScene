using UnityEngine;
using Unity.XR.CoreUtils;

public class EnemyAttack : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody rb;
    // Booleano para saber si el enemigo está atacando
    private bool attacking = false;
    // Transform de la cámara del jugador (XR Origin)
    private Transform target;

    private void Awake()
    {
        // Obtenemos el rigidbody de los prefabs
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // No girar al chocar

    }
    // Para este caso creamos un spherecollider para los prefabs de "Capsula" y "Cubo"
    // lo marcamos como trigger ya que lo usaremos para detectar cuando el XR Origin entra dentro de su "radio" de acción
    private void OnTriggerEnter(Collider other)
    {
        // Detectamos al XR Origin
        XROrigin xrOrigin = other.GetComponentInParent<XROrigin>();
        if (xrOrigin != null)
        {
            // El objetivo es la cámara del XR Origin
            target = xrOrigin.Camera.transform;
            // Activamos el modo de ataque
            attacking = true;

            // Desactivamos gravedad para que el enemigo pueda volar hacia el jugador
            rb.useGravity = false;


        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Detectamos si el XR Origin ha salido del rango
        XROrigin xrOrigin = other.GetComponentInParent<XROrigin>();
        // Si el objetivo es la cámara del XR Origin y ha salido del rango
        if (xrOrigin != null && xrOrigin.Camera.transform == target)
        {
            // Desactivamos el modo de ataque
            attacking = false;
            // El objetivo pasa a ser nulo
            target = null;

            // Volvemos a activar gravedad para que caiga al suelo
            rb.useGravity = true;


        }
    }

    private void FixedUpdate()
    {
        // Si el ataque y el objetivo no es nulo
        if (attacking && target != null)
        {
            // Movemos el enemigo hacia el objetivo
            Vector3 direction = (target.position - rb.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }
}
