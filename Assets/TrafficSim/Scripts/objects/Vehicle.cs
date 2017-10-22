using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class Vehicle : MonoBehaviour {
	[SerializeField] CarType carType;
	[SerializeField] List<Wheel> wheels = new List<Wheel>();

	[Range(1, 4)]
	[SerializeField] int seats = 1;

	[SerializeField] float maxSpeed;
	[SerializeField] float speed;

	[SerializeField] float currentFriction = 0.5f;
	[SerializeField] float breakFriction = 15;

	[SerializeField] bool accelerating;
	[SerializeField] bool breaking;

	[SerializeField] Driver driver;

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

	public float getCurrentFriction() {
		return currentFriction;
	}

	public float getBreakFriction() {
		return breakFriction;
	}

	public bool isAccelerating() {
		return accelerating;
	}

	public bool isBreaking() {
		return breaking;
	}

	public Driver getDriver() {
		return driver;
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

	public void setCurrentFriction(float _friction) {
		currentFriction = _friction;
	}

	public void setBreakFriction(float _friction) {
		breakFriction = _friction;
	}

	public void setAccelerating(bool _accelerating) {
		accelerating = _accelerating;
	}

	public void setBreaking(bool _breaking) {
		breaking = _breaking;
	}

	public void setDriver(Driver _driver) {
		driver = _driver;
	}

	//Functions

	//Unity Methods
	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	void Update() {
		if (driver != null) {
			driver.setVehicle(this);
		}
	}

	void FixedUpdate() {
		//Calculate speed...
		if (accelerating) {
			//Go faster!!!
			float newSpeed = speed + Time.fixedDeltaTime;
			speed = newSpeed >= maxSpeed ? maxSpeed : newSpeed;
		} else {
			if (breaking) {
				float newSpeed = speed - (Time.fixedDeltaTime * breakFriction);
				speed = newSpeed <= 0 ? 0 : newSpeed;
			} else {
				//Slow down! (but only to 0!!!)
				float newSpeed = speed - (Time.fixedDeltaTime * currentFriction);
				speed = newSpeed <= 0 ? 0 : newSpeed;
			}
		}


		Vector3 newRot = new Vector3(speed, 0, 0);

		foreach (Wheel w in getWheels()) {
			w.wheel.transform.Rotate(newRot, Space.Self);
		}
		transform.Translate(Vector3.forward * Time.fixedDeltaTime * speed, Space.Self);
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