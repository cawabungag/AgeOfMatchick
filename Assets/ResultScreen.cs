using UnityEngine;

public class ResultScreen : MonoBehaviour
{
   public GameObject _victoryScreen;
   public GameObject _defeatScreen;

   public void SetResult(bool isVictory)
   {
      _victoryScreen.SetActive(isVictory);
      _defeatScreen.SetActive(!isVictory);
   }
}
