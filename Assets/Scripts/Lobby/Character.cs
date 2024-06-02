using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
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
		if (string.IsNullOrEmpty(Profile.Instance.Position))
		{
			transform.position = StartNode.transform.position;
		}
		else
		{
			var find = GraphLobby.Instance.Nodes.ToList().Find(x => x.LevelId == Profile.Instance.Position);
			if (find)
			{
				StartNode = find;
				GraphLobby.Instance.StartNode = find;
				transform.position = find.transform.position;
			}
			else
			{
				transform.position = StartNode.transform.position;
			}
		}
	}

	private bool isInProgress = false;
	private void OnSelectNode(Node obj)
	{
		if (isInProgress)
		{
			return;
		}
		ClearPath();

		var path = _pathfinding.FindPath(StartNode, obj);
		HighlightPath(path);
		var enumerable = path.Select(x => x.transform.position).ToArray();
		
		StartNode = path[^1];

		isInProgress = true;
		transform.DOPath(enumerable, 1, PathType.CatmullRom)
			.SetEase(Ease.Linear)
			.OnComplete(() =>
			{
				isInProgress = false;
				GraphLobby.Instance.EntryLevel(StartNode, path[^2]);
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