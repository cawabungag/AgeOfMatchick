using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Character : MonoBehaviour
{
	private Pathfinding _pathfinding = new();
	private List<Node> _nodeBuffer = new();
	private Node StartNode;

	void Start()
	{
		GraphLobby.Instance.OnSelectNode += OnSelectNode;
		StartNode = GraphLobby.Instance.StartNode;
		transform.position = StartNode.transform.position;
	}

	private void OnSelectNode(Node obj)
	{
		ClearPath();

		var path = _pathfinding.FindPath(StartNode, obj);
		HighlightPath(path);
		var enumerable = path.Select(x => x.transform.position).ToArray();
		
		StartNode = path[^1];

		transform.DOPath(enumerable, 1, PathType.CatmullRom)
			.SetEase(Ease.Linear)
			.OnComplete(() =>
			{
				GraphLobby.Instance.EntryLevel(StartNode);
				ClearPath();
			});
	}

	private void HighlightPath(List<Node> path)
	{
		foreach (var node in path)
		{
			_nodeBuffer.Add(node);
			node.SetHighlight(true);
		}
	}

	private void ClearPath()
	{
		foreach (var node in _nodeBuffer)
		{
			node.SetHighlight(false);
		}

		_nodeBuffer.Clear();
	}
}