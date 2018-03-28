using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Elenesski.Camera.Utilities {

    public class GenericMoveCameraInputs : MonoBehaviour {
		public Text DebugText;
        public bool isSlowModifier;         // Slows the movement down by a factor
        public bool isFastModifier;         // Speeds the movement up by a factor
        public bool isRotateAction;         // Indicates that the camera is rotating.
        public Vector2 RotateActionStart;   // The X,Y position where the right mouse was clicked
        public bool isLockForwardMovement;  // Turns of forward dampening while on
        public bool ResetMovement;          // Stops all movement
        public bool isPanLeft;              // Tells the system to pan left
        public bool isPanRight;             // Tells the system to pan right
        public bool isPanUp;                // Tells the system to pan up
        public bool isPanDown;              // Tells the system to pan down
        public bool isMoveForward;          // Moves the camera forward
        public bool isMoveBackward;         // Moves the camera backward
        public bool isMoveForwardAlt;       // Moves the camera forward (alternate)
        public bool isMoveBackwardAlt;      // Moves the camera backward (alternate)

        public virtual void Initialize() {
            RotateActionStart = new Vector2();
        }

        public virtual void QueryInputSystem() {

			if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				// Get movement of the finger since last frame
				Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
				if (Mathf.Abs(transform.rotation.x) >= 85.0f) {
					touchDeltaPosition.y = 0.0f;
				}
				if (touchDeltaPosition.y > 0.0f) {
					isPanUp = true;
					isPanDown = false;
				} else {
					isPanUp = false;
					isPanDown = true;
				}
				if (touchDeltaPosition.x > 0.0f) {
					isPanRight = true;
					isPanLeft = false;
				} else {
					isPanRight = false;
					isPanLeft = true;
				}
				DebugText.text += touchDeltaPosition + "\n";
			}
			// If there are two touches on the device...
			if (Input.touchCount == 2)
			{
				// Store both touches.
				Touch touchZero = Input.GetTouch(0);
				Touch touchOne = Input.GetTouch(1);

				// Find the position in the previous frame of each touch.
				Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

				// Find the magnitude of the vector (the distance) between the touches in each frame.
				float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
				float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

				// Find the difference in the distances between each frame.
				float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

				isMoveForward = deltaMagnitudeDiff > 0;
				isMoveBackward = deltaMagnitudeDiff < 0;

				DebugText.text += deltaMagnitudeDiff + "\n";
			}

            isSlowModifier = (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl));
            isFastModifier = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
            isRotateAction = Input.GetButton("Fire2");

            // Get mouse starting point when the button was clicked.
            if ( Input.GetButtonDown("Fire2") ) {
                RotateActionStart.x = Input.mousePosition.x;
                RotateActionStart.y = Input.mousePosition.y;
            }
            isLockForwardMovement = Input.GetButton("Fire3");
            ResetMovement = Input.GetKey(KeyCode.Space);

            isPanLeft = Input.GetKey(KeyCode.A);
            isPanRight = Input.GetKey(KeyCode.D);
            isPanUp = Input.GetKey(KeyCode.Q);
            isPanDown = Input.GetKey(KeyCode.Z);

            isMoveForward = Input.GetKey(KeyCode.W);
            isMoveBackward = Input.GetKey(KeyCode.S);

            isMoveForwardAlt = Input.GetAxis("Mouse ScrollWheel") > 0;
            isMoveBackwardAlt = Input.GetAxis("Mouse ScrollWheel") < 0;

        }

    }
}