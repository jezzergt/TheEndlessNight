using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RelicLogic : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject _freezePositionGO;
    [SerializeField] private GameObject _storyEndingDialogGO;
    [SerializeField] private GameObject _confusedDialogGO;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _atmosphericMusic;
    [SerializeField] private AudioSource _characterSound;

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer[] _bgLayers;

    [Header("Animators")]
    [SerializeField] private Animator _playerAnim;

    private PlayerMovement _playerMovement;
    private Camera _camera;
    private Vector3 _cameraInitialPosition;

    [Header("Primitives")]
    public float ShakeMagnetude = 0.5f;
    public float ShakeTime = 0.5f;

    private void Start()
    {
        _camera = Camera.main;
        _playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("We detected collision");
            _playerMovement.HasRelic = true;
            _atmosphericMusic.Play();
            ShakeIt();
            _playerAnim.Play("Player_Changing");
            StartCoroutine("ConfusedText");
        }
    }
    public void ShakeIt()
	{
		_cameraInitialPosition = _camera.transform.position;
		InvokeRepeating ("StartCameraShaking", 0f, 0.005f);
		Invoke ("StopCameraShaking", ShakeTime);
	}

	void StartCameraShaking()
	{
		float cameraShakingOffsetX = Random.value * ShakeMagnetude * 2 - ShakeMagnetude;
		float cameraShakingOffsetY = Random.value * ShakeMagnetude * 2 - ShakeMagnetude;
		Vector3 cameraIntermadiatePosition = _camera.transform.position;
		cameraIntermadiatePosition.x += cameraShakingOffsetX;
		cameraIntermadiatePosition.y += cameraShakingOffsetY;
		_camera.transform.position = cameraIntermadiatePosition;
	}

	void StopCameraShaking()
	{
		CancelInvoke ("StartCameraShaking");
		_camera.transform.position = _cameraInitialPosition;
	}

    IEnumerator ConfusedText()
    {
        _confusedDialogGO.SetActive(true);
        _characterSound.Play();

        yield return new WaitForSeconds(4.5f);

        _confusedDialogGO.SetActive(false);
        _storyEndingDialogGO.SetActive(true);
        StartCoroutine("ToMainMenu");
    }

    IEnumerator ToMainMenu()
    {
        yield return new WaitForSeconds(10f);

        SceneManager.LoadScene("Main_Menu");
    }
}
