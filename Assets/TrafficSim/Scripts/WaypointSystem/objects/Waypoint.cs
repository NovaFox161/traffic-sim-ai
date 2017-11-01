using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Waypoint {
	string id = "ID";
	Vector3 location= Vector3.zero;
	int number;

	List<int> connections = new List<int>();

	//Getters
	public string getId() {
		return id;
	}

	public Vector3 getLocation() {
		return location;
	}

	public int getNumber() {
		return number;
	}

	public List<int> getConnections() {
		return connections;
	}

	//Setters
	public void setId(string _id) {
		id = _id;
	}

	public void setLocation(Vector3 _loc) {
		location = _loc;
	}

	public void setNumber(int _num) {
		number = _num;
	}

	//Functions
	public string toSavableString() {
		string wp = id + "---" + location.x + "***" + location.y  + "***" + location.z + "---" + number + "---";

		foreach (int i in connections) {
			wp += "!!!" + i;
		}

		return wp;
	}

	public Waypoint fromString(string pointData) {
		string[] wd = pointData.Split(new [] {"---"}, StringSplitOptions.None);
		setId(wd[0]);

		string[] loc = wd[1].Split(new [] {"***"}, StringSplitOptions.None);

		setLocation(new Vector3(float.Parse(loc[0]), float.Parse(loc[1]), float.Parse(loc[2])));

		setNumber(int.Parse(wd[2]));

		try {
			string[] con = wd[3].Split(new [] {"!!!"}, StringSplitOptions.None);
			foreach (string c in con) {
				getConnections().Add(int.Parse(c));
			}
		} catch (IndexOutOfRangeException e) {
			//No connections
			e.ToString();
		} catch (FormatException e) {
			//No connections probably...
			e.ToString();
		}
		return this;
	}
}