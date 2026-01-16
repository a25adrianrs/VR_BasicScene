using UnityEngine;
using Unity.XR.CoreUtils;

[RequireComponent(typeof(Rigidbody))]
public class EnemyAttack : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody rb;
    private bool attacking = false;
    private XROrigin target;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // No girar al chocar
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detectamos al XR Origin
        XROrigin xrOrigin = other.GetComponentInParent<XROrigin>();
        if (xrOrigin != null)
        {
            target = xrOrigin;
            attacking = true;

            // Desactivamos gravedad para que el enemigo pueda volar hacia el jugador
            rb.useGravity = false;

            Debug.Log(name + " ha detectado al XR Origin, ¡atacando!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        XROrigin xrOrigin = other.GetComponentInParent<XROrigin>();
        if (xrOrigin != null && xrOrigin == target)
        {
            attacking = false;
            target = null;

            // Volvemos a activar gravedad para que caiga al suelo
            rb.useGravity = true;

            Debug.Log(name + " ha perdido al XR Origin, vuelve al suelo.");
        }
    }

    private void FixedUpdate()
    {
        if (attacking && target != null)
        {
            Vector3 direction = (target.transform.position - rb.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }
}
