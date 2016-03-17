using UnityEngine;
using System.Collections;

public class SelectableObject : MonoBehaviour {

	public float _SelectDistance = 1.0f;

	public EventScript _SelectEvent;

	public ActionExecType _ActionExecuteType = ActionExecType.Once;
	/****************************************/

	public float _DistanceFromObject = 1.0f;

	public float _RotationSpeed = 1.0f;

	public Vector3 _ArrowScale = new Vector3 (3.0f, 3.0f, 3.0f);

	public Color _NormalColor = Color.green;
	public Color _FocusColor = Color.yellow;
	public Color _selectColor = Color.red;

	/****************************************/
	/****************************************/

	private int _id;

	private bool isSelected = false;

	private GameObject arrowInst;

	// Use this for initialization
	void Start () {
		_id = SelectableObjectUtil.AutoItemId;

		GameObject arrowPrefab = Resources.Load ("Prefabs/Arrow") as GameObject;

		arrowInst = MonoBehaviour.Instantiate (arrowPrefab) as GameObject;

		arrowInst.name = "arrow";
		arrowInst.transform.SetParent (gameObject.transform, false);
		arrowInst.transform.localPosition = new Vector3 (0.0f, _DistanceFromObject, 0.0f);
		arrowInst.transform.eulerAngles = new Vector3 (180.0f, 0.0f, 0.0f);
		arrowInst.transform.localScale = _ArrowScale;
	}

	// Update is called once per frame
	void Update () {
		arrowInst.transform.Rotate (0, _RotationSpeed, 0);

		ObjectInteractionManager.SetObjectPos (_id, gameObject.transform.position);

		float dis = ObjectInteractionManager.findNearestPointerDistance (_id);
		if (dis < _SelectDistance) {
			print ("select!!");
			arrowInst.GetComponent<Renderer> ().material.color = _selectColor;
			if (_SelectEvent != null) {
				if (_ActionExecuteType == ActionExecType.DuringSelecting) {
					_SelectEvent.ClickAction ();
				} else if (_ActionExecuteType == ActionExecType.Once) {
					if (!isSelected) {
						isSelected = true;
						_SelectEvent.ClickAction ();
					}
				}
			}
		} else if (dis < (_SelectDistance * 1.5f)) {
			print ("focus~~~");
			arrowInst.GetComponent<Renderer> ().material.color = _FocusColor;
		}
		else {
			isSelected = false;
			arrowInst.GetComponent<Renderer> ().material.color = _NormalColor;
		}
	}
}
