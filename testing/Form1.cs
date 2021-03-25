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
        string prevAccount = "";
        string prevExplore = "";
        string outputTemplate;

        GraphKita g;
        Microsoft.Msagl.Drawing.Graph graph;
        TextHandler textData = new TextHandler();
        public Form1()
        {
            InitializeComponent();
            outputTemplate = textBox2.Text;
        }

        private void printGraph()
        {
            Microsoft.Msagl.GraphViewerGdi.GraphRenderer renderer = new Microsoft.Msagl.GraphViewerGdi.GraphRenderer(graph);
            //graph.Attr.  .ArrowheadAtTarget = ArrowStyle.None;
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
        private void instantSearch()
        {
            //no button, just instant search
            if (!(comboBox1.SelectedItem == null || comboBox2.SelectedItem == null))
            {
                for (int i = 0; i < textData.getTotalPeople(); i++)
                {
                    graph.FindNode(textData.getPerson(i)).Attr.FillColor = Microsoft.Msagl.Drawing.Color.White;
                    graph.FindNode(textData.getPerson(i)).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Octagon;
                }

                List<string> test = g.SearchPath(comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString(), method);
                textBox2.Text = "Nama akun: " + comboBox1.SelectedItem.ToString() + " dan " + comboBox2.SelectedItem.ToString() + "\r\n";


                if (test[test.Count - 1] != "")
                {
                    if (test.Count == 1) textBox2.Text += ("1st - degree connection\r\n");
                    else if (test.Count == 2) textBox2.Text += ("2nd - degree connection\r\n");
                    else if (test.Count == 3) textBox2.Text += ("3rd - degree connection\r\n");
                    else textBox2.Text += (test.Count + "th - degree connection\r\n");
                    for (int i = 0; i < test.Count; i++)
                    {
                        graph.FindNode(test[i]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;
                        if (i == 0) graph.FindNode(test[i]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightBlue;
                        textBox2.Text += (test[i]);
                        if (i != test.Count - 1) textBox2.Text += (" → ");
                    }
                    graph.FindNode(test[0]).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
                    graph.FindNode(test[test.Count - 1]).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
                }
                else
                {
                    textBox2.Text += ("tidak ada relasi pada kedua akun\r\n");
                    graph.FindNode(test[0]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightBlue;
                    graph.FindNode(test[test.Count - 2]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;
                    graph.FindNode(test[0]).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
                    graph.FindNode(test[test.Count - 2]).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
                }

                printGraph();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //friend recommendation button
            List<string> userFriends = new List<string>();
            List<List<string>> userRecommendFriends = new List<List<string>>();
            List<string> rangeTest;
            bool hasFriendRecommend = false;
            for (int i = 0; i < textData.getTotalPeople(); i++)
            {
                rangeTest = g.SearchPath(comboBox1.SelectedItem.ToString(), textData.getPerson(i), "BFS");
                if (rangeTest.Count == 2 && rangeTest[rangeTest.Count - 1] != "")
                {
                    userFriends.Add(rangeTest[rangeTest.Count - 1]);
                }
            }

            for (int i = 0; i < userFriends.Count; i++)
            {
                textBox2.Text += "Nama akun: " + userFriends[i] + "\r\n";
                userRecommendFriends.Add(new List<string>());
                for (int j = 0; j < textData.getTotalPeople(); j++)
                {
                    rangeTest = g.SearchPath(userFriends[i], textData.getPerson(j), "BFS");
                    if (rangeTest.Count == 2 && rangeTest[rangeTest.Count - 1] != "" && !(rangeTest[rangeTest.Count - 1].Equals(comboBox1.SelectedItem.ToString())) && !(userFriends.Contains(rangeTest[rangeTest.Count - 1])) )
                    {
                        userRecommendFriends[i].Add(rangeTest[rangeTest.Count - 1]);
                        textBox2.Text += rangeTest[rangeTest.Count - 1] + "\r\n";
                        hasFriendRecommend = true;
                    }
                }
            }

            if (hasFriendRecommend)
            {
                textBox2.Text = "Daftar rekomendasi teman untuk akun " + comboBox1.SelectedItem.ToString() + ": \r\n";
                for (int i = 0; i < userFriends.Count; i++)
                {
                    textBox2.Text += "Nama akun: " + userFriends[i] + "\r\n";
                    textBox2.Text += userRecommendFriends[i].Count + " mutual friends:" + "\r\n";
                    for (int j = 0; j < userRecommendFriends[i].Count; j++)
                    {
                        textBox2.Text += userRecommendFriends[i][j] + "\r\n";
                    }
                    textBox2.Text += "\r\n";
                }
            }
            else
            {
                textBox2.Text = "Tidak ada rekomendasi teman yang relevan, coba berteman dengan lebih banyak orang lagi a.k.a tidak no life";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //upload button
            if (!(textBox1.Text.Equals("")))
            {
                //for (int i = 0; i < textData.getTotalPeople(); i++) graph.RemoveNode(graph.FindNode(textData.getPerson(i)));
                //g.Clear();
                textData = new TextHandler();
                prevAccount = "";
                prevExplore = "";
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                textData.Load(textBox1.Text);
                graph = new Microsoft.Msagl.Drawing.Graph("graph");
                g = new GraphKita(textData);

                for (int i = 0; i < textData.getTotalConnection(); i++)
                {
                    g.NewEdge(textData.getFriendConnection(i)[0], textData.getFriendConnection(i)[1]);
                    var edge = graph.AddEdge(textData.getFriendConnection(i)[0], textData.getFriendConnection(i)[1]);
                    edge.Attr.ArrowheadAtSource = Microsoft.Msagl.Drawing.ArrowStyle.None;
                    edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                }
                for (int i = 0; i < textData.getTotalPeople(); i++)
                {
                    comboBox1.Items.Add(textData.getPerson(i));
                    comboBox2.Items.Add(textData.getPerson(i));
                }


                if ((textData.getTotalPeople() == 1))
                {
                    comboBox1.SelectedItem = (textData.getPerson(0));
                    comboBox2.SelectedItem = (textData.getPerson(0));
                    button3.Enabled = true;
                }
                else if ((textData.getTotalPeople() > 1))
                {
                    comboBox1.SelectedItem = (textData.getPerson(0));
                    comboBox2.SelectedItem = (textData.getPerson(1));

                    comboBox1.Items.Remove(comboBox2.SelectedItem.ToString());
                    comboBox2.Items.Remove(comboBox1.SelectedItem.ToString());
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                    button3.Enabled = true;
                }
                printGraph();
                instantSearch();

            }
            else
            {
                Console.WriteLine("Path file tidak valid");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //select account
            if (!(prevAccount.Equals("")))
            {
                comboBox2.Items.Add(prevAccount);
            }
            prevAccount = comboBox1.SelectedItem.ToString();
            comboBox2.Items.Remove(comboBox1.SelectedItem.ToString());
            instantSearch();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //explore friends with
            if (!(prevExplore.Equals("")))
            {
                comboBox1.Items.Add(prevExplore);
            }
            prevExplore = comboBox2.SelectedItem.ToString();
            comboBox1.Items.Remove(comboBox2.SelectedItem.ToString());
            instantSearch();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //bfs
            method = "BFS";
            instantSearch();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //dfs
            method = "DFS";
            instantSearch();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public void sendError(string error)
        {
            textBox2.Text = error;
        }
    }
}
