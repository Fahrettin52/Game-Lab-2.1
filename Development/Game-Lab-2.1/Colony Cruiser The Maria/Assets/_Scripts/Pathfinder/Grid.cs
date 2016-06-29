using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {
	public Transform player;
	public Node[,] grid;
	public float nodeRadius;
	private float nodeDiameter;
	public float worldPositionDivider;
	private int gridSizeX;
	private int gridSizeY;
	public Vector2 worldSize;
	public Vector3 mapStartingPoint;
	public LayerMask unwalkableMask;
	public bool displayGridGizmos;

	public void Awake(){
		nodeDiameter = nodeRadius * 2;
		mapStartingPoint = transform.position - Vector3.right * worldSize.x / 2 - Vector3.forward * worldSize.y / 2;
		gridSizeX = Mathf.RoundToInt (worldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt (worldSize.y / nodeDiameter);
		CreateGrid ();
	}

	public int MaxSize{
		get{ 
			return gridSizeX * gridSizeY;
		}
	}

	public void CreateGrid(){
		grid = new Node[gridSizeX, gridSizeY];
		for(int i = 0; i < gridSizeX; i++){
			for(int j = 0; j < gridSizeY; j++){
				float nodeX = mapStartingPoint.x + i * nodeDiameter + nodeRadius;
				float nodeY = mapStartingPoint.y;
				float nodeZ = mapStartingPoint.z + j * nodeDiameter + nodeRadius;
				Vector3 nodePos = new Vector3 (nodeX, nodeY, nodeZ);
				bool walkable = !(Physics.CheckSphere (nodePos, nodeDiameter, unwalkableMask));
				grid [i, j] = new Node (i, j, nodePos, walkable);
			}
		}
	}

	public List<Node> GetNeighbours(Node node){
		List<Node> neighbours = new List<Node> ();
		for(int x = -1; x <= 1; x++){
			for(int y = -1; y <= 1; y++){
				if(x == 0 && y == 0){
					continue;
				}
				int checkX = node.gridX + x;
				int checkY = node.gridY + y;
				if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY){
					neighbours.Add (grid[checkX, checkY]);
				}
			}
		}
		return neighbours;
	}

	public Node NodeFromWorldPoint(Vector3 worldPosition){
		float percentX = (worldPosition.x + worldSize.x / worldPositionDivider) / worldSize.x;
		float percentY = (worldPosition.z + worldSize.y / worldPositionDivider) / worldSize.y;
		percentX = Mathf.Clamp01 (percentX);
		percentY = Mathf.Clamp01 (percentY);
		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
		return grid[x,y];
	}
		
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube (transform.position, new Vector3 (worldSize.x, 1, worldSize.y));
		if (grid != null && displayGridGizmos) {
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable) ? Color.blue : Color.red;
				Gizmos.DrawSphere (n.worldPos, nodeRadius);
			}
		}
	}
}
