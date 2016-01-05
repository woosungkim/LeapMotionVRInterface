using UnityEngine;
using System.Collections;

namespace Interface.Shortcut
{
	public class ShortcutView 
	{
		private GameObject[] itemInstance;

		/// <summary>
		/// Bind shortcut items to shortcut view
		/// </summary>
		public void bindItems(ShortcutItem[] items)
		{
			itemInstance = new GameObject[items.Length];

			for (int i = 0; i < items.Length; i++) {
				// 각 아이템의 데이터를 itemInstance에 bind한다.
				

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