using UnityEngine;
using System.Collections.Generic;

public class WaypointDebug : MonoBehaviour {
	public bool debug;
	public bool showAllPoints;

	static bool creatingPoints;
	static Vector3 currentLocation;

	//Getters
	public static bool isCreatingPoints() {
		return creatingPoints;
	}

	public static Vector3 getCurrentLocation() {
		return currentLocation;
	}

	//Setters
	public static void setCreatingPoints(bool _creatingPoints) {
		creatingPoints = _creatingPoints;
	}

	public static void setCurrentLocation(Vector3 loc) {
		currentLocation = loc;
	}

	//Unity Methods
	void OnDrawGizmos() {
		if (creatingPoints) {
			Gizmos.color = Color.blue;
			Gizmos.DrawCube(currentLocation, Vector3.one);
		}

		if (debug) {
			if (showAllPoints) {
				Waypoint lastPoint = null;
				List<Waypoint> points = WaypointSaver.loadWaypoints();
				foreach (Waypoint p in points) {
					Gizmos.color = Color.red;
					Gizmos.DrawSphere(p.getLocation(), 1);

					//Make the lines to waypoints
					if (lastPoint != null) {
						Gizmos.color = Color.green;
						Gizmos.DrawLine(p.getLocation(), lastPoint.getLocation());
					}

					lastPoint = p;
				}
			}
		}
	}
}