using UnityEngine;

[System.Serializable]
public class Waypoint {
	string id = "ID";
	Vector3 location= Vector3.zero;

	//Getters
	public string getId() {
		return id;
	}

	public Vector3 getLocation() {
		return location;
	}

	//Setters
	public void setId(string _id) {
		id = _id;
	}

	public void setLocation(Vector3 _loc) {
		location = _loc;
	}

	//Functions
	public string toSavableString() {
		return id + "---" + location.x + "***" + location.y  + "***" + location.z;
	}
}