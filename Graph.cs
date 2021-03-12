using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Graph
{
	private int totalnode; //jumlah node
	List<string> nodes; //list nama node
	List<string>[] pointingTo; //list of list of string, yang tiap index nya adalah node-node yg ditunjuk oleh node bersangkutan
	//Misal
	//nodes = ["A","B"]
	//pointingTo = [ ["B","C"], ["A"] ]
	//Berarti, node A menunjuk ke node B dan C, dst
	//Maaf kalau keliru istilah. idgaf about istilah

	public Graph(int size){ 
		//ctor with size
		nodes = new List<string>();
		pointingTo = new List<string>[size];
		for (int i = 0; i < pointingTo.Length; i++){
			pointingTo[i] = new List<string>();
		}
		totalnode = size;
	}

	private int findIdxInNodes(string value){
		//Mencari index dari this->nodes
		//input adalah string node yg mau dicari
		//output adalah index value dari list nodes
		int i = 0;
		foreach (var item in nodes){
			if (string.Compare(item,value)==0){
				break;
			}
			i++;
		}
		return i;
	}

	public void NewEdge(string thisNode, string toNode){
		//Menambah edge baru
		if (!nodes.Contains(thisNode)){
			nodes.Add(thisNode);
		}
		if (!nodes.Contains(toNode))
        {
			nodes.Add(toNode);
        }

		pointingTo[findIdxInNodes(thisNode)].Add(toNode);
		pointingTo[findIdxInNodes(thisNode)].Sort();
	}

	public Boolean isConnected(string from, string target){
		foreach (var val in pointingTo[findIdxInNodes(from)])
        {
			if (target == val)
            {
				return true;
            }
        }
		return false;
    }

	public List<string> SearchPathWithBFS(string from, string target){

		List<string> path = new List<string>(); //seperti namanya, list ini adalah jalur dari from ke target
		string first; //untuk traversal saja, ignore this
		Boolean found = false; //boolean apakah target ditemukan atau tidak
		bool[] visit = new bool[totalnode]; //menandai apakah node-node telah dikunjungi atau tidak
		for (int i = 0; i < totalnode; i++){
			visit[i] = false; //default value : false
		}

		// antrian node untuk dikunjungi
		List<string> queue = new List<string>();

		// Node pertama
		visit[findIdxInNodes(from)] = true;
		queue.Add(from);

		while (queue.Count!=0 && !found){

			// Dequeue, masukkan ke path
			first = queue.First();
			if (path.Count != 0)
            {
				if (isConnected(path[path.Count-1], first))
					path.Add(first);
			}
            else
            {
				path.Add(first);
			}
			queue.RemoveAt(0);
			if (first == target)
			{ //Jika ketemu, maka found=true
				found = true;
				break;
			}
			// Melist pontingTo agar diproses ; in short mencari tetangga dari node tsb.
			List<string> list = pointingTo[findIdxInNodes(first)];

			foreach (var val in list){
				if (!visit[findIdxInNodes(val)]){
					visit[findIdxInNodes(val)] = true;
					queue.Add(val);
				}
			}
		}

		return path;
	}
}
