# UI - Ataque de Enemigos
 Para este ejercicio añadi un boton que se llama **Create Enemies**, el cual hace que aparazecan los enemigos y ***"Ataquen"*** al jugador.

- Primero añadi a los tres prefabs existentes **"Cilinder, Cube y Sphere"** un **Sphere Collider** y marcado como **isTrigger** en los tres,
  tambien ***les asigne un Radio de 30*** , esto ***para controlar cuando el XR Origin entra dentro del radio de los Prefabs***.

- Luego creé un nuevo script llamado **EnemyAttack**, el cual asigne a los tres Prefabs, básicamente lo que hace el script es que nada mas
  instanciarse los Prefabs si el XR Origin entra dentro del Radio de los mismos se activaran.

  - Primero cree las variables para asignar y ***controlar la velocidad de los enemigos***, el ***Rigidbody***, un ***booleano para saber cuando el enemigo ataca***, y un ***transform para la posición de la camara del XR Origin***.
    ```csharp
    
    public float speed = 10f;

    private Rigidbody rb;
    // Booleano para saber si el enemigo está atacando
    private bool attacking = false;
    // Transform de la cámara del jugador (XR Origin)
    private Transform target;
    ``` 
.
  - Luego añadi un metodo **Awake()** para obtener el ***RigidBody*** y tambien ***congelar la rotación de los mismsos cuando el RigidBody choca***.
    ```csharp
     private void Awake()
    {
        // Obtenemos el rigidbody de los prefabs
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // No girar al chocar

    }
    ```
    
  - Añado dos metodos **OnTriggerEnter** y **OnTriggerExit** ambos se usarán para controlar cuando el XR Origin esta dentro o fuera del radio de los prefabs.
    
     - En el **OnTriggerEnter**, cuando se active este metodo obtenemos el **XR Origin**
       si este es distinto de ***NULL** entonces dentro de la variable **transform** se guarda ***la posición de la Camara del XR Origin***,
       se ***activa el ataque*** y se desactiva la ***gravedad** , esto último para que los objetos ***"salgan volando"*** hacia el objetivo, simulando un ataque.
       ```csharp
        private void OnTriggerEnter(Collider other){
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
          }}
       ```
    - En el **OnTriggerExit**, se hace exactamente lo contrario, una vez el **XR Origin** sale del radio de los Prefabs se ***desactiva el ataque***, el objetivo pasa a ser **NULL** de nuevo,
      y ***vuelve la gravedad a los prefabs***.
      ```csharp
          private void OnTriggerExit(Collider other){
        // Detectamos si el XR Origin ha salido del rango
        XROrigin xrOrigin = other.GetComponentInParent<XROrigin>();
        // Si el objetivo es la cámara del XR Origin y ha salido del rango
        if (xrOrigin != null && target == xrOrigin.Camera.transform)
        {
            // Desactivamos el modo de ataque
            attacking = false;
            // El objetivo pasa a ser nulo
            target = null;

            // Volvemos a activar gravedad para que caiga al suelo
            rb.useGravity = true;
        }}
      ```
- Por último esta el método **FixedUpdate**, este metodo si el **Ataque** es **true** y el **target** no es **NULL**,
  los objetos se lanzan a atacar a la posición de la cámara del XR Origin.
  ```csharp
  
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
  ``` 
