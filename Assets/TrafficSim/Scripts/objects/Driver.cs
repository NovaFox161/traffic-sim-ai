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
	public void applyBreak() {
		vehicle.setBreaking(true);
		vehicle.setAccelerating(false);
	}

	public void accelerate() {
		vehicle.setBreaking(false);
		vehicle.setAccelerating(true);
	}

	public void stopAccelerating() {
		vehicle.setAccelerating(false);
	}
}