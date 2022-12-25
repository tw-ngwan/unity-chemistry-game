using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
	public Transform target;
	public float speed = 5;

	bool _rightButtonDown;
	bool _leftButtonDown;

	// Update is called once per frame
	void Update ()
	{
		if (_rightButtonDown) {
			float moveRight = (Time.deltaTime * speed);
			target.Translate (moveRight, 0, 0);
		}
		if (_leftButtonDown) {
			float moveLeft = (Time.deltaTime * speed);
			target.Translate (-moveLeft, 0, 0);
		}
	}

	public void OnRightButtonDown (bool down)
	{
		_rightButtonDown = down;
	}

	public void OnLeftButtonDown (bool down)
	{
		_leftButtonDown = down;
	}
}
