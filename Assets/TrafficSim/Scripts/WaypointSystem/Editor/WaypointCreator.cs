using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;

public class WaypointCreator : EditorWindow {
	static WaypointCreator instance;

	string lastPointId;

	Vector2Int points;
	int toDelete;

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
			if (!EditorApplication.isPlaying && Event.current.capsLock && WaypointDebug.isCreatingPoints()) {
				if (Event.current.shift) {
					Debug.Log("STUFS!!!s");
				}
				Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, WaypointSettings.getRange())) {
					float roundedX = Mathf.Round(hit.point.x * 1000) / 1000;
					float roundedY = Mathf.Round(hit.point.y * 1000) / 1000;
					float roundedZ = Mathf.Round(hit.point.z * 1000) / 1000;
					WaypointDebug.setCurrentLocation(new Vector3(roundedX, roundedY, roundedZ));
					GetWindow(typeof(WaypointCreator)).Repaint();
				} else {
					if (WaypointDebug.getCurrentLocation() != Vector3.zero) {
						WaypointDebug.setCurrentLocation(Vector3.zero);
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
		WaypointDebug.setCreatingPoints(EditorGUILayout.Toggle("Creating Point", WaypointDebug.isCreatingPoints()));

		EditorGUILayout.Space();
		WaypointDebug.setCurrentLocation(EditorGUILayout.Vector3Field("", WaypointDebug.getCurrentLocation()));

		if (GUILayout.Button("Make new Point")) {
			if (WaypointDebug.isCreatingPoints()) {
				saveNewWaypoint();
			}
		}

		//Connect/Disconnect points
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Connect/Disconnect Points");
		points = EditorGUILayout.Vector2IntField("", points);

		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Connect")) {
			if (points.x != points.y) {
				connectPoints();
			}
		}
			
		if (GUILayout.Button("Disconnect")) {
			if (points.x != points.y) {
				disconnectPoints();
			}
		}
		GUILayout.EndHorizontal();

		//Delete point..
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Delete Point");
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Delete")) {
			//TODO: Delete point...
		}
		toDelete = EditorGUILayout.IntField("", toDelete);
		EditorGUILayout.EndHorizontal();
	}

	void saveNewWaypoint() {
		Waypoint wp = new Waypoint();
		wp.setId(Guid.NewGuid().ToString());
		wp.setLocation(WaypointDebug.getCurrentLocation());

		lastPointId = wp.getId();

		List<Waypoint> list = WaypointSaver.loadWaypoints();
		wp.setNumber(list.Count);
		list.Add(wp);
		WaypointSaver.saveWaypoints(list);
	}

	void connectPoints() {
		List<Waypoint> list = WaypointSaver.loadWaypoints();

		if (points.x > -1 && points.x < list.Count && points.y > -1 && points.y < list.Count) {
			foreach (Waypoint p in list) {
				if (p.getNumber() == points.x) {
					p.getConnections().Add(points.y);
				} else if (p.getNumber() == points.y) {
					p.getConnections().Add(points.x);
				}
			}
			WaypointSaver.saveWaypoints(list);
		}
	}

	void disconnectPoints() {
		List<Waypoint> list = WaypointSaver.loadWaypoints();

		if (points.x > -1 && points.x < list.Count && points.y > -1 && points.y < list.Count) {
			foreach (Waypoint p in list) {
				if (p.getNumber() == points.x) {
					if (p.getConnections().Contains(points.y)) {
						p.getConnections().Remove(points.y);
					}
				} else if (p.getNumber() == points.y) {
					if (p.getConnections().Contains(points.x)) {
						p.getConnections().Remove(points.x);
					}
				}
			}
			WaypointSaver.saveWaypoints(list);
		}
	}
}