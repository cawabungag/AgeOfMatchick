using System;
using AgeOfMatchic.Config;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GraphLobby : MonoBehaviour
{
	public Action<Node> OnSelectNode;
	public ConfigObject Config;
	public Node StartNode;
	public Node[] Nodes;

	public static GraphLobby Instance;

	private void Awake()
	{
		Instance = this;
	}

	public void EntryLevel(Node startNode, Node node)
	{
		if (startNode.IsCompleted)
		{
			return;
		}
		
		if (startNode.LevelData != null)
		{
			startNode.ClearPrefab();
			switch (startNode.LevelData.Type)
			{
				case LevelType.Reward:
					ApplyReward(startNode);
					break;
				
				case LevelType.Enemy:
					Profile.Instance.CurrentEnemy = startNode.LevelData.EnemyId;
					Profile.Instance.CurrectLevel = startNode.LevelData.Id;
					Profile.Instance.Position = node.LevelData.Id;
					SceneManager.LoadScene("MainScene");
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}

	private static void ApplyReward(Node startNode)
	{
		var rewards = startNode.LevelData.Rewards;
		foreach (var reward in rewards)
		{
			switch (reward.RewardType)
			{
				case RewardType.Common:
					Profile.Instance.SetCommon(reward.Amount);
					break;
				case RewardType.Premium:
					Profile.Instance.SetPremium(reward.Amount);
					break;
				case RewardType.Hero:
					Profile.Instance.AddHero(reward.Hero);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		Profile.Instance.CompleteLevel(startNode.LevelData.Id);
	}
}