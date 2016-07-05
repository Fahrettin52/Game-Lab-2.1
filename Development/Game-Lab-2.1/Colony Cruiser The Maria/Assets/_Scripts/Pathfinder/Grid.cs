using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {
	public Node[,,] grid;
	public float nodeRadius;
	private float nodeDiameter;
	private int gridSizeX;
	private int gridSizeY;
	private int gridSizeZ;
	public Vector3 worldSize;
	public Vector3 mapStartingPoint;
	public LayerMask walkableMask;
	public bool displayGridGizmos;

	public void Awake(){
		nodeDiameter = nodeRadius * 2;
		mapStartingPoint = transform.position - Vector3.right * worldSize.x / 2 - Vector3.forward * worldSize.y / 2 - Vector3.up * worldSize.z / 2;
		gridSizeX = Mathf.RoundToInt (worldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt (worldSize.z / nodeDiameter);
		gridSizeZ = Mathf.RoundToInt (worldSize.y / nodeDiameter);
		CreateGrid ();
	}

	public int MaxSize{
		get{ 
			return gridSizeX * gridSizeY * gridSizeZ;
		}
	}

	public void CreateGrid(){
		grid = new Node[gridSizeX, gridSizeY, gridSizeZ];
		for(int i = 0; i < gridSizeX; i++){
			for(int j = 0; j < gridSizeY; j++){
				for (int k = 0; k < gridSizeZ; k++) {
					float nodeX = mapStartingPoint.x + i * nodeDiameter + nodeRadius;
					float nodeY = mapStartingPoint.y + j * nodeDiameter + nodeRadius;
					float nodeZ = mapStartingPoint.z + k * nodeDiameter + nodeRadius;
					Vector3 nodePos = new Vector3 (nodeX, nodeY, nodeZ);
					bool walkable = (Physics.CheckSphere (nodePos, nodeDiameter, walkableMask));
					grid [i, j, k] = new Node (i, j, k, nodePos, walkable);
				}
			}
		}
	}

	public List<Node> GetNeighbours(Node node){
		List<Node> neighbours = new List<Node> ();
		for(int x = -1; x <= 1; x++){
			for(int y = -1; y <= 1; y++){
				for (int z = -1; z <= 1; z++) {
					if (x == 0 && y == 0 && z == 0) {
						continue;
					}
					int checkX = node.gridX + x;
					int checkY = node.gridY + y;
					int checkZ = node.gridZ + z;
					if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY && checkZ >= 0 && checkZ < gridSizeZ) {
						neighbours.Add (grid [checkX, checkY, checkZ]);
					}
				}
			}
		}
		return neighbours;
	}

	public Node NodeFromWorldPoint(Vector3 worldPosition){
		Vector3 gridWorldPosition = worldPosition - mapStartingPoint;
		int x = Mathf.RoundToInt((gridWorldPosition.x - nodeRadius) / nodeDiameter);
		int y = Mathf.RoundToInt((gridWorldPosition.y - nodeRadius) / nodeDiameter);
		int z = Mathf.RoundToInt((gridWorldPosition.z - nodeRadius) / nodeDiameter);
		return grid[x,y,z];
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
