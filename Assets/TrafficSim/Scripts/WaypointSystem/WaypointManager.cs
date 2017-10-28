using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(WaypointSaver))]
public class WaypointManager : MonoBehaviour {
	static WaypointManager instance;

	WaypointSaver saver;

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
		saver = GetComponent<WaypointSaver>();

		saver.loadWaypoints();
	}
}