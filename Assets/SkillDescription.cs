using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillDescription : MonoBehaviour
{
   public Image Image;
   public Button Hide;
   public TextMeshProUGUI Text;

   public void Setup(Sprite sprite, string desk)
   {
      gameObject.SetActive(true);
      Image.sprite = sprite;
      Text.text = desk;
      Hide.onClick.RemoveAllListeners();
      Hide.onClick.AddListener(() => gameObject.SetActive(false));
   }
}
