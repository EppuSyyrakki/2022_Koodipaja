using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewInputSwipe : MonoBehaviour
{
    [SerializeField]
    private float tapMaxDelta = 0.25f;

    [SerializeField]
    private float maxSwipeDuration = 0.5f;

    [SerializeField]
    private float maxSwipeYdelta = 0.5f;

    [SerializeField]
    private Vector3 _startPosition;

    [SerializeField]
    private Vector3 _endPosition;
    private Vector3 _currentPosition;

    private float _startTime;
    private float _endTime;

    private bool _recordPositions;
    private List<Vector3> _positions;

    private void OnTouchPosition(InputAction.CallbackContext context)
    {
        Vector3 screenPosition = context.ReadValue<Vector2>();
        _currentPosition = GetWorldPosition(screenPosition);

        if (_recordPositions)
        {
            _positions.Add(_currentPosition);
        }
    }

    private void OnTouchContact(InputAction.CallbackContext context)
	{
        if (context.performed) 
        {
            Debug.Log("Touch started");
            _startPosition = _currentPosition;
            _startTime = Time.time;

            _positions = new List<Vector3>();
            _recordPositions = true;           
        }
        
        if (context.canceled)
		{
            Debug.Log("Touch ended");
            _endPosition = _currentPosition;
            _endTime = Time.time;
            _recordPositions = false;

            ProcessSwipe();          
		}
	}  

    private void ProcessSwipe()
    {
        float duration = _endTime - _startTime;

        if (duration > maxSwipeDuration)
        {
            Debug.Log("Twas a Long Touch!");
            DrawDebugLines(_positions, Color.blue);
            return;
        }

        float distance = Vector3.Distance(_startPosition, _endPosition);

        if (distance < tapMaxDelta)
        {
            Debug.Log("Twas a Tap!");
            DrawCross(_endPosition);
            return;
        }

        if (Mathf.Abs(_endPosition.y - _startPosition.y) > maxSwipeYdelta)
        {
            Debug.Log("Swiped too vertically");
            DrawDebugLines(_positions, Color.red);
            return;
        }

        if (_endPosition.x < _startPosition.x)
        {
            Debug.Log("Swiped left");
        }
        if (_endPosition.x > _startPosition.x)
        {
            Debug.Log("Swiped right");
        }

        DrawDebugLines(_positions, Color.yellow);
    }

    private void DrawDebugLines(List<Vector3> positions, Color color)
	{
        for (int i = 1; i < positions.Count; i++)
		{
            Vector3 previous = positions[i - 1];
            Vector3 current = positions[i];

            Debug.DrawLine(previous, current, color, 3f);
		}
	}

    private void DrawCross(Vector3 position)
	{
        Debug.DrawLine(position, position + Vector3.up, Color.green, 3f);
        Debug.DrawLine(position, position + Vector3.down, Color.green, 3f);
        Debug.DrawLine(position, position + Vector3.left, Color.green, 3f);
        Debug.DrawLine(position, position + Vector3.right, Color.green, 3f);
    }

    private static Vector3 GetWorldPosition(Vector2 screenPosition)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPos.z = 0;
        return worldPos;
    }
}
