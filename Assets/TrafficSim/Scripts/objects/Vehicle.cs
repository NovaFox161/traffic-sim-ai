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
	[SerializeField] float acceleration;

	[SerializeField] float maxMotorTorque;
	[SerializeField] float maxSteeringAngle;

	[SerializeField] float motorRaw;
	[SerializeField] float steeringRaw;
	[SerializeField] float brakeTorqueRaw;

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

	public float getMaxMotorTorque() {
		return maxMotorTorque;
	}

	public float getMaxSteeringAngle() {
		return maxSteeringAngle;
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

	public void setMaxMotorTorque(float _max) {
		maxMotorTorque = _max;
	}

	public void setMaxSteeringAngle(float _max) {
		maxSteeringAngle = _max;
	}

	public void setMotorRaw(float _raw) {
		motorRaw = _raw;
	}

	public void setSteeringRaw(float _raw) {
		steeringRaw = _raw;
	}

	public void setBrakeTorqueRaw(float _raw) {
		brakeTorqueRaw = _raw;
	}

	public void setDriver(Driver _driver) {
		driver = _driver;
	}

	//Functions
	public void visualizeWheel(Wheel w) {
		Quaternion rot;
		Vector3 pos;
		w.wheelCol.GetWorldPose ( out pos, out rot);
		w.wheelMesh.transform.position = pos;
		w.wheelMesh.transform.rotation = rot;
	}

	//Unity Methods
	void Awake() {
		if (driver != null) {
			driver.setVehicle(this);
		}
	}

	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	void Update() {
		//Calculate acceleration and speed
		float currentSpeed = Mathf.Round(rb.velocity.magnitude * 3.6f * 100) / 100;

		acceleration = Mathf.Round((currentSpeed - speed) / Time.deltaTime / 3.6f * 100) / 100;

		speed = currentSpeed;
	}

	void FixedUpdate() {
		float motor = maxMotorTorque * motorRaw;
		float steering = maxSteeringAngle * steeringRaw;
		float brakeTorque = brakeTorqueRaw;

		if (brakeTorque > 0.001) {
			brakeTorque = maxMotorTorque;
			motor = 0;
		} else {
			brakeTorque = 0;
		}

		foreach (Wheel w in wheels) {
			if (w.steering) {
				w.wheelCol.steerAngle = ((w.reverseTurn)?-1:1)*steering;
			}

			if (w.motor) {
				w.wheelCol.motorTorque = speed <= maxSpeed ? motor : 0;
			}

			w.wheelCol.brakeTorque = brakeTorque;

			visualizeWheel(w);
		}
	}

	void LateUpdate() {
		if (driver != null) {
			driver.setVehicle(this);
		}
	}
}

[System.Serializable]
public class Wheel {
	public GameObject wheelMesh;
	public WheelCollider wheelCol;
	public WheelType type;
	public bool steering;
	public bool motor;
	public bool reverseTurn;
}

[System.Serializable]
public enum CarType {
	Sedan, Van, Truck, Bus, Motorcycle
}

[System.Serializable]
public enum WheelType {
	Front_left, Front_right, Back_left, Back_right
}