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

	public static GraphLobby Instance;

	private void Awake()
	{
		Instance = this;
	}

	public void EntryLevel(Node startNode)
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