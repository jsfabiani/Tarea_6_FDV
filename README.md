# Tarea_6_FDV

## Físicas 2D

### 1. Pruebas de física
Para las pruebas de física usamos un script sencillo: cuando detecta una colisión pinta el objeto controlado de verde, y cuando detecta un trigger pinta el otro objeto de rojo.

a. Ninguno de los objetos será físico.
![](https://github.com/jsfabiani/Tarea_6_FDV/blob/main/gifs/FDV_Tarea_6_1a.gif)
No hay interacción entre los objetos.

b. Un objeto tiene físicas y el otro no.
![](https://github.com/jsfabiani/Tarea_6_FDV/blob/main/gifs/FDV_Tarea_6_1b.gif)
Se activa el Collider2D. El objeto con físicas no puede moverse a través del otro objeto.

c. Ambos objetos tienen físicas.
![](https://github.com/jsfabiani/Tarea_6_FDV/blob/main/gifs/FDV_Tarea_6_1c.gif)
Se activa el Collider2D. El objeto con físicas empuja al otro objeto.

d. Ambos objetos tienen físcas y uno de ellos tiene 10 veces más masa que el otro.
![](https://github.com/jsfabiani/Tarea_6_FDV/blob/main/gifs/FDV_Tarea_6_1d.gif)
Se activa el Collider2D. El objeto con físicas empuja al otro objeto, pero con menos velocidad.

e. Un objeto tiene físicas y el otro es IsTrigger.
![](https://github.com/jsfabiani/Tarea_6_FDV/blob/main/gifs/FDV_Tarea_6_1e.gif)
Se activa el Trigger2D. Los objetos no tienen interaccion física.


f. Ambos objetos son físicos y uno de ellos está marcado como IsTrigger.
![](https://github.com/jsfabiani/Tarea_6_FDV/blob/main/gifs/FDV_Tarea_6_1f.gif)
Se activa el Trigger2D. Sin interacción física, igual que el caso anterior.

g. Uno de los objetos es cinemático.
![](https://github.com/jsfabiani/Tarea_6_FDV/blob/main/gifs/FDV_Tarea_6_1g.gif)
Se activa el Collider2D. El objeto con físicas no empuja al cinemático.


### 2. Incorporar elementos físicos.
Hemos incorporado cuatro elementos:

a. Una barrera infranqueable: el rectángulo naranja con Collider y Rigidbody en modo Kinematic, que bloquea objetos y no le afectan las físicas.

b. Zona de impulso: el hexágono verde, con un componente de Area Effector 2D. El Collider está marcado como Trigger para que no bloquee a los objetos.

c. Un objeto arrastrado por otro a una distancia fija: el círculo marrón, arrastrada por un Distance Joint 2D que hemos puesto en la esfera del jugador.

d. Objeto que sigue un comportamiento físico: el cuadrado amarillo, con un Rigidbody marcado como Dynamic.

e. Dos capas asignadas a diferentes objetos: hemos creado otra capa "Ignore Collision," y hemos puesto a la esfera que es arrastrada en esa capa. En los colliders de los otros objetos, hemos marcado que ignoren esa capa en las colisiones.
![](https://github.com/jsfabiani/Tarea_6_FDV/blob/main/gifs/FDV_Tarea_6_1-2.gif)


### 3. Tilemaps
Tenemos tres tilemaps:
![](https://github.com/jsfabiani/Tarea_6_FDV/blob/main/screenshots/tileset_screenshot_1.png)

Un tilemap para el suelo. Tiene la tag "Floor" para interactuar con el personaje jugable. Tiene un Composite Collider 2D para que todas las tiles cuenten como un mismo objeto para colisiones.

![](https://github.com/jsfabiani/Tarea_6_FDV/blob/main/screenshots/tileset_screenshot_2.png)

Un tilemap para el decorado. Está en la capa "NoCollisions" para que ignore colisiones con el personaje jugable.

![](https://github.com/jsfabiani/Tarea_6_FDV/blob/main/screenshots/tileset_screenshot_3.png)

Un tilemap para obstáculos. Tiene también la tag "Floor." Tiene componente Tilemap Collider, cada obstáculo cuenta como un objeto individual para colisiones.

Los tilemaps están puestos en una capa de renderizado propia, que se renderiza antes que el resto de objetos. El orden de renderizado es: 1. suelo, 2. decorado, 3. obstáculos. Así los obstáculos están por delante del resto de tilemaps.


### 4. Físicas

#### a. Controlador

El controlador de personaje viene en el script ![PlayerController](https://github.com/jsfabiani/Tarea_6_FDV/blob/main/scripts/PlayerController.cs).

Para el movimiento horizontal, recoge el input del eje horizontal en Update. En ForcedUpdate, actualiza la velocidad del rigidbody.

```
rb2D.velocity = new Vector2(horizontalInput * speed * Time.fixedDeltaTime, rb2D.velocity.y);
```

Para el salto, definimos una función Jump, a la que asociamos una variable isJumping. Aplicamos un modo de fuerza impulso, por lo que no necesita estar en FixedUpdate.

```
private void Jump()
{
    rb2D.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);

    isJumping = true;
}
```

Como variable pública para el salto, definimos la altura máxima. En Start, calculamos el impulso usando cinemática básica. Hay que multiplicar el valor de la gravedad por la escala que se aplica al personaje.

```
jumpImpulse = rb2D.mass * Mathf.Sqrt(2.0f * Mathf.Abs(Physics2D.gravity.y) * rb2D.gravityScale * jumpHeight);
```

El salto se activa en Update, cuando isJumping está desactivado y se detecta el botón de salto. También activamos un bool del animator para el salto.

```
if (jumpInput && isJumping == false)
{
    Jump();
    animator.SetBool("firstJump", true);
}
```

IsJumping se desactiva cuando entra en colisión con un objeto marcado como "Floor." Además, anulamos el componente de la velocidad en y. En OnCollisionEnter2D:
```
if (collision.gameObject.tag == "Floor")
{          
    // Recharge Jumping and Double Jump upon touching a floor.
    isJumping = false;
    doubleJumpSpent = false;
    rb2D.velocity = new Vector2(rb2D.velocity.x, 0);

    // Exit jump animations
    animator.SetBool("firstJump", false);
    animator.SetBool("secondJump", false);
}
```

Para evitar bugs, también se mantiene desactivado en OnCollisionStay2D. 

IsJumping se activa como true cuando el personaje salta o cuado sale de colisión con un Floor en OnCollisionExit2D, para que no pueda saltar en el aire.
```
if (collision.gameObject.tag == "Floor")
{          
    // Enable isJumping so it can't jump again after falling from a platform.
    isJumping = true;

}
```



#### b. Plataformas

En OnCollisionEnter2D y OnCollisionExit2D, programamos el comportamiento con plataformas.

Cuando el personaje salta a una plataforma, la establecemos como parent del transform. Así, el personaje jugable se moverá junto con plataformas móviles.

```
if (collision.gameObject.layer==LayerMask.NameToLayer("Platform") | collision.gameObject.layer==LayerMask.NameToLayer("InvisiblePlatform"))
{
    this.transform.SetParent(collision.gameObject.transform, true);
}
```

Al salir de la plataforma, separamos el transform del personaje del de la plataforma.

```
if (collision.gameObject.layer==LayerMask.NameToLayer("Platform") | collision.gameObject.layer==LayerMask.NameToLayer("InvisiblePlatform"))
{
    this.transform.SetParent(null);
}
```

Para las plataformas invisibles: cuando entramos en contacto, editamos el color de la plataforma, poniendo el alpha a 1.

```
if (collision.gameObject.layer==LayerMask.NameToLayer("InvisiblePlatform"))
{
    Material mat = collision.gameObject.GetComponent<SpriteRenderer>().material;
    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 1.0f);
}
```

Al salir, volvemos a poner el alpha a 0.

```
if (collision.gameObject.layer==LayerMask.NameToLayer("InvisiblePlatform"))
{
    Material mat = collision.gameObject.GetComponent<SpriteRenderer>().material;
    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0.0f);
}
```

Para excluir las colisiones con una capa determinada, lo más sencillo es marcar esa capa específica como ExcludeLayer en el Collider2D del personaje. En el archivo hay una forma para ignorar colisiones por código comentada.

Las plataformas móviles están configuradas en el script ![MovingPlatforms](https://github.com/jsfabiani/Tarea_6_FDV/blob/main/scripts/MovingPlatform.cs). La plataforma se mueve entre dos puntos de forma regular. Toma como punto de partida el punto en el que esté la plataforma, y se dirige a la localización de un GameObject vacío. Cuando llega al final, cambia el punto de partida y de destino, realizando el mismo movimiento en sentido contrario.

Las plataformas invisibles están controladas por el script ![MakePlatformsInvisible](https://github.com/jsfabiani/Tarea_6_FDV/blob/main/scripts/MakePlatformsInvisible.cs), que se puede colocar a un GameObject vacío. Hace una lista con todos los GameObjects con la tag "Floor," y después busca entre ellos cuáles están en el Layer de InvisiblePlatform y pone el alpha a 0.


#### c. Mecánicas de recolección

Los objetos que recolectar están puestos como Trigger, y marcados con el tag "PowerUp." Cuando detectamos el trigger con el objeto, aumentamos la puntuación y actualizamos el contador en pantalla. Cuando conseguimos suficientes puntos, se activa una mejora de doble salto y se indica en la interfaz.

```
private void OnTriggerEnter2D(Collider2D trigger)
{
    // Collect power ups and increase the score
    if (trigger.gameObject.tag == "PowerUp")
    {
        score += 50.0f;
        scoreCounter.text = score + "$";
        Destroy(trigger.gameObject, 0.0f);

        // Enable Double Jump upon reaching a high score
        if (score >= doubleJumpScore)
        {
            doubleJumpEnabled = true;
            powerUpMessage.text = "Double Jump Unlocked!";
        }
    }
}
```

El doble salto funciona con su propio flag doubleJumpExpended, se podrá saltar siempre que esté desactivado. Una vez lo hemos desbloqueado, se activa cuando recibimos el input y ya nos encontramos en el aire. Entonces se borra la componente y de la velocidad, y se vuelve a ejecutar el Jump. Se activa el flag doubleJumpExpended, y se actualizan bools para las animaciones.

```
if (doubleJumpEnabled && doubleJumpSpent == false)
{
    if (jumpInput && isJumping == true)
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
        Jump();
        doubleJumpSpent = true;
        animator.SetBool("firstJump", false);
        animator.SetBool("secondJump", true);
    }
    
}
```

El doble salto se recarga al entrar en colisión con un objeto con la tag "Floor." A diferencia del salto normal, sólo se desactiva el flag cuando hacemos doble salto, no cuando dejamos de estar en contacto con el suelo.


### Gif de ejecución

![](https://github.com/jsfabiani/Tarea_6_FDV/blob/main/gifs/fisicas-gif-final.gif)
