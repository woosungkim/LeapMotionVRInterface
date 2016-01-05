using UnityEngine;
using System.Collections;

namespace Interface.Shortcut
{
	public class ShortcutItem
	{
		private int id { set; get; }
		private string name { set; get; }

		/// <summary>
		/// Constructors
		/// </summary>
		public ShortcutItem()
		{
		}
		public ShortcutItem(int id, string name)
		{
			this.id = id;
			this.name = name;
		}
	}
}