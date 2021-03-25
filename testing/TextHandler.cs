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

namespace tubesstima
{
    public class TextHandler
    {
        private string[] textLine;
        private string[][] friendConnection;
        private int totalConnection = 0;
        private List<string> person = new List<string>();
        private int totalPeople = 0;
        //public string[,] ServicePoint = new string[10, 9];

        public void Load(string fileTarget)
        {
            textLine = File.ReadAllLines(fileTarget);
            if (textLine[0].Equals((textLine.Length - 1).ToString()))
            {
                //Console.WriteLine(textLine[0]);

                for (int i = 0; i < textLine.Length - 1; i++)
                {
                    textLine[i] = textLine[i + 1];
                }
                textLine[textLine.Length - 1] = (textLine.Length - 1).ToString();
                string lastPersonA = "";
                string personA;
                string personB;

                int[] forbiddenConnection = new int[textLine.Length - 1];
                for (int i = 0; i < textLine.Length - 1; i++) forbiddenConnection[i] = -1;
                totalConnection = textLine.Length - 1;

                //Console.WriteLine("jumlah connection : " + (textLine.Length - 1));

                for (int i = 0; i < textLine.Length - 1; i++)
                {
                    personA = textLine[i].Split(' ')[0];
                    personB = textLine[i].Split(' ')[1];
                    if (!(lastPersonA.Equals(personA)))
                    {
                        lastPersonA = textLine[i].Split(' ')[0];
                    }

                    for (int j = 1 + i; j < textLine.Length - 1; j++)
                    {
                        if (textLine[j].Split(' ')[0].Equals(personA) && textLine[j].Split(' ')[1].Equals(personB) || textLine[j].Split(' ')[0].Equals(personB) && textLine[j].Split(' ')[1].Equals(personA))
                        {
                            forbiddenConnection[(textLine.Length - 1) - totalConnection] = i;
                            totalConnection -= 1;
                            //Console.WriteLine("Pengurangan connection menjadi : " + (totalConnection));
                            //Console.WriteLine("I sekarang adalah : " + i.ToString());
                        }
                        //Console.WriteLine("j : " + j);
                    }
                    //Console.WriteLine("i : " + i);
                }

                friendConnection = new string[totalConnection][];

                for (int i = 0; i < totalConnection; i++)
                {
                    friendConnection[i] = new string[2];
                }

                int counterC = 0;
                for (int i = 0; i < textLine.Length - 1; i++)
                {
                    //Console.WriteLine("I sekarang adalah : " + i.ToString());
                    if (!(forbiddenConnection.Contains(i)))
                    {
                        friendConnection[counterC][0] = textLine[i].Split(' ')[0];
                        friendConnection[counterC][1] = textLine[i].Split(' ')[1];
                        //Console.WriteLine("friend connection ke-" + counterC + " :\n" + friendConnection[counterC][0] + " " + friendConnection[counterC][1]);
                        counterC += 1;
                    }
                    else
                    {
                        //Console.WriteLine(textLine[i].Split(' ')[0] + " " + textLine[i].Split(' ')[1] + " <- Ini sudah terdapat dalam connection");
                    }
                }
                for (int i = 0; i < totalConnection; i++)
                {
                    if (!(person.Contains(friendConnection[i][0])))
                    {
                        //Console.WriteLine("Add : " + friendConnection[i][0] + " Dari Kiri");
                        person.Add(friendConnection[i][0]);
                    }
                    if (!(person.Contains(friendConnection[i][1])))
                    {
                        //Console.WriteLine("Add : " + friendConnection[i][1] + " Dari Kanan");
                        person.Add(friendConnection[i][1]);
                    }
                }
                totalPeople = person.Count;
            }
            else
            {
                Console.WriteLine("File yang diupload tidak memenuhi kriteria format");
            }
        }

        public int getPersonFriend(int idx)
        {
            int jumlah = 0;
            for (int i = 0; i < totalConnection; i++)
            {
                if (getFriendConnection(i)[0].Equals(getPerson(idx)))
                {
                    jumlah += 1;
                }

                if (getFriendConnection(i)[1].Equals(getPerson(idx)))
                {
                    jumlah += 1;
                }
            }
            return jumlah;
        }
        public int getTotalConnection()
        {
            return totalConnection;
        }
        public string[] getFriendConnection(int idx)
        {
            string[] tempFriendConnection = new string[2];

            tempFriendConnection[0] = friendConnection[idx][0];
            tempFriendConnection[1] = friendConnection[idx][1];

            return tempFriendConnection;
        }
        public int getTotalPeople()
        {
            return totalPeople;
        }
        public string getPerson(int idx)
        {
            return person[idx];
        }
        public int getPersonId(string name)
        {
            for (int i = 0; i < getTotalPeople(); i++)
            {
                if (person[i].Equals(name)) return i;
            }
            return -1;
        }
    }
}
