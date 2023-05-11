using System.Collections;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject _freezePositionGO;
    [SerializeField] private GameObject _playerGO;
    [SerializeField] private GameObject _evilPlayerGO;
    [SerializeField] private GameObject _keyGO;
    [SerializeField] private GameObject _playerDialogGO_1;
    [SerializeField] private GameObject _playerDialogGO_2;
    [SerializeField] private GameObject _playerDialogGO_3;
    [SerializeField] private GameObject _evilDialogGO_1;
    [SerializeField] private GameObject _evilDialogGO_2;
    [SerializeField] private GameObject _evilDialogGO_3;
    [SerializeField] private GameObject _evilDialogGO_4;
    [SerializeField] private GameObject _evilDialogGO_5;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _playerDialogSound;
    [SerializeField] private AudioSource _evilDialogSound;
    [SerializeField] private AudioSource _musicSound;
    [SerializeField] private AudioSource _vanishSound;

    private PlayerMovement _playerMovement;
    private bool _startedDialog = false;

    private void Start()
    {
        _playerMovement = _playerGO.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (_playerMovement.IsFrozen && _startedDialog == false)
        {
            StartCoroutine("StartPlayerDialog_1");
            _startedDialog = true;
        }
    }

    IEnumerator StartPlayerDialog_1()
    {
        _musicSound.Play();
        _playerDialogGO_1.SetActive(true);
        _playerDialogSound.Play();

        yield return new WaitForSeconds(2.5f);

        _playerDialogGO_1.SetActive(false);
        StartCoroutine("StartEvilDialog_1");
    }

    IEnumerator StartEvilDialog_1()
    {
        _evilDialogGO_1.SetActive(true);
        _evilDialogSound.Play();

        yield return new WaitForSeconds(2.5f);

        _evilDialogGO_1.SetActive(false);
        StartCoroutine("StartPlayerDialog_2");
    }

    IEnumerator StartPlayerDialog_2()
    {
        _playerDialogGO_2.SetActive(true);
        _playerDialogSound.Play();

        yield return new WaitForSeconds(2.5f);

        _playerDialogGO_2.SetActive(false);
        StartCoroutine("StartEvilDialog_2");
    }

    IEnumerator StartEvilDialog_2()
    {
        _evilDialogGO_2.SetActive(true);
        _evilDialogSound.Play();

        yield return new WaitForSeconds(2.5f);

        _evilDialogGO_2.SetActive(false);
        StartCoroutine("StartEvilDialog_3");
    }

    IEnumerator StartEvilDialog_3()
    {
        _evilDialogGO_3.SetActive(true);
        _evilDialogSound.Play();

        yield return new WaitForSeconds(2.5f);

        _evilDialogGO_3.SetActive(false);
        StartCoroutine("StartPlayerDialog_3");
    }

    IEnumerator StartPlayerDialog_3()
    {
        _playerDialogGO_3.SetActive(true);
        _evilDialogSound.Play();

        yield return new WaitForSeconds(2.5f);

        _playerDialogGO_3.SetActive(false);
        StartCoroutine("StartEvilDialog_4");
    }

    IEnumerator StartEvilDialog_4()
    {
        _evilDialogGO_4.SetActive(true);
        _evilDialogSound.Play();

        yield return new WaitForSeconds(2.5f);

        _evilDialogGO_4.SetActive(false);
        StartCoroutine("StartEvilDialog_5");
    }

    IEnumerator StartEvilDialog_5()
    {
        _vanishSound.Play();
        _evilDialogGO_5.SetActive(true);
        _evilDialogSound.Play();

        yield return new WaitForSeconds(1.5f);

        _evilDialogGO_5.SetActive(false);
        _evilPlayerGO.gameObject.SetActive(false);
        _playerMovement.IsFrozen = false;
        _playerMovement.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        _freezePositionGO.SetActive(false);
        _keyGO.SetActive(true);
    }
}
