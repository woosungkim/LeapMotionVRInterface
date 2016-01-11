using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemHierarchy : MonoBehaviour {
	
	Transform _camera;
	private float _xPos, _yPos, _zPos;

	internal void Build(ShortcutSetting setting, Transform camera) {
		_xPos = setting.X;
		_yPos = setting.Y;
		_zPos = setting.Z;
		_camera = camera;


		ItemLayer layer = Getter.GetChildLayerFromGameObject (gameObject);

		layer.Build (setting, setting.ShortcutName);


		Vector3 pos = new Vector3 (_xPos, _yPos, _zPos);
		gameObject.transform.localPosition = Camera.main.ViewportToWorldPoint (pos);
		gameObject.transform.rotation = Quaternion.LookRotation(_camera.transform.up);



	}

	void Update()
	{


	}






}
