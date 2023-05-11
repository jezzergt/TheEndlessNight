using UnityEngine;

public class DropBelowPlatform : MonoBehaviour
{
    [SerializeField] private float _waitTime;

    private PlatformEffector2D _platformEffector;

    private void Start()
    {
        _platformEffector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            _waitTime = 0.5f;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) 
        {
            if (_waitTime <= 0)
            {
                _platformEffector.rotationalOffset = 180;
                _waitTime = 0.5f;
            }
            else
            {
                _waitTime -= Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _platformEffector.rotationalOffset = 0;
        }
    }
}
