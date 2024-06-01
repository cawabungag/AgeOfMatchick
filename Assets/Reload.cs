using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Reload : MonoBehaviour
{
    [SerializeField]
    private Button _button;
    void Start()
    {
        _button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainScene");
        });
    }
}
