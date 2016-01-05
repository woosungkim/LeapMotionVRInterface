using UnityEngine;
using System.Text;
using System.Collections;

namespace Interface.Shortcut
{
	public class ShortcutController
	{
		private static int MAX_ITEMS = 20;
		private ShortcutItem[] items;
	
		private int nItems; // number of shortcut items

		/// <summary>
		/// Constructor
		/// </summary>
		public ShortcutController() {
			items = new ShortcutItem[MAX_ITEMS];
			nItems = 0;
		}

		/// <summary>
		/// Add item to this shortcut
		/// </summary>
		public void addItem(string n)
		{
			if (nItems < MAX_ITEMS) {
				items[nItems] = new ShortcutItem();// param
				nItems++;
			}
		}




	}
}