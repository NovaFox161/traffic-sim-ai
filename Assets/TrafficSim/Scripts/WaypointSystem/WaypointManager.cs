using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(WaypointSaver))]
public class WaypointManager : MonoBehaviour {
	static WaypointManager instance;

	[SerializeField] List<Waypoint> waypoints = new List<Waypoint>();

	public static WaypointManager getManager() {
		return instance;
	}

	//Unity methods
	void Awake() {
		if (instance != null) {
			Destroy(gameObject);
		} else {
			DontDestroyOnLoad(gameObject);
		}

		instance = this;

		waypoints = WaypointSaver.loadWaypoints();
	}

	//Getters
	public List<Waypoint> getWaypoints() {
		return waypoints;
	}
}