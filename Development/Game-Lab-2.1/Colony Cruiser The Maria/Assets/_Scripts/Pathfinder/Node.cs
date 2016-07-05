using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node>{
	public float gCost;
	public float hCost;
	public int gridX;
	public int gridY;
	public int gridZ;
	public Vector3 worldPos;
	public bool walkable;
	public bool flyable;
	public Node parent;
	private int heapIndex;

	public Node(int x, int y, int z, Vector3 world, bool mayWalk, bool mayFly){
		gridX = x;
		gridY = y;
		gridZ = z;
		worldPos = world;
		walkable = mayWalk;
		flyable = mayFly;
	}

	public float fCost{
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
