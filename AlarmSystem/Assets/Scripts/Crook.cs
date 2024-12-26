using UnityEngine;

public class Crook : MonoBehaviour
{
    [SerializeField] private Transform[] _controlPoints;
    [SerializeField] private float _speed;

    private Transform _currentcontrolPoint;
    private int _currentIndexPoint;

    private void Start()
    {
        _currentcontrolPoint = _controlPoints[_currentIndexPoint];
    }

    private void Update()
    {
        if (transform.position.x != _currentcontrolPoint.position.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentcontrolPoint.position, _speed * Time.deltaTime);
        }
        else 
        {
            UpdateDirection();
        }
    }

    private void UpdateDirection() 
    {
        if (_currentIndexPoint < _controlPoints.Length - 1)
        {
            _currentIndexPoint++;
            _currentcontrolPoint = _controlPoints[_currentIndexPoint];
        }
    }
}
