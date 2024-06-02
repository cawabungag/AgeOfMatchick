using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Complete : MonoBehaviour
{
    [SerializeField]
    private Button _button;
    
    void Start()
    {
        _button.onClick.AddListener(() =>
        {
            Profile.Instance.CompleteLevel(Profile.Instance.CurrectLevel);
            SceneManager.LoadScene("Lobby");
        });
    }
}
