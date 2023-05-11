using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    [SerializeField] private AudioSource _clickSound;

    public void ReturnToMainMenu()
    {
        _clickSound.Play();
        SceneManager.LoadScene("Main_Menu");
    }
}
