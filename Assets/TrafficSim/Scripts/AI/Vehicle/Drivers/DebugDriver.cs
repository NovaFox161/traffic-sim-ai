using UnityEngine;


public class DebugDriver : Driver {

	void Start() {
		if (getVehicle() != null) {
			setCurrentCam(0);
		}
	}

	void Update() {
		if (getVehicle() != null) {
			setRawInputs(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), Mathf.Abs(Input.GetAxis("Jump")));

			//Change camera view
			if (Input.GetKeyDown(KeyCode.Tab)) {
				setCurrentCam(getCurrentCam() + 1);
			}
		}
	}
}