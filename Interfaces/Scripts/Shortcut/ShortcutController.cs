using UnityEngine;
using System.Collections;

namespace Interface.Shortcut
{
	public class ShortcutController
	{
		public static int MAX_ITEMS = 10; // max number of shortcut

		private string resName;

		private ShortcutView scView;
		private ShortcutItem[] scItems;
		
		private int nItems; // number of shortcut items

		/// <summary>
		/// Constructors
		/// </summary>
		public ShortcutController() 
		{
		}
		public ShortcutController(string resName, int size)
		{
			this.resName = resName;

			scItems = new ShortcutItem[size];
			nItems = size;
		
		}

		/// <summary>
		/// Add shortcut item to shortcut
		/// </summary>
		public void addItem(int id, SelectActionBase action)
		{
			if (nItems < MAX_ITEMS) {
				scItems[id] = new ShortcutItem(id, action);
			}
		}

		/// <summary>
		/// Bind shortcut items to shortcut view 
		/// </summary>
		public void bindItems()
		{
			scView = new ShortcutView ();
			scView.bindItems (resName, scItems);
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