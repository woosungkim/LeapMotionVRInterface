using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Leap;

namespace ShorcutMVC.Untitled
{
	public class SCItem
	{
		private int itemId;
		public static int maxItemNum = 200;
        public static int itemNum = 0;
	    private int groupId;
        private String itemName;
		private String gameObjectName;
        
		public SCItem()
		{}

        public SCItem(int itemid, int groupId, String gameObjectName)
        {
            if(itemNum<maxItemNum)
            {
                this.groupId = groupId;
                this.itemId = itemid;
                this.gameObjectName = gameObjectName;
                this.itemName = "item" + this.itemId;
                itemNum++;
            }
        }

		~SCItem()
		{}
        
        public String getItemName()
        {
            return this.itemName;
        }

        public void setItemName(String value)
        {
            this.itemName = value;
        }

        public String getGameObjectName()
        {
            return this.gameObjectName;
        }

        public void setGameObjectName(String value)
        {
            this.gameObjectName = value;
        }

        public int getItemId()
        {
            return this.itemId;
        }

        public void setItemId(int value)
        {
            this.itemId = value;
        }

        public int getGroupId()
        {
            return this.groupId;
        }

        public void setGroupId(int value)
        {
            this.groupId = value;
        }
	}
}
