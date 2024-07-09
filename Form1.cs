using System.ServiceProcess;

namespace SpoolerCleaner
{
    public partial class frmMain : Form
    {
        private readonly string strSpoolPath = Environment.SystemDirectory + "\\spool\\printers";
        private ServiceController scSpool = new ServiceController();
        private int iCounter = 0;
        private int iTotalFiles = 0;
#if DEBUGLOOP
        int iLoopCounter = 0;
#endif

        public frmMain()
        {
            InitializeComponent();
        }

        private enum SPOOLCLEANERSTATUS
        {
            AnalyzingFolder,
            ServiceStopped,
            CleaningSpool,
            CleanedSpool,
            ServiceStarted,
            Finished
        }

        /// <summary>
        /// Refresca el estado de las tareas durante la ejecución.
        /// </summary>
        /// <param name="scStatus">(SPOOLCLEANERSTATUS) Indica la etapa en la que se encuentra la ejecución.</param>
        /// <param name="iFiles">(int) (opc.) Número de ficheros totales o parciales para ciertas etapas.</param>
        private void RefreshStatus(SPOOLCLEANERSTATUS scStatus, int iFiles = 0)
        {
            switch (scStatus)
            {
                case SPOOLCLEANERSTATUS.AnalyzingFolder:
                    iTotalFiles = iFiles;
                    sslblMessage.Text = "Analizando Spool...";
                    sspbTask.Maximum = 100;
                    break;
                case SPOOLCLEANERSTATUS.ServiceStopped:
                    sslblMessage.Text = "Parando Servicio de Spool...";
                    break;
                case SPOOLCLEANERSTATUS.CleaningSpool:
                    sslblMessage.Text = $"Eliminando {iFiles} de {iTotalFiles}";
                    break;
                case SPOOLCLEANERSTATUS.CleanedSpool:
                    sslblMessage.Text = "Limpieza Completada. Intentando iniciar Servicio.";
                    sslblMessage.BackColor = Color.Yellow;
                    break;
                case SPOOLCLEANERSTATUS.ServiceStarted:
                    sslblMessage.Text = "Servicio Iniciado";
                    sslblMessage.BackColor = Color.LightGreen;
                    break;
                case SPOOLCLEANERSTATUS.Finished:
                    sslblMessage.Text = "Limpieza Terminada";
                    sslblMessage.BackColor = Color.LightGreen;
                    iCounter = iTotalFiles + 5;
                    break;
            }
            iCounter++;
            UpdatePercentage(iCounter);
        }

        /// <summary>
        /// Actualiza 
        /// </summary>
        /// <param name="iStep"></param>
        private void UpdatePercentage (int iStep)
        {
            sspbTask.Value = (int)Math.Ceiling((double)((iStep * 100.0) / (iTotalFiles + 6)));
        }

        /// <summary>
        /// Acciones a realizar cuando se presiona el botón de limpieza.
        /// </summary>
        private void btnClean_Click(object sender, EventArgs e)
        {
            
            DialogResult drContinue = MessageBox.Show("Se va a proceder a limpiar la cola de impresión. Durante la misma, no mande a imprimir a ninguna impresora.", "ADVERTENCIA", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (drContinue == DialogResult.Cancel)
            {
                MessageBox.Show("Se ha cancelado la limpieza.", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            bool bBreaker = true;

            try
            {
                //Fase 1
                DirectoryInfo diSpool = new DirectoryInfo(strSpoolPath);
                FileInfo[] fiFiles = diSpool.GetFiles("*", SearchOption.AllDirectories);
                RefreshStatus(SPOOLCLEANERSTATUS.AnalyzingFolder, fiFiles.Length);

                //Fase 2
                scSpool = new ServiceController("Spooler");
                while (bBreaker)
                {
#if DEBUGLOOP
                    iLoopCounter++;
                    MessageBox.Show($"PHASE 2 - Stopping Service. Loop No.: {iLoopCounter}");
#endif
                    if (scSpool.Status == ServiceControllerStatus.Stopped)
                    {
                        bBreaker = false;
                        break;
                    }
                    else if (scSpool.Status == ServiceControllerStatus.StopPending) continue;
                    scSpool.Stop();
                    scSpool.Refresh();
                    Thread.Sleep(1000);
                }
                bBreaker = true;
                RefreshStatus(SPOOLCLEANERSTATUS.ServiceStopped);
#if DEBUGLOOP
                iLoopCounter = 0;
#endif

                //Fase 3
                if (fiFiles.Length <= 0) goto PHASE4;
                int iFile = 0;
                foreach (FileInfo fiFile in fiFiles)
                {
                    fiFile.Delete();
                    iFile++;
                    RefreshStatus(SPOOLCLEANERSTATUS.CleaningSpool, iFile);
                    if (fiFile.Exists)
                        throw new Exception($"No se ha podido eliminar el archivo \"{fiFile.FullName}\".\n\nSe detendrá el la ejecución.");
                }
                foreach (DirectoryInfo diFolder in diSpool.GetDirectories("*", SearchOption.AllDirectories))
                {
                    if (diFolder.GetFiles().Length != 0)
                        throw new Exception($"El directorio \"{diFolder.FullName}\" no está vacío.\n\nSe detendrá la ejecución.");
                    diFolder.Delete();
                    if (diFolder.Exists)
                        throw new Exception($"No se ha podido eliminar el directorio \"{diFolder.FullName}\".\n\nSe detendrá el la ejecución.");
                }
                PHASE4:
                RefreshStatus(SPOOLCLEANERSTATUS.CleanedSpool);

                //Fase 4
                while (bBreaker)
                {
#if DEBUGLOOP
                    iLoopCounter++;
                    MessageBox.Show($"PHASE 4 - Starting Service. Loop No.: {iLoopCounter}");
#endif
                    if (scSpool.Status == ServiceControllerStatus.Running)
                    {
                        bBreaker = false;
                        break;
                    }
                    else if (scSpool.Status == ServiceControllerStatus.StartPending) continue;
                    scSpool.Start();
                    scSpool.Refresh();
                    Thread.Sleep(1000);
                }
                bBreaker = true;
                RefreshStatus(SPOOLCLEANERSTATUS.ServiceStarted);
#if DEBUGLOOP
                iLoopCounter = 0;
#endif

                //Fase 5
                RefreshStatus(SPOOLCLEANERSTATUS.Finished);
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error. Tome nota del mismo y comuníquelo al encargado informático:\n\n" + ex.Message);
            }
            finally
            {
                scSpool.Refresh();
                if (scSpool.Status == ServiceControllerStatus.Stopped) scSpool.Start();
                MessageBox.Show("Limpieza finalizada correctamente.", "FINALIZADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Acciones a realizar cuando se presiona el botón de salida o al cerrar el formulario.
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Confirmación del cierre del programa
        /// </summary>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult drClosing = MessageBox.Show("¿Desea salir de la aplicación?","SALIR",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (drClosing == DialogResult.No) { e.Cancel = true; }
        }
    }
}
