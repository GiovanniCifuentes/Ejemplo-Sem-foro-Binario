using System; // Importa el espacio de nombres para tipos b�sicos y funciones comunes.
using System.Threading; // Importa el espacio de nombres para el manejo de hilos.
using System.Windows.Forms; // Importa el espacio de nombres para crear aplicaciones de Windows Forms.
using static System.Windows.Forms.VisualStyles.VisualStyleElement; // Importa elementos visuales de Windows Forms.

namespace ejemploSemaforo // Define el espacio de nombres para la aplicaci�n.
{
    public partial class Form1 : Form // Define la clase del formulario principal que hereda de la clase 'Form'.
    {
        // Sem�foro binario inicializado a 1 (recurso disponible).
        private static SemaphoreSlim semaforo = new SemaphoreSlim(1, 1);
        private static int recursoCompartido = 0; // Recurso compartido que ser� accedido por los hilos.
        private int cantidadAccesos = 5; // N�mero de accesos permitidos por hilo.

        public Form1() // Constructor de la clase 'Form1'.
        {
            InitializeComponent(); // Inicializa los componentes del formulario.
            InicializarEtiquetas(); // Llama a la funci�n para inicializar las etiquetas del formulario.
        }

        private void InicializarEtiquetas() // M�todo para inicializar las etiquetas de estado en el formulario.
        {
            // Configura el texto inicial de las etiquetas.
            label1.Text = "Hilo 1: Esperando...";
            label2.Text = "Hilo 2: Esperando...";
            label3.Text = "Hilo 3: Esperando...";
        }

        private void Form1_Load(object sender, EventArgs e) // Evento que se ejecuta al cargar el formulario.
        {
            // Crea y empieza los hilos que ejecutar�n el m�todo 'AccederRecurso'.
            Thread hilo1 = new Thread(AccederRecurso);
            Thread hilo2 = new Thread(AccederRecurso);
            Thread hilo3 = new Thread(AccederRecurso);
            hilo1.IsBackground = true; // Configura el hilo 1 como de segundo plano.
            hilo2.IsBackground = true; // Configura el hilo 2 como de segundo plano.
            hilo3.IsBackground = true; // Configura el hilo 3 como de segundo plano.
            hilo1.Start(1); // Inicia el hilo 1 pasando el par�metro '1'.
            hilo2.Start(2); // Inicia el hilo 2 pasando el par�metro '2'.
            hilo3.Start(3); // Inicia el hilo 3 pasando el par�metro '3'.
        }

        private void AccederRecurso(object idHilo) // M�todo que accede al recurso compartido.
        {
            for (int i = 0; i < cantidadAccesos; i++) // Itera la cantidad de accesos permitidos.
            {
                // Actualiza la etiqueta para mostrar que el hilo est� esperando.
                ActualizarEtiqueta((int)idHilo, "Esperando...");

                semaforo.Wait(); // Espera a que el sem�foro est� disponible para acceder al recurso.
                try
                {
                    // Actualiza la etiqueta para mostrar que el hilo est� accediendo.
                    ActualizarEtiqueta((int)idHilo, "Accediendo...");
                    ActualizarBarraProgreso(); // Actualiza la barra de progreso en la interfaz gr�fica.

                    // Simula trabajo con una pausa de 1.5 segundos.
                    Thread.Sleep(1500);
                }
                finally
                {
                    // Actualiza la etiqueta para mostrar que el hilo est� terminando.
                    ActualizarEtiqueta((int)idHilo, "Terminando...");
                    semaforo.Release(); // Libera el sem�foro, permitiendo que otros hilos accedan al recurso.
                }
            }
            // Indica que el hilo ha completado su tarea.
            ActualizarEtiqueta((int)idHilo, "Completado.");
            ActualizarUI($"Hilo {(int)idHilo} ha terminado. Valor final: {recursoCompartido}");
        }

        private void ActualizarEtiqueta(int idHilo, string estado) // M�todo para actualizar la etiqueta correspondiente al estado del hilo.
        {
            // Verifica si es necesario invocar este m�todo en el hilo principal de la UI.
            if (InvokeRequired)
            {
                Invoke(new Action<int, string>(ActualizarEtiqueta), idHilo, estado);
            }
            else
            {
                // Actualiza el texto de la etiqueta correspondiente al hilo.
                if (idHilo == 1)
                    label1.Text = $"Hilo 1: {estado}";
                else if (idHilo == 2)
                    label2.Text = $"Hilo 2: {estado}";
                else if (idHilo == 3)
                    label3.Text = $"Hilo 3: {estado}";
            }
        }

        private void ActualizarBarraProgreso() // M�todo para actualizar la barra de progreso en la interfaz gr�fica.
        {
            // Verifica si es necesario invocar este m�todo en el hilo principal de la UI.
            if (InvokeRequired)
            {
                Invoke(new Action(ActualizarBarraProgreso));
            }
            else
            {
                recursoCompartido++; // Incrementa el valor del recurso compartido.
                // Actualiza el valor de la barra de progreso con el recurso compartido.
                progressBar1.Value = Math.Min(recursoCompartido, progressBar1.Maximum);
                // A�ade el valor actual del recurso al ListBox.
                listBox1.Items.Add($"Recurso compartido: {recursoCompartido}");
                listBox1.TopIndex = listBox1.Items.Count - 1; // Desplaza hacia abajo el ListBox para mostrar el �ltimo �tem.
            }
        }

        private void ActualizarUI(string mensaje) // M�todo para actualizar la interfaz gr�fica con un mensaje.
        {
            // Verifica si es necesario invocar este m�todo en el hilo principal de la UI.
            if (InvokeRequired)
            {
                Invoke(new Action<string>(ActualizarUI), mensaje);
            }
            else
            {
                listBox1.Items.Add(mensaje); // A�ade el mensaje al ListBox.
                listBox1.TopIndex = listBox1.Items.Count - 1; // Desplaza hacia abajo el ListBox para mostrar el �ltimo �tem.
            }
        }
    }
}
