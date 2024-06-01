using System.Collections.Generic;

public class Pathfinding
{
	public List<Node> FindPath(Node startNode, Node targetNode)
	{
		if (startNode == null || targetNode == null)
		{
			return null;
		}

		Queue<Node> queue = new Queue<Node>();
		Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();

		queue.Enqueue(startNode);
		cameFrom[startNode] = null;

		while (queue.Count > 0)
		{
			Node current = queue.Dequeue();

			if (current == targetNode)
			{
				return ReconstructPath(cameFrom, current);
			}

			var neighbors = new List<Node>();
			neighbors.AddRange(current.Next);
			neighbors.AddRange(current.Preview);
			
			foreach (Node neighbor in neighbors)
			{
				if (!cameFrom.ContainsKey(neighbor))
				{
					queue.Enqueue(neighbor);
					cameFrom[neighbor] = current;
				}
			}
		}

		return null;
	}

	private List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node current)
	{
		List<Node> path = new List<Node>();
		while (current != null)
		{
			path.Add(current);
			current = cameFrom[current];
		}
		path.Reverse();
		return path;
	}
}