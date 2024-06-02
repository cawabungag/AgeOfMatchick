using System;
using UnityEngine;

namespace AgeOfMatchic.Config
{
	[Serializable]
	public class AomConfig
	{
		[SerializeField]
		public CharacterConfig[] Characters;
		public AudioClip SilverClip;
		public AudioClip GoldClip;
	}
}