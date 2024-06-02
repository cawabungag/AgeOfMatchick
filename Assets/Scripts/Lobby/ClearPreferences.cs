using UnityEditor;
using UnityEngine;

public static class ClearPreferences
{
	[MenuItem("Tools/Clear All Preferences")]
	private static void ClearAllPreferences()
	{
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
		Debug.Log("All Player Preferences have been cleared.");
	}
}