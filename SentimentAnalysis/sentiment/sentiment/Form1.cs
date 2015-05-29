using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace sentiment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[] positiveword;
        string[] negativeword;
        
        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            string postivepath = Application.StartupPath + "\\positive-words.txt";
            string negpath = Application.StartupPath + "\\Negative-words.txt";
            positiveword = File.ReadAllLines(@postivepath);
            negativeword = File.ReadAllLines(@negpath);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int procescnt = 0;
                string oppath = @textBox2.Text + "\\output" + DateTime.Now.ToString("ddMMMyyyyhhmmss") + ".txt";
                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Please select the output folder");
                }
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    panel1.Visible = true;
                    string verbatium = string.Empty; ;
                    string[] inputrec = File.ReadAllLines(textBox1.Text);
                    lbltotal.Text = inputrec.Count().ToString();
                    lblfinaltotal.Text = inputrec.Count().ToString();
                    foreach (string verbatiums in inputrec)
                    {
                        procescnt++;
                        lblprctotal.Text = procescnt.ToString();
                        verbatium = verbatiums.ToLower();

                        string[] sentences = Regex.Split(verbatium, @"(?<=[.!?])\s+(?=[A-Z])");
                        string[] words; string prevchar = string.Empty;
                        string currentval = string.Empty;
                        string[] booster = { "horrible", "absolutely", "complete", "completely", "definitely", "especially", "could", "did", "especially", "extremely", "fuckin", "fucking", "hugely", "incredibly", "just", "may", "might", "ought", "overwhelmingly", "really", "should", "slightly", "totally", "very", "would" };
                        
                        int positivecnt = 0;
                        int negativecnt = 0;
                        int i = 0;
                        int j = 0;
                        int k = 0;
                        foreach (string sentence in sentences)
                        {                            
                            words = sentence.Split(' ');
                            foreach (string word in words)
                            {                                
                                if (booster.Contains(word))
                                {

                                    if (j == 1)
                                    {
                                        positivecnt = positivecnt + 2;
                                    }
                                    else if (k == 1)
                                    {
                                        negativecnt = negativecnt + 2;
                                    }
                                    else
                                    {
                                        i = 1;
                                    }
                                   
                                }
                                else if (positiveword.Contains(word))
                                {                                    
                                    positivecnt++;
                                    if (i == 1)
                                    {
                                        positivecnt = positivecnt + 2;
                                    }
                                    j = 1;

                                }
                                else if (negativeword.Contains(word))
                                {                                    
                                    negativecnt++;
                                    if (i == 1)
                                    {
                                        negativecnt = negativecnt + 2;
                                    }
                                    k = 1;
                                }
                                else
                                {
                                    i = 0;
                                    j = 0;
                                    k = 0;                                    
                                }
                            }                           

                        }
                        
                        int sentiscore = positivecnt - negativecnt;
                        string sentivalue = "Negative";
                        if (sentiscore == 0)
                        {
                            sentivalue = "Netural";
                        }
                        else if (sentiscore > 0)
                        {
                            sentivalue = "Positive";
                        }
                        writeoutput(sentivalue, oppath);                        
                    }

                    label2.Text = "Processed ";
                    label3.Text = "Output available at " + oppath;
                }
                else
                {
                    panel1.Visible = false;
                    MessageBox.Show("Please select the input file");
                }
            }
            catch
            {
                MessageBox.Show("Error occured while processing please try again");
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Title = "Browse Text Files";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.RestoreDirectory = true;            
            openFileDialog1.InitialDirectory = @"C:\";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            } 

        }


        private void writeoutput(string value,string path)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(value);
                    writer.Flush();                    
                }
            }
            catch
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.SelectedPath = @"C:\";
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;             

            } 
        }


     
    }
}
