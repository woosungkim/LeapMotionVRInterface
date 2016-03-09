using UnityEngine;
using System.Collections;

public class SelectableObject : MonoBehaviour {

	public float _SelectDistance = 1.0f;

	public EventScript _SelectEvent;

	public ActionExecType _ActionExecuteType = ActionExecType.Once;
	/****************************************/

	private int _id;

	private bool isSelected = false;

	// Use this for initialization
	void Start () {
		_id = SelectableObjectUtil.AutoItemId;

	}
	
	// Update is called once per frame
	void Update () {

		ObjectInteractionManager.SetObjectPos (_id, gameObject.transform.position);

		float dis = ObjectInteractionManager.findNearestPointerDistance (_id);

		if (dis < _SelectDistance) {
			//print ("select!!");
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
		} else {
			isSelected = false;
		}
	}
}
