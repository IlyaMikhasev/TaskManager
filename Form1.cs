using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace TaskManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DataColumn idColumn = new DataColumn("Id", Type.GetType("System.Int32"));
            DataColumn nameColumn = new DataColumn("Name", Type.GetType("System.String"));
            DataColumn timeStart = new DataColumn("TimeStart", Type.GetType("System.String"));
            DataColumn timeProcessor = new DataColumn("TimeProcessor", Type.GetType("System.String"));
            DataColumn countTreads = new DataColumn("CountTreads", Type.GetType("System.Int32"));

            dt.Columns.Add(idColumn);
            dt.Columns.Add(nameColumn);
            dt.Columns.Add(timeStart);
            dt.Columns.Add(timeProcessor);
            dt.Columns.Add(countTreads);
            //dgMainFill(dgMain);
            reloadTime = 1000;
            timer1 = new System.Threading.Timer(dgMainFill, dgMain, 50, reloadTime);

        }

        private static DataTable dt = new DataTable();
        private static Process[] processes;
        public static Int32 reloadTime;
        System.Threading.Timer timer1;

        
        public void dgMainFill(object sender)
        {
            processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                string s, t;
                //ДОБАВИТЬ В МЕНЮСТРИП
                //process.Kill(); // принудительное закрытие
                //process.CloseMainWindow(); // закрывает с диалогом
                try 
                { 
                    s = process.StartTime.ToString(); 
                } catch { s = ""; }
                try 
                { 
                    t = process.TotalProcessorTime.ToString(); 
                } catch { t = ""; }
                try 
                { 
                    dt.Rows.Add(new object[] { process.Id, process.ProcessName, s, t, process.Threads.Count }); 
                } catch { }
            }
            //try
            {
                Invoke((MethodInvoker)delegate
                {
                    (sender as DataGridView).DataSource = dt;
                });
            }
            //catch { }
           
        }
        // привязать контекстное меню к гриду 
        private void времяОбновленияToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            Form form = new Form2();
            form.ShowDialog();
            timer1.Change(reloadTime, reloadTime);
        }
    }
}
