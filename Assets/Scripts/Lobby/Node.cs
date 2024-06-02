using System.Linq;
using AgeOfMatchic.Config;
using DefaultNamespace;
using UnityEngine;

public class Node : MonoBehaviour
{
   public string LevelId = string.Empty;
   public Node[] Next;
   public Node[] Preview;

   private SpriteRenderer _sprite;
   public LevelData LevelData;
   public bool IsCompleted => Profile.Instance.CompletedLevel.Contains(LevelId);
   private GameObject _obj;

   private void Start()
   {
      _sprite = GetComponent<SpriteRenderer>();
      
      if (LevelId == string.Empty)
      {
         Debug.LogError($"NODE {gameObject.name} is EMPTY");
         return;
      }

      if (IsCompleted)
      {
         return;
      }
       
      LevelData = GraphLobby.Instance.Config.LevelsData.Data.ToList().Find(x => x.Id == LevelId);
      if (LevelData == null)
         return;

      _obj = Instantiate(LevelData.ViewPrefab, transform.position, Quaternion.identity);
   }

   public void SetHighlight(bool isHighlight)
   {
      _sprite.color = isHighlight ? Color.green : Color.clear;
   }

   public void ClearPrefab()
   {
      Destroy(_obj.gameObject);
   }
}