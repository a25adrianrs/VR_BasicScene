# Interactables
  Para este ejercicio crée tres objetos (el boton lo saque de los prefabs).
  Para ello copie una tabla y el canvar renombrandoles como **Personal Interactable**.
  Para los objetos excepto el botón les añadi una estructura parecida a la que cuentan los ejemplos es decir **Visuals,Highlight Interaction Affordance y un COL** y un **Atach_Transform** en el caso del objeto **Far Grabb** 
  todos usan el **Material : Interactable** en sus **Visuals**.
  
  - ## Poke
    En este basicamente lo que hice fue crear dentro del boton un objeto 3D **Cube** de color verde , el cual esta desactivado, y en su ***Interactable Events*** en **Select Entered** añadi dicho objeto el cual al pulsar el boton se ejecuta el **setActive** haciendo
    que aparezca en pantalla el cubo y que se desactive cuando se deja de pulsar el botón.
    
  - ## Grabb
    Creé un objeto Sphere el al cual añadi como en el **XR Grab Interactable** un **Select Mode: ***Multiple*****, lo cual permite que pueda ser agarrado con ambos mandos, el **Movement Type: ***Velocity Tracking***** ya al tratarse de un objeto esferico
    el movimiento resulta mas fluido y el **Use Dynamic Attach** seleccionado, en Visuals cuenta con un **Mesh Filter** de tipo **Base2** ya existente en los Assets.
    
  - ## Far Grabb
    Este fue un poco más complicado debido a su forma , al igual que la pelota cree un objeto de tipo **Cylinder** el cual añadi un **Mesh Filter** llamado ***s.00035*** ya existente en los Assets que le da una apariencia de **Boomerang** al objeto, cuenta con un **Selet Mode : ***Multiple****
    y un **Movement Type: ***Instantaneous*****, le añadi un **Throw Smoothing** para suavizar su lanzamiento parecido a los mostrados en los ejemplos, así como un Atach Transform como habia mencionado, tambie seleccione un **Far Atach Mode: ***Near*****, de ese modo el objeto
    puede ser llamado a distancia.
