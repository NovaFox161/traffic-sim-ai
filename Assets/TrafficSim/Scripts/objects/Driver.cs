using UnityEngine;

public class Driver : MonoBehaviour {
	[SerializeField] Vehicle vehicle;

	//Getters
	public Vehicle getVehicle() {
		return vehicle;
	}

	//Setters
	public void setVehicle(Vehicle _vehicle) {
		vehicle = _vehicle;
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
	}

	void LateUpdate() {
		if (vehicle != null) {
			vehicle.setDriver(this);
		}
	}
}