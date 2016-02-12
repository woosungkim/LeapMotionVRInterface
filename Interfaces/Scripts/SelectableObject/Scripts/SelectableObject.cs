using UnityEngine;
using System.Collections;

public class SelectableObject : MonoBehaviour {

	public float _SelectDistance = 1.0f;

	private int _id;
	private Color originalColor;

	// Use this for initialization
	void Start () {
		_id = SelectableObjectUtil.AutoItemId;

		originalColor = gameObject.GetComponent<Renderer> ().material.color;
		print (gameObject.GetComponent<Renderer> ());
	}
	
	// Update is called once per frame
	void Update () {

		ObjectInteractionManager.SetObjectPos (_id, gameObject.transform.position);

		float dis = ObjectInteractionManager.findNearestPointerDistance (_id);

		if (dis < _SelectDistance) {
			print ("select!!");
			gameObject.GetComponent<Renderer> ().material.color = new Color(0.5f, 0.0f, 0.0f, 0.2f);
		} else {
			gameObject.GetComponent<Renderer> ().material.color = originalColor;
		}
	}
}
