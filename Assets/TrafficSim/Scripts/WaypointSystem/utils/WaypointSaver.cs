using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;
using System;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class WaypointSaver : MonoBehaviour {


	public static void saveWaypoints(List<Waypoint> waypoints) {
		Scene scene = SceneManager.GetActiveScene();

		string path = "TrafficSim/Resources/Data/Waypoints/" + scene.name + "_waypoints.txt";

		#if UNITY_STANDALONE
		path = "Assets/TrafficSim/Resources/Data/Waypoints/" + scene.name + "_waypoints.txt";
		#endif

		string toSave = "";

		foreach (Waypoint w in waypoints) {
			toSave = toSave + "|||" + w.toSavableString();
		}

		using (FileStream fs = new FileStream(path, FileMode.Create)) {
			using (StreamWriter writer = new StreamWriter(fs)) {
				writer.Write(toSave);
			}
		}

		#if UNITY_EDITOR
		AssetDatabase.Refresh();
		#endif
	}

	public static List<Waypoint> loadWaypoints() {
		Scene scene = SceneManager.GetActiveScene();

		string path = "TrafficSim/Resources/Data/Waypoints/" + scene.name + "_waypoints.txt";

		#if UNITY_STANDALONE
		path = "Assets/TrafficSim/Resources/Data/Waypoints/" + scene.name + "_waypoints.txt";
		#endif

		List<Waypoint> waypoints = new List<Waypoint>();
		try {
			string waypointsString = AssetDatabase.LoadAssetAtPath<TextAsset>(path).text;

			string[] data = waypointsString.Split(new [] {"|||"}, StringSplitOptions.None);

			foreach (string w in data) {
				try {
					waypoints.Add(new Waypoint().fromString(w));
				} catch (IndexOutOfRangeException e) {
					//Debug.Log("Invalid Waypoint Data: " + e.Message);
					e.ToString();
				}
			}
		} catch (NullReferenceException e) {
			e.ToString();
		}
		return waypoints;
	}
}