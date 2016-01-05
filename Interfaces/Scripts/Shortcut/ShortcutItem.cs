using UnityEngine;
using System.Collections;

namespace Interface.Shortcut
{
	public class ShortcutItem
	{
		private string name { set; get; }

	
		public ShortcutItem()
		{
		}

		public ShortcutItem(string n)
		{
			this.name = n;
		}

		~ShortcutItem()
		{
		}

	}
}