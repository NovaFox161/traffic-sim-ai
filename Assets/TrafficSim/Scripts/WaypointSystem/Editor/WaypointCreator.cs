using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;

public class WaypointCreator : EditorWindow {
	static WaypointCreator instance;
	Vector3 currentPosition;

	bool creatingPoint;
	bool showAll;

	GameObject point;
	string lastPointId;

	List<GameObject> allPoints = new List<GameObject>();

	static WaypointCreator() {
		SceneView.onSceneGUIDelegate += Update;
	}

	void OnEnable() {
		instance = this;
	}

	[MenuItem("TrafficSimAI/Waypoints/Creator")]
	public static void ShowWindow() {
		//Show existing window instance. If one doesn't exist, make one.
		GetWindow(typeof(WaypointCreator), false, "Point Creator");
		instance = GetWindow<WaypointCreator>();
	}

	public static void Update(SceneView view) {
		if (instance != null) {
			if (!EditorApplication.isPlaying && Event.current.capsLock && instance.creatingPoint) {
				Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, WaypointSettings.getRange())) {
					float roundedX = Mathf.Round(hit.point.x * 1000) / 1000;
					float roundedY = Mathf.Round(hit.point.y * 1000) / 1000;
					float roundedZ = Mathf.Round(hit.point.z * 1000) / 1000;
					instance.currentPosition = new Vector3(roundedX, roundedY, roundedZ);
					GetWindow(typeof(WaypointCreator)).Repaint();
				} else {
					if (instance.currentPosition != Vector3.zero) {
						instance.currentPosition = Vector3.zero;
						GetWindow(typeof(WaypointCreator)).Repaint();
					}
				}
			}
		}
	}

	void OnGUI() {
		if (instance == null) {
			instance = this;
		}
		GUILayout.Label("Settings", EditorStyles.boldLabel);

		creatingPoint = EditorGUILayout.Toggle("Creating Point", creatingPoint);

		showAll = EditorGUILayout.Toggle("Show All", showAll);

		GUILayout.Label("Mouse Location", EditorStyles.boldLabel);
		currentPosition = EditorGUILayout.Vector3Field("Unity World Space", currentPosition);

		if (GUILayout.Button("Confirm Point")) {
			if (creatingPoint) {
				//Confirm waypoint
				saveNewWaypoint();
			}
		}

		if (showAll) {
			showAllPoints();
		} else {
			hideAllPoints();
		}

		if (creatingPoint) {
			moveWaypointPlacer();
		} else {
			hideWaypointPlacer();
		}
	}

	void moveWaypointPlacer() {
		if (point == null) {
			point = GameObject.CreatePrimitive(PrimitiveType.Cube);
			point.GetComponent<BoxCollider>().enabled = false;
			point.name = "Waypoint Placer";
		}

		point.transform.position = currentPosition;
		point.transform.localScale = Vector3.one;
	}

	void hideWaypointPlacer() {
		if (point != null) {
			DestroyImmediate(point);
		}
	}

	void saveNewWaypoint() {
		Waypoint wp = new Waypoint();
		wp.setId(Guid.NewGuid().ToString());
		wp.setLocation(currentPosition);

		lastPointId = wp.getId();

		List<Waypoint> points = WaypointSaver.loadWaypoints();
		points.Add(wp);
		WaypointSaver.saveWaypoints(points);

		hideWaypointPlacer();
	}

	void showAllPoints() {
		if (allPoints.Count < 1) {
			//Empty...
			foreach (Waypoint p in WaypointSaver.loadWaypoints()) {
				GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				g.GetComponent<Collider>().enabled = false;
				g.name = p.getId();
				g.transform.position = p.getLocation();
				g.transform.localScale = Vector3.one;
				//g.GetComponent<Renderer>().material.color = Color.red;
				allPoints.Add(g);
			}
		}
	}

	void hideAllPoints() {
		foreach (GameObject g in allPoints) {
			DestroyImmediate(g);
		}

		allPoints.Clear();
	}
}