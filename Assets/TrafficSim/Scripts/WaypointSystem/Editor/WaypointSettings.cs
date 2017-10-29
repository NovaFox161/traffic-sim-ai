using UnityEditor;
using UnityEngine;

public class WaypointSettings : EditorWindow {
	static WaypointSettings instance;
	static float range = 1000;

	void OnEnable() {
		instance = this;
	}

	[MenuItem("TrafficSimAI/Editor Settings")]
	public static void ShowWindow() {
		//Show existing window instance. If one doesn't exist, make one.
		GetWindow(typeof(WaypointSettings), false, "TrafficSimAI");
		instance = GetWindow<WaypointSettings>();
	}

	void OnGUI() {
		if (instance == null) {
			instance = this;
		}
		GUILayout.Label("Settings", EditorStyles.boldLabel);

		range = EditorGUILayout.FloatField("Mouse Range", range);
	}

	//Getters
	public static float getRange() {
		return range;
	}
}