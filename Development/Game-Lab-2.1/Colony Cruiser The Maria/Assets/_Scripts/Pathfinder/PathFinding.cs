using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System; 

public class PathFinding : MonoBehaviour {
	PFRequestMng requestManager;
	Grid grid;

	void Awake(){
		requestManager = GetComponent<PFRequestMng> ();
		grid = GetComponent<Grid> ();
	}

	public void StartFindPath(Vector3 startPos, Vector3 targetPos){
		StartCoroutine (FindPath (startPos, targetPos));
	}

	IEnumerator FindPath(Vector3 startPos, Vector3 targetPos){
		Vector3[] wayPoints = new Vector3[0];
		bool pathSucces = false;
		Node startNode = grid.NodeFromWorldPoint (startPos);
		Node targetNode = grid.NodeFromWorldPoint (targetPos);
		if (startNode.walkable && targetNode.walkable) {
			Heap<Node> openSet = new Heap<Node> (grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node> ();
			openSet.Add (startNode);
			while (openSet.Count > 0) {
				Node currentNode = openSet.RemoveFirst ();
				closedSet.Add (currentNode);
				if (currentNode == targetNode) {
					pathSucces = true;
					break;
				}
				foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
					if (!neighbour.walkable || closedSet.Contains (neighbour)) {
						continue;
					} 
					float newMovementCostToNeighbour = currentNode.gCost + GetDistance (currentNode, neighbour);
					if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains (neighbour)) {
						neighbour.gCost = newMovementCostToNeighbour;
						neighbour.hCost = GetDistance (neighbour, targetNode);
						neighbour.parent = currentNode;
						if (!openSet.Contains (neighbour)) {
							openSet.Add (neighbour);
							openSet.UpdateItem (neighbour);
						}
					}
				}
			}
		}
		yield return null;
		if(pathSucces){
			wayPoints = RetracePath (startNode, targetNode);
		}
		requestManager.FinishedProcessingPath (wayPoints, pathSucces);
	}

	Vector3[] RetracePath(Node startNode, Node endNode){
		List<Node> path = new List<Node> ();
		Node currentNode = endNode;
		while(currentNode != startNode){
			path.Add (currentNode);
			currentNode = currentNode.parent;
		}
		Vector3[] waypoints = SimplifyPath(path);
		Array.Reverse(waypoints);
		return waypoints;
	}

	Vector3[] SimplifyPath(List<Node> path){
		List<Vector3> waypoints = new List<Vector3> ();
		Vector3 directionOld = Vector3.zero;
		for(int i = 1; i < path.Count; i++){
			Vector3 directionNew = new Vector3 (path [i - 1].gridX - path [i].gridX, path[i-1].gridY - path[i].gridY, path [i - 1].gridZ - path [i].gridZ);
			if(directionNew != directionOld){
				waypoints.Add (path[i].worldPos);
			}
			directionOld = directionNew;
		}
		return waypoints.ToArray();
	}

	float GetDistance(Node nodeA, Node nodeB){
		Vector3 startLocation = nodeA.worldPos;
		Vector3 endLocation = nodeB.worldPos;
		return Vector3.Distance (startLocation, endLocation);
	}
}
