using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Complete : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    public bool IsWin;
    
    void Start()
    {
        _button.onClick.AddListener(() =>
        {
            if (IsWin)
            {
                Profile.Instance.CompleteLevel(Profile.Instance.CurrectLevel);
            }
            SceneManager.LoadScene("Lobby");
        });
    }
}
