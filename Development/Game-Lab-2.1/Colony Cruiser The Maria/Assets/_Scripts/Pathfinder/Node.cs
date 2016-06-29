using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node>{
	public int gCost;
	public int hCost;
	public int gridX;
	public int gridY;
	public Vector3 worldPos;
	public bool walkable;
	public Node parent;
	private int heapIndex;

	public Node(int x, int y, Vector3 world, bool mayWalk){
		gridX = x;
		gridY = y;
		worldPos = world;
		walkable = mayWalk;
	}

	public int fCost{
		get{ 
			return gCost + hCost;
		}
	}

	public int HeapIndex{
		get{ 
			return heapIndex;
		}
		set{ 
			heapIndex = value;
		}
	}

	public int CompareTo(Node nodeToCompare){
		int compare = fCost.CompareTo (nodeToCompare.fCost);
		if(compare == 0){
			compare = hCost.CompareTo (nodeToCompare.hCost);
		}
		return -compare;
	}
}
