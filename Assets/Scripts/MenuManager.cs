using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject _howToPanelGO;
    [SerializeField] private GameObject _storyIntroPanelGO;
    [SerializeField] private GameObject _menuGroupGO;
    
    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSound;
    [SerializeField] private AudioSource _clickSound;
    
    private bool _audioToggle;

    public void PlayGame()
    {
        _clickSound.Play();
        _menuGroupGO.SetActive(false);
        _storyIntroPanelGO.SetActive(true);
        Invoke("StartLevelOne", 40f);
    }

    public void StartLevelOne()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void HowToOpen()
    {
        _clickSound.Play();
        _howToPanelGO.SetActive(true);
    }

    public void HowToClose()
    {
        _clickSound.Play();
        _howToPanelGO.SetActive(false);
    }

    public void ExitGame()
    {
        _clickSound.Play();
        Application.Quit();
    }

    public void ToggleAudio()
    {
        _audioToggle = !_audioToggle;
 
         if (_audioToggle)
             AudioListener.volume = 1f;
 
         else
             AudioListener.volume = 0f;
    }

}
