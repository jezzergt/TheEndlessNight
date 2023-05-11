using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject[] _waypointArray;

    [Header("Primitives")]
    [SerializeField] private float _speed = 2f;
    private int _currentWaypointIndex = 0;

    private void Update()
    {
        if (Vector2.Distance(_waypointArray[_currentWaypointIndex].transform.position, 
            transform.position) < .1f)
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= _waypointArray.Length)
            {
                _currentWaypointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position,
            _waypointArray[_currentWaypointIndex].transform.position, Time.deltaTime * _speed);
    }
}
