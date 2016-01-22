﻿using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ShortcutItem))]
public class ShortcutItemEditor : Editor {

	ShortcutItem _sItem;

	void OnEnable() {
		_sItem = target as ShortcutItem;
	}

	public override void OnInspectorGUI() {

		EditorGUILayout.BeginVertical ();

		_sItem._Label = EditorGUILayout.TextField ("Label", _sItem._Label);

		_sItem._ItemType = (ItemType)EditorGUILayout.EnumPopup ("Item Type", _sItem._ItemType);

		if (_sItem._ItemType == ItemType.NormalButton) {
			_sItem._Action = (EventScript)EditorGUILayout.ObjectField("Action Script", _sItem._Action, typeof(EventScript), true);
		}


		EditorGUILayout.EndVertical ();

		if (GUI.changed) {
			EditorUtility.SetDirty (target);
		}

	}


}