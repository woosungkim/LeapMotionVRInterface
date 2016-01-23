using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ShortcutSettings))]
public class ShortcutSettingsEditor : Editor {

	ShortcutSettings _sSettings;

	void OnEnable() {
		_sSettings = target as ShortcutSettings; 
	}


	public override void OnInspectorGUI() {
		EditorGUILayout.BeginVertical ();


		_sSettings.ShortcutName = EditorGUILayout.TextField ("Shortcut Name", _sSettings.ShortcutName);

		_sSettings.AutoStart = EditorGUILayout.Toggle ("Auto Start", _sSettings.AutoStart);

		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.PrefixLabel ("Distance From MainCamera");
		_sSettings.DistanceFromMainCamera = EditorGUILayout.Slider (_sSettings.DistanceFromMainCamera, 0.3f, 1.0f, null);
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.PrefixLabel ("X Position");
		_sSettings.XPosition = EditorGUILayout.Slider (_sSettings.XPosition, 0.0f, 1.0f, null);
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.PrefixLabel ("Y Position");
		_sSettings.YPosition = EditorGUILayout.Slider (_sSettings.YPosition, 0.0f, 1.0f, null);
		EditorGUILayout.EndHorizontal ();

		_sSettings.Type = (ShortcutType)EditorGUILayout.EnumPopup ("Shortcut Type", _sSettings.Type);

		if (_sSettings.Type == ShortcutType.Arc) {
			_sSettings.InnerRadius = EditorGUILayout.FloatField ("Inner Radius", _sSettings.InnerRadius);
			_sSettings.Thickness = EditorGUILayout.FloatField ("Thickness", _sSettings.Thickness);
			_sSettings.EachItemDegree = EditorGUILayout.FloatField ("Each Item Degree", _sSettings.EachItemDegree);

		} else if (_sSettings.Type == ShortcutType.Stick) {
			_sSettings.Direction = (StickShortcutDirection)EditorGUILayout.EnumPopup ("Direction", _sSettings.Direction);
			_sSettings.ItemWidth = EditorGUILayout.FloatField ("Each Item Width", _sSettings.ItemWidth);
			_sSettings.ItemHeight = EditorGUILayout.FloatField ("Each Item Height", _sSettings.ItemHeight);

		}

		EditorGUILayout.EndVertical ();



		if (GUI.changed)
			EditorUtility.SetDirty (target);
	}
}