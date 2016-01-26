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

		GUIStyle headingStyle = new GUIStyle(GUI.skin.label);
		headingStyle.fontStyle = FontStyle.Bold;
		headingStyle.fontSize = 14;

		EditorGUILayout.BeginVertical ();
	
		// layout settings
		EditorGUILayout.LabelField ("");
		EditorGUILayout.LabelField ("Layout Settings", headingStyle);

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


		// type settings
		EditorGUILayout.LabelField ("");
		EditorGUILayout.LabelField ("Type Settings", headingStyle );
		//if (typeListFold = EditorGUILayout.Foldout (typeListFold, "Type Settings")) {
		_sSettings.Type = (ShortcutType)EditorGUILayout.EnumPopup ("Type", _sSettings.Type);

		if (_sSettings.Type == ShortcutType.Arc) {
			_sSettings.InnerRadius = EditorGUILayout.FloatField ("Inner Radius", _sSettings.InnerRadius);
			_sSettings.Thickness = EditorGUILayout.FloatField ("Thickness", _sSettings.Thickness);
			_sSettings.EachItemDegree = EditorGUILayout.FloatField ("Each Item Degree", _sSettings.EachItemDegree);
			
		} else if (_sSettings.Type == ShortcutType.Stick) {
			_sSettings.Direction = (StickShortcutDirection)EditorGUILayout.EnumPopup ("Direction", _sSettings.Direction);
			_sSettings.ItemWidth = EditorGUILayout.FloatField ("Each Item Width", _sSettings.ItemWidth);
			_sSettings.ItemHeight = EditorGUILayout.FloatField ("Each Item Height", _sSettings.ItemHeight);
			 
		}
		//}

		// UI Settings
		EditorGUILayout.LabelField ("");
		EditorGUILayout.LabelField ("Color Settings", headingStyle );
		
		_sSettings.BackgroundColor = EditorGUILayout.ColorField ("Item Background Color", _sSettings.BackgroundColor);
		_sSettings.FocusingColor = EditorGUILayout.ColorField ("Item Focusing Color", _sSettings.FocusingColor);
		_sSettings.SelectingColor = EditorGUILayout.ColorField ("Item Selecting Color", _sSettings.SelectingColor);



		// Item Settings 
		EditorGUILayout.LabelField ("");
		EditorGUILayout.LabelField ("Text Settings", headingStyle );

		_sSettings.TextFont = EditorGUILayout.TextField ("Font Name", _sSettings.TextFont);
		_sSettings.TextColor = EditorGUILayout.ColorField ("Text Color", _sSettings.TextColor);
		_sSettings.TextSize = EditorGUILayout.IntField ("Text Size", _sSettings.TextSize);



		// Interaction Settings 
		EditorGUILayout.LabelField ("");
		EditorGUILayout.LabelField ("Interaction Settings", headingStyle );

		_sSettings.AppearAnimSpeed = EditorGUILayout.FloatField ("Appear Animation Speed", _sSettings.AppearAnimSpeed);
		_sSettings.SelectSpeed = EditorGUILayout.FloatField ("Select Speed", _sSettings.SelectSpeed);
		_sSettings.FocusStart = EditorGUILayout.FloatField ("Focus Start", _sSettings.FocusStart);


		EditorGUILayout.EndVertical ();



		if (GUI.changed)
			EditorUtility.SetDirty (target);
	}
}