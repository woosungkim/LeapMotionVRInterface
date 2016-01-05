using UnityEngine;
using System.Collections;

namespace Interface.Shortcut
{
	public class ShortcutItem
	{
		public int id { set; get; }
		public string name { set; get; }
	
		public SelectActionBase action { set; get; }


		/// <summary>
		/// Constructors
		/// </summary>
		public ShortcutItem()
		{
		}
		public ShortcutItem(int id, SelectActionBase action)
		{
			this.id = id;
			this.action = action;
		}

	}
}