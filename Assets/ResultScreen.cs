using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
   public GameObject _victoryScreen;
   public GameObject _defeatScreen;

   public Button Replay;
   public Button Play;

   public void SetResult(bool isVictory)
   {
      Replay.onClick.RemoveAllListeners();
      Play.onClick.RemoveAllListeners();
      
      Replay.onClick.AddListener(() => SceneManager.LoadScene("MainScene"));
      Play.onClick.AddListener(() => SceneManager.LoadScene("Lobby"));
      
      Replay.gameObject.SetActive(!isVictory);
      _victoryScreen.SetActive(isVictory);
      _defeatScreen.SetActive(!isVictory);

      if (isVictory)
      {
         Profile.Instance.Position = Profile.Instance.CurrectLevel;
         Profile.Instance.CompleteLevel(Profile.Instance.CurrectLevel);
      }
   }
}
