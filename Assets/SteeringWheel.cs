using UnityEngine;
using System.Collections;

public class SteeringWheel : MonoBehaviour {

	Vector2 rectWordPos;
	float initAngle;
	float dragAngle;
	RectTransform rectTran;
	public float angleThreshold = 45f;

	// Use this for initialization
	void OnEnable () {
		Lean.LeanTouch.OnFingerDown += OnFingerDown;
		Lean.LeanTouch.OnFingerUp += OnFingerUp;
		Lean.LeanTouch.OnFingerDrag += OnFingerDrag;
	}

	void Start(){
		rectTran = GetComponent<RectTransform> ();
		rectWordPos = RectTransformExtension.GetScreenWorldPos (rectTran);
	}
	
	void OnFingerDown(Lean.LeanFinger fg){
		initAngle = fg.GetDegrees (rectWordPos);
		dragAngle = initAngle;
	}

	void OnFingerDrag(Lean.LeanFinger fg){
		dragAngle += fg.GetDeltaDegrees(rectWordPos);
		float angleDiff = (dragAngle - initAngle) * -1;
		Debug.Log (angleDiff);

		if(Mathf.Abs(angleDiff) < angleThreshold){
			rectTran.Rotate (Vector3.forward, fg.GetDeltaDegrees(rectWordPos));
		}
	}

	void OnFingerUp(Lean.LeanFinger fg){
		//reset rotation
	}
}
