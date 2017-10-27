using UnityEngine;


public class DebugDriver : Driver {

	void Update() {
		if (getVehicle() != null) {
			setRawInputs(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), Mathf.Abs(Input.GetAxis("Jump")));
		}
	}
}