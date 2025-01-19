using UnityEngine;

public class Crook : MonoBehaviour
{
    [SerializeField] private Transform[] _controlPoints;
    [SerializeField] private float _speed;

    private Transform _currentControlPoint;
    private int _currentIndexPoint;

    private void Start()
    {
        _currentControlPoint = _controlPoints[_currentIndexPoint];
    }

    private void Update()
    {
        if (transform.position.x != _currentControlPoint.position.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentControlPoint.position, _speed * Time.deltaTime);
        }
        else 
        {
            UpdateTargetPoint();
        }
    }

    private void UpdateTargetPoint() 
    {
        if (_currentIndexPoint < _controlPoints.Length - 1)
        {
            _currentIndexPoint++;
            _currentControlPoint = _controlPoints[_currentIndexPoint];
        }
    }
}
