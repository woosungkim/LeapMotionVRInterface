using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Leap;

namespace ShorcutMVC.Untitled
{
	public class SCController
	{
		private SCView scv;
		private SCItem[] sci;
		private SCController scController;
   
         
		public SCController()
		{
            sci = new SCItem[201];
        }

		~SCController()
		{}

		public void addItem(int itemId, int groupId, String path)
		{
			if(SCItem.itemNum < SCItem.maxItemNum)
            {
                sci[SCItem.itemNum] = new SCItem(itemId, groupId, path);
            
            }else{
                Console.WriteLine("Can't add the Item!");
            }
		}

        public void createView(int mode, bool IsLeftSide, bool IsVertical)
        {
            scv = new SCView(sci, IsLeftSide, mode, IsVertical);
        }

        public void setViewItem()
        {
            
            scv.AllocateItem();
        }

		public void setItemSize(float itemSize)
		{
			scv.setItemSize(itemSize);
		}
        
        public float getItemSize()
        {
            return scv.getItemSize();
        }

		public void setTextSize(int textSize)
		{
            scv.setTextSize(textSize);
		}

		public void setTextColor(Color color)
		{
            scv.setTextColor(color);
		}

        public void setPosition(float x, float y)
        {
            scv.setPosition(x, y);
        }

        public Vector3 getPosition()
        {
            return scv.getPosition() ;
        }

        public void onDraw(HandController controller, GameObject camera)
		{
			scv.onDraw(controller, camera);
		}

        
	}
}
