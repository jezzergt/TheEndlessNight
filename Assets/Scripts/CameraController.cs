using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _player;
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        transform.position = new Vector3(_player.position.x, _player.position.y, transform.position.z);
    }
}
