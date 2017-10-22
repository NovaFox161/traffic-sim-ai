using UnityEngine;


public class DebugDriver : Driver {

	void Update() {
		if (getVehicle() != null) {
			if (Input.GetKey(KeyCode.W)) {
				accelerate();
			} else if (Input.GetKey(KeyCode.S)) {
				applyBreak();
			} else {
				stopAccelerating();
			}
		}
	}
}