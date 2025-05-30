using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Waypoint _waypoint;
    private int _currentWaypointIndex;
    private Vector3 _targetPosition;

    private void OnEnable()
    {
        _waypoint = FindFirstObjectByType<Waypoint>();
        _currentWaypointIndex = 0;

        if (_waypoint != null && _waypoint.Points.Length > 0)
        {
            _targetPosition = _waypoint.GetWaypointPosition(_currentWaypointIndex);
        }
    }

    void Update()
    {
        if (_waypoint == null || _currentWaypointIndex >= _waypoint.Points.Length)
        {
            ObjectPooler.ReturnToPool(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex < _waypoint.Points.Length)
            {
                _targetPosition = _waypoint.GetWaypointPosition(_currentWaypointIndex);
            }
            else
            {
                // Raggiunto l'ultimo waypoint
                ObjectPooler.ReturnToPool(gameObject);
            }
        }
    }

}
