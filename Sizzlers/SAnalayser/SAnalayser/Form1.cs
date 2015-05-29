using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using System.Data;
using BusinessObjects;

namespace SAnalayser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            /*SentimentAnalyser s = new SentimentAnalyser();
           s.app = new Microsoft.Office.Interop.Word.Application();
           MessageBox.Show( s.process(textBox1.Text));
             FileSystemWatcher watcher = new FileSystemWatcher()
              {
                  Path = @"d:\pickup",
                  Filter = "*.txt"
              };
              watcher.Created += new FileSystemEventHandler(OnChanged); */

            processfile(@"d:\pickup\HackathonInput.txt", "HackathonInput.txt");

        }

        public void processfile(string path, string fileName)
        {
            string line;
            System.IO.StreamReader file =
              new System.IO.StreamReader(path);
            StringBuilder sb = new StringBuilder();
            List<SentimentObj> listObj = new List<SentimentObj>();
            Microsoft.Office.Interop.Word.Application app1 = new Microsoft.Office.Interop.Word.Application();
            SentimentAnalyser s = new SentimentAnalyser();
            s.app = app1;
            s.setup();
            while ((line = file.ReadLine()) != null)
            {
                
                //s.process("I took the day off yesterday for the installation, whole family was around and the technician did not show up. How do I cancel the service now?");
                sb.AppendLine(s.process(line));
                listObj.Add(s.obj);
            }
            
            file.Close();
            System.IO.File.WriteAllText(@"d:\pickup\sizzlers_Out.txt", sb.ToString());

            BusinessLayer.SentimentData data = new SentimentData();
            listObj.ForEach(i=>data.insertSentiments(i));

            MessageBox.Show("Analysis complete");
            
            //SendStatusEmail.SendEmail(fileName);


           
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            processfile(e.FullPath,e.Name);
        }
    }
}
