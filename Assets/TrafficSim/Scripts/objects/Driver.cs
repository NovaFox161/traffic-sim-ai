using UnityEngine;

public class Driver : MonoBehaviour {
	[SerializeField] Vehicle vehicle;

	[SerializeField] int currentCam;

	//Getters
	public Vehicle getVehicle() {
		return vehicle;
	}

	public int getCurrentCam() {
		return currentCam;
	}

	//Setters
	public void setVehicle(Vehicle _vehicle) {
		vehicle = _vehicle;
	}

	public void setCurrentCam(int _cam) {
		if (vehicle == null) {
			currentCam = -1;
			return;
		}

		int oldCam = currentCam;
		currentCam = _cam;
		if (vehicle.getCameras().Count == _cam) {
			//Reset to 0
			currentCam = 0;
		}
		if (currentCam == -1) {
			//No camera
			foreach (CameraView c in vehicle.getCameras()) {
				c.cam.gameObject.SetActive(false);
			}
		} else {
			if (oldCam != -1) {
				vehicle.getCameras()[oldCam].cam.gameObject.SetActive(false);
			}

			vehicle.getCameras()[currentCam].cam.gameObject.SetActive(true);
		}
	}

	//Functions
	public void setRawInputs(float motor, float steering, float brake) {
		vehicle.setMotorRaw(motor);
		vehicle.setSteeringRaw(steering);
		vehicle.setBrakeTorqueRaw(brake);
	}

	//Unity methods
	void Awake() {
		if (vehicle != null) {
			vehicle.setDriver(this);
		}
		currentCam = -1;
	}

	void LateUpdate() {
		if (vehicle != null) {
			vehicle.setDriver(this);
		}
	}
}