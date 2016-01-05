using UnityEngine;
using System.Collections;

namespace Interface.Shortcut
{
	public class ShortcutController
	{
		public static int MAX_ITEMS = 10; // max number of shortcut

		private ShortcutView scView;
		private ShortcutItem[] scItems;
		
		private int nItems; // number of shortcut items

		/// <summary>
		/// Constructors
		/// </summary>
		public ShortcutController() 
		{
		}
		public ShortcutController(int size)
		{
			scItems = new ShortcutItem[size];
			nItems = size;

		
		}

		/// <summary>
		/// Add shortcut item to shortcut
		/// </summary>
		public void addItem(int id, string name)
		{
			if (nItems < MAX_ITEMS) {
				scItems[id] = new ShortcutItem(id, name);
			}
		}

		/// <summary>
		/// Bind shortcut items to shortcut view 
		/// </summary>
		public void bindItems()
		{
			scView.bindItems (scItems);
		}

		/// <summary>
		/// Draw shortcut interface
		/// </summary>
		public void drawShortcut()
		{

			scView.drawShortcut ();
		}

	}
}