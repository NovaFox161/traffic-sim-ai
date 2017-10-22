using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class vehicle : MonoBehaviour {
	[SerializeField] CarType carType;
	[SerializeField] List<Wheel> wheels = new List<Wheel>();

	[Range(1, 4)]
	[SerializeField] int seats = 1;

	[SerializeField] float maxSpeed;
	[SerializeField] float speed;

	Rigidbody rb;

	//Getters
	public CarType getCarType() {
		return carType;
	}

	public List<Wheel> getWheels() {
		return wheels;
	}

	public Wheel getWheel(WheelType type) {
		foreach (Wheel w in wheels) {
			if (w.type == type) {
				return w;
			}
		}
		return null;
	}

	public int getSeats() {
		return seats;
	}

	public float getMaxSpeed() {
		return maxSpeed;
	}

	public float getSpeed() {
		return speed;
	}
		
	//Setters
	public void setCarType(CarType _type) {
		carType = _type;
	}

	public void setSeats(int _seats) {
		seats = _seats;
	}

	public void setMaxSpeed(float _maxSpeed) {
		maxSpeed = _maxSpeed;
	}

	//Functions
	public void accelerate(float multiplier) {
		if (speed <= 0) {
			speed = 1;
		}
			
		speed = speed * multiplier;
	}

	//Unity Methods
	void Start() {
		rb = GetComponent<Rigidbody>();

		accelerate(20);
	}

	void FixedUpdate() {
		Vector3 currentRot = getWheel(WheelType.Back_right).wheel.transform.rotation.eulerAngles;
		float appliedRot = speed * Time.fixedDeltaTime;
		Vector3 newRot = new Vector3(appliedRot, 0, 0);

		getWheel(WheelType.Back_right).wheel.transform.Rotate(newRot);
		getWheel(WheelType.Back_left).wheel.transform.Rotate(newRot);
		//transform.position = new Vector3(transform.position.x * appliedRot, transform.position.y, transform.position.z);
	}
}

[System.Serializable]
public class Wheel {

	public GameObject wheel;
	public WheelType type;
}

[System.Serializable]
public enum CarType {
	Sedan, Van, Truck, Bus, Motorcycle
}

[System.Serializable]
public enum WheelType {
	Front_left, Front_right, Back_left, Back_right
}