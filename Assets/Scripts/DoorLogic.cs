using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorLogic : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject _noKeyGO;
    
    [Header("AudioSources")]
    [SerializeField] private AudioSource _finishSound;
    [SerializeField] private AudioSource _noKeySound;

    [Header("Animators")]
    [SerializeField] private Animator _doorAnim;

    private PlayerInventory _playerInventory;

    private void Start()
    {
        _playerInventory = GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Door")
        {
            if (_playerInventory.HasKey)
            {
                _finishSound.Play();
                _doorAnim.SetTrigger("Unlocked");
                Invoke("CompleteLevel", 1f);
            }
            else
            {
                _noKeySound.Play();
                StartCoroutine(DisplayHint());
            }
        }
    }

    IEnumerator DisplayHint()
    {
        _noKeyGO.SetActive(true);

        yield return new WaitForSeconds(2.5f);

        _noKeyGO.SetActive(false);
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
