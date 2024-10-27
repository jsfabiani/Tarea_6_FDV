# Tarea_6_FDV

## Físicas 2D

### 1. Pruebas de física
Hemos programado un script que cuando detecta una colisión pinta el objeto controlado de verde, y cuando detecta un trigger pinta el otro objeto de rojo.

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
