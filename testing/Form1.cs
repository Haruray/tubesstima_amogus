using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using System.Drawing.Imaging;
using System.Web;

using Microsoft.Msagl;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;

namespace tubesstima
{
    public partial class Form1 : Form
    {
        string[] input;
        string[] temp1;
        string[] temp2;
        string method;
        int size;
        Graph g;
<<<<<<< HEAD
=======

>>>>>>> fa27a9b46d05d01b8bd7d73c1c7729e794d98e67
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //browse button
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Text|*.txt|All|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = open.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //search button
<<<<<<< HEAD
            List<string> test = g.SearchPath("A", "C", method);
            
=======
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //upload button
            
            File.Copy(textBox1.Text, Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(textBox1.Text)), true);
            input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(textBox1.Text)));
            string alltext = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(textBox1.Text)));
            int graphSize = alltext.Distinct().Count();
            size = Int32.Parse(input[0]);
            g = new Graph(graphSize);
            string[] temp1 = new string[size];
            string[] temp2 = new string[size];
            int j = 0;
            for (int i = 1; i < size; i++)
            {
                string[] row = input[i].Split(' ');
                temp1[j] = row[0];
                temp2[j] = row[1];
                j++;
            }
            textBox1.Text = temp1[0];
            for (int i = 0; i < size - 1; i++)
            {
                g.NewEdge(temp1[i], temp2[i]);
            }
            
            List<string> test = g.SearchPath("A", "B", "BFS");
            //create a form 
            //System.Windows.Forms.Form form = Form.ActiveForm;
            //create a viewer object 
            //Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object 
>>>>>>> fa27a9b46d05d01b8bd7d73c1c7729e794d98e67
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            

            for (int i = 0; i < size - 1; i++)
            {
                graph.AddEdge(temp1[i], temp2[i]);
            }
            for (int i = 0; i < test.Count; i++)
            {
                graph.FindNode(test[i]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
            }

            Microsoft.Msagl.GraphViewerGdi.GraphRenderer renderer = new Microsoft.Msagl.GraphViewerGdi.GraphRenderer(graph);
            renderer.CalculateLayout();
<<<<<<< HEAD

=======
            //bind the graph to the viewer 
            //associate the viewer with the form 
            //form.FormBorderStyle = 
            //Microsoft.Msagl.Drawing.SvgGraphWriter.Write(graph, @"C:\Users\SAFIQ FARAY\source\repos\testing\testing\bin\Debug\graph.svg");
>>>>>>> fa27a9b46d05d01b8bd7d73c1c7729e794d98e67
            int width = 150;
            Bitmap bitmap = new Bitmap(width, (int)(graph.Height * (width / graph.Width)), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            renderer.Render(bitmap);
            bitmap.Save("graph.png");
            pictureBox1.Image = bitmap;
<<<<<<< HEAD
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //upload button
            File.Copy(textBox1.Text, Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(textBox1.Text)), true);
            input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(textBox1.Text)));
            string alltext = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(textBox1.Text)));
            int graphSize = alltext.Distinct().Count();
            textBox1.Text = input[0];
            size = Int32.Parse(input[0]);
            g = new Graph(graphSize);
            temp1 = new string[size];
            temp2 = new string[size];
            int j = 0;
            for (int i = 1; i < size; i++)
            {
                string[] row = input[i].Split(' ');
                temp1[j] = row[0];
                temp2[j] = row[1];
                j++;
            }
            textBox1.Text = "Berhasil!";
            for (int i = 0; i < size - 1; i++)
            {
                g.NewEdge(temp1[i], temp2[i]);
            }
=======
>>>>>>> fa27a9b46d05d01b8bd7d73c1c7729e794d98e67
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //select account
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //explore friends with
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //bfs
            method = "BFS";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //dfs
            method = "DFS";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
