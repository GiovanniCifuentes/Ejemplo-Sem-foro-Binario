# Ejemplo-Sem-foro-Binario
Semáforo binario para gestionar el acceso de tres hilos a un recurso compartido. Los hilos, en segundo plano, actualizan etiquetas de estado y una barra de progreso en la interfaz gráfica mientras esperan, acceden y liberan el recurso de manera sincronizada, simulando trabajo con pausas y actualizando la UI en cada paso.

Este es un ejemplo de una aplicación de Windows Forms en C# que utiliza múltiples hilos y un semáforo binario (SemaphoreSlim) para coordinar el acceso de los hilos a un recurso compartido. Aquí está el desglose:

# 1. Importaciones y Declaración del Formulario

* using System;: Importa el espacio de nombres base de C# que contiene clases fundamentales.
* using System.Threading;: Importa el espacio de nombres para la programación multihilo.
* using System.Windows.Forms;: Importa las clases para crear aplicaciones de Windows Forms.

# 2. Definición de la Clase y Variables

![image](https://github.com/user-attachments/assets/56357506-ab72-4b4c-b4e9-5184da81adb1)


* Form1 : Form: Define la clase del formulario que hereda de Form de Windows Forms.
* SemaphoreSlim semaforo: Un semáforo binario para controlar el acceso al recurso compartido. Inicializado a 1, permite un solo acceso al recurso a la vez.
* int recursoCompartido: Variable que representa el recurso que los hilos intentarán modificar.
* int cantidadAccesos: Número de veces que cada hilo intentará acceder al recurso compartido.

# 3. Constructor del Formulario

![image](https://github.com/user-attachments/assets/c0f5f346-8126-4a5a-97aa-0d6c2ecc6315)


* InitializeComponent();: Método generado automáticamente que inicializa los componentes del formulario.
* InicializarEtiquetas();: Método personalizado que configura las etiquetas iniciales.

# 4. Inicialización de Etiquetas

![image](https://github.com/user-attachments/assets/ccbe4d9b-ebd8-4823-89a9-c16a7dd1c8e3)


* label1, label2, label3: Configuran el texto inicial de tres etiquetas para mostrar el estado de los tres hilos.

# 5. Carga del Formulario y Creación de Hilos

![image](https://github.com/user-attachments/assets/19f048d2-35f4-4763-ae64-f5767d653810)


* Thread hilo1, hilo2, hilo3: Crea tres hilos que ejecutarán el método AccederRecurso.
* hiloX.IsBackground = true: Define los hilos como de fondo, lo que significa que terminarán cuando el programa principal termine.
* hiloX.Start(n): Inicia cada hilo con un identificador único (1, 2, 3).

# 6. Método para Acceder al Recurso

![image](https://github.com/user-attachments/assets/3c20c5fa-772e-4bb3-831d-fdc42297a672)
}

* ActualizarEtiqueta((int)idHilo, "Esperando...");: Actualiza la etiqueta correspondiente al hilo para mostrar que está esperando.
* semaforo.Wait();: Espera hasta que el semáforo esté disponible, bloqueando otros hilos.
* ActualizarEtiqueta((int)idHilo, "Accediendo...");: Muestra que el hilo ha obtenido acceso.
* ActualizarBarraProgreso();: Actualiza la barra de progreso y el ListBox para reflejar el nuevo estado del recurso compartido.
* Thread.Sleep(1500);: Simula un trabajo durante 1.5 segundos.
* semaforo.Release();: Libera el semáforo para permitir que otro hilo acceda al recurso.
* ActualizarEtiqueta y ActualizarUI: Actualiza la interfaz de usuario con el estado final del hilo.

# 7. Actualización de la Interfaz de Usuario
Actualización de Etiquetas

![image](https://github.com/user-attachments/assets/cad96e62-abf9-4b0a-acc6-127d0382a65c)


* InvokeRequired: Verifica si la llamada es desde un hilo diferente al hilo de la interfaz gráfica.
* Invoke: Utiliza un delegado para actualizar la interfaz de usuario desde el hilo adecuado.

#  Actualización de la Barra de Progreso

![image](https://github.com/user-attachments/assets/7a81b51b-6f9c-4d1a-a009-104a7403ace0)


* recursoCompartido++: Incrementa el valor del recurso compartido.
* progressBar1.Value: Actualiza la barra de progreso con el nuevo valor.
* listBox1.Items.Add: Añade un mensaje al ListBox con el valor actualizado.

# Actualización del ListBox

![image](https://github.com/user-attachments/assets/3bed5c69-0833-4823-948f-ed4868956c46)


* listBox1.Items.Add(mensaje): Añade un mensaje al ListBox para reflejar el estado final de un hilo.

# Ejecución
![image](https://github.com/user-attachments/assets/58615519-b218-4600-9198-5fbb8357e45c)
![image](https://github.com/user-attachments/assets/15ea586c-c52d-49bb-9a95-098ccd4bf7f4)
![image](https://github.com/user-attachments/assets/faaf2b45-177f-497b-bdd7-fbc209c7186e)






# Resumen
Este código crea tres hilos que intentan acceder a un recurso compartido controlado por un semáforo binario. Los métodos de actualización de la interfaz de usuario aseguran que los cambios se realicen de forma segura desde cualquier hilo. La barra de progreso y las etiquetas proporcionan una representación visual del estado y el progreso de cada hilo.


