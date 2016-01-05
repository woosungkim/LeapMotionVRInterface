using UnityEngine;
using System.Collections;

namespace Interface.Shortcut
{
	public class ShortcutView : MonoBehaviour
	{
		private GameObject[] itemInstance;

		/// <summary>
		/// Bind shortcut items to shortcut view & Instantiate
		/// </summary>
		public void bindItems(string resName, ShortcutItem[] items)
		{
			itemInstance = new GameObject[items.Length];

			for (int i = 0; i < items.Length; i++) {
				// 각 아이템의 데이터를 itemInstance에 bind한다.
				itemInstance[i] = Resources.Load ("ShortcutItem/" + resName) as GameObject;

				//Debug.Log ("name : " + items[i].action.name); 

			}

			Vector3 pos = new Vector3 (0.0f, 0.0f, 0.0f);
			Quaternion angle = new Quaternion (0.0f, 0.0f, 0.0f, 0.0f);
			
			for (int i = 0; i < itemInstance.Length; i++) {
				
				itemInstance[i] = (GameObject)Instantiate(itemInstance[i], pos, angle);

				// 생성된 각 리소스 객체 인스턴스를 버튼 액션 인스턴스 밑으로 붙인다.
				itemInstance[i].transform.parent = items[i].action.transform;
			}



		}
		    
		/// <summary>
		/// Draw shortcut interface
		/// </summary>
		public void drawShortcut()
		{
			
		}
	}

}