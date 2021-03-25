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

using System.Drawing;
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
        string method = "BFS";
        GraphKita g;
        Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
        TextHandler textData = new TextHandler();
        public Form1()
        {
            InitializeComponent();
        }

        private void printGraph()
        {
            Microsoft.Msagl.GraphViewerGdi.GraphRenderer renderer = new Microsoft.Msagl.GraphViewerGdi.GraphRenderer(graph);
            renderer.CalculateLayout();

            int width = 150;
            Bitmap bitmap = new Bitmap(width, (int)(graph.Height * (width / graph.Width)), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            renderer.Render(bitmap);
            bitmap.Save("graph.png");
            pictureBox1.Image = bitmap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //browse button
            //get file directory, file yang di load "BUKAN" di dalam folder debug
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Text|*.txt|All|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = open.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < textData.getTotalPeople(); i++)
            {
                graph.FindNode(textData.getPerson(i)).Attr.FillColor = Microsoft.Msagl.Drawing.Color.White;
            }

                //search button
                List<string> test = g.SearchPath(comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString(), method);

            for (int i = 0; i < test.Count; i++)
            {
                graph.FindNode(test[i]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;
            }
            printGraph();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //upload button
            textData.Load(textBox1.Text);
            g = new GraphKita(textData.getTotalConnection());
            File.Copy(textBox1.Text, Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(textBox1.Text)), true);
            input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(textBox1.Text)));
            textBox1.Text = input[0];

            for (int i = 0; i < textData.getTotalConnection(); i++)
            {
                g.NewEdge(textData.getPerson(i, 0), textData.getPerson(i, 1));
                graph.AddEdge(textData.getPerson(i, 0), textData.getPerson(i, 1));
                //g.NewEdge(textData.getPerson(i, 1), textData.getPerson(i, 0));
                //graph.AddEdge(textData.getPerson(i, 1), textData.getPerson(i, 0));
            }
            for (int i = 0; i < textData.getTotalPeople(); i++)
            {
                comboBox1.Items.Add(textData.getPerson(i));
                comboBox2.Items.Add(textData.getPerson(i));
            }

            printGraph();
            
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
