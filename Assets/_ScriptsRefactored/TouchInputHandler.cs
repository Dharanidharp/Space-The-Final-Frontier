using System.Collections;
using UnityEngine;

public class TouchInputHandler : MonoBehaviour {

    public delegate void TapAction(Touch t); // Holds a reference to a method
    public static event TapAction OnTap; // A message sent to other objects

    public float maxTapMovement = 50f;

    private Vector2 movement;

    private bool tapGestureFailed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0]; // Contains all the current touches

            if (touch.phase == TouchPhase.Began)
            {
                movement = Vector2.zero; // Set the movement to zero when the touch phase begins
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                movement += touch.deltaPosition; // Movement of touch since the last reading

                if (movement.magnitude > maxTapMovement)
                {
                    tapGestureFailed = true;
                }
                else
                {
                    if (!tapGestureFailed)
                    {
                        if (OnTap != null) // If null, then no other scripts have registered the event
                            OnTap(touch); // Pass the current touch into the OnTap event
                    }

                    tapGestureFailed = false;
                }
            }
        }
	}
}
