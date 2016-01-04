using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Leap;

namespace ShorcutMVC.Untitled 
{
    public class SCView 
	{
        
		private SCController scController = null;
        public GameObject trakedCamera;
        public HandController handcontroller;
		public bool IsLeftSide;
		private SCItem[] scItem;
		private float itemSize;
		private Color textColor;
		private int textSize;
        private Hand hand;
		private Frame frame;
		private Finger finger;
		private FingerList fingerlist;
		private Vector3 _initPos;
		private Vector3 _nowPos;
		public Controller controller;
		private Arm arm;
        private int mode;
        private GameObject go;
        private Vector3 userPos;
        private bool IsVertical;

        public SCView() { }

		public SCView(SCItem[] scItem, bool IsLeftSide, int mode, bool IsVertical)
		{
            this.scItem = scItem;
            this.mode = mode;
            this.IsLeftSide = IsLeftSide;
            this.IsVertical = IsVertical;

            controller = new Controller();
            _initPos = Vector3.zero;
            //GameObject go = GameObject.Find("ShorCut");

            
            
		}

		~SCView()
		{}

        public float getItemSize()
        {
            return this.itemSize;
        }

        public void setItemSize(float value)
        {
            if(value!=0)
            {
                this.itemSize = value;
            }
            else
            {
                this.itemSize = 1;
            }
        }

        public Color getTextColor()
        {
            return this.textColor;
        }

        public void setTextColor(Color value)
        {
            this.textColor = value;
        }

        public int getTextSize()
        {
            return this.textSize;
        }
        
        public void setTextSize(int value)
        {
            this.textSize = value;
        }

        public void setPosition(float x, float y)
        {
            userPos = new Vector3(x, y, 0);
        }

        public Vector3 getPosition()
        {
            return this.userPos;
        }

		public void AllocateItem()
		{
            for(int i = 0; i<SCItem.itemNum; i++)
            {
                GameObject prefab = Resources.Load(scItem[i].getGameObjectName()) as GameObject;
                GameObject itemtemp = MonoBehaviour.Instantiate(prefab) as GameObject;
                
                itemtemp.name = scItem[i].getItemName();
                itemtemp.transform.parent = GameObject.Find("ShortCut").GetComponent<mainScript>().itemGroup.transform;
                itemtemp.transform.localScale = new Vector3(this.itemSize, this.itemSize, this.itemSize);
            }
		}

        public void onDraw(HandController handcontroller, GameObject trackedCamera)
        {
            this.trakedCamera = trackedCamera;
            this.handcontroller = handcontroller;

            if(mode == 0)//카메라 모드
            {
                if(IsVertical)
                {
                    for (int i = 0; i < SCItem.itemNum; i++)
                    {
                        if (i == 0)
                        {
                            String temp = "item" + i;
                            GameObject.Find(temp).transform.position = _initPos + this.trakedCamera.transform.position + new Vector3(0, 0, 5f);
                        }
                        else
                        {
                            String temp2 = "item" + i;
                            String temp1 = "item" + (i - 1);
                            GameObject.Find(temp2).transform.position = GameObject.Find(temp1).transform.position + new Vector3(0, GameObject.Find(temp1).transform.localScale.y, 0);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < SCItem.itemNum; i++)
                    {
                        if (i == 0)
                        {
                            String temp = "item" + i;
                            GameObject.Find(temp).transform.position = _initPos + this.trakedCamera.transform.position + new Vector3(-3f, 0, 5f);
                        }
                        else
                        {
                            String temp2 = "item" + i;
                            String temp1 = "item" + (i - 1);
                            GameObject.Find(temp2).transform.position = GameObject.Find(temp1).transform.position + new Vector3(GameObject.Find(temp1).transform.localScale.x, 0, 0);
                        }
                    }
                }
                
            }
            else if(mode == 1)// 손목 모드
            {
                frame = controller.Frame(0);
                hand = frame.Hands.Frontmost;

                if(hand.IsValid)
                {
                    if (IsLeftSide)//왼손일 때
                    {
                        if(hand.IsLeft)
                        {
                            arm = hand.Arm;
                            Vector wrist = arm.WristPosition;

                            Vector3 unityPosition = UnityVectorExtension.ToUnityScaled(wrist, false);

                            Vector3 worldPosition = handcontroller.transform.TransformPoint(unityPosition);
                            if(IsVertical)
                            {
                                for (int i = 0; i < SCItem.itemNum; i++)
                                {
                                    if (i == 0)
                                    {
                                        String temp = "item" + i;
                                        GameObject.Find(temp).transform.position = worldPosition + new Vector3(3f, 0, 0);
                                    }
                                    else
                                    {
                                        String temp2 = "item" + i;
                                        String temp1 = "item" + (i - 1);
                                        GameObject.Find(temp2).transform.position = GameObject.Find(temp1).transform.position + new Vector3(0, -GameObject.Find(temp1).transform.localScale.y, 0);
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < SCItem.itemNum; i++)
                                {
                                    if (i == 0)
                                    {
                                        String temp = "item" + i;
                                        GameObject.Find(temp).transform.position = worldPosition + new Vector3(3f, 0, 0);
                                    }
                                    else
                                    {
                                        String temp2 = "item" + i;
                                        String temp1 = "item" + (i - 1);
                                        GameObject.Find(temp2).transform.position = GameObject.Find(temp1).transform.position + new Vector3(GameObject.Find(temp1).transform.localScale.x, 0, 0);
                                    }
                                }
                            }
                            
                        }
                    }
                    else//오른손일 때
                    {
                        if (hand.IsRight)
                        {
                            arm = hand.Arm;
                            Vector wrist = arm.WristPosition;

                            Vector3 unityPosition = UnityVectorExtension.ToUnityScaled(wrist, false);

                            Vector3 worldPosition = handcontroller.transform.TransformPoint(unityPosition);

                            if(IsVertical)
                            {
                                for (int i = 0; i < SCItem.itemNum; i++)
                                {
                                    if (i == 0)
                                    {
                                        String temp = "item" + i;
                                        GameObject.Find(temp).transform.position = worldPosition + new Vector3(-3f, 0, 0);
                                    }
                                    else
                                    {
                                        String temp2 = "item" + i;
                                        String temp1 = "item" + (i - 1);
                                        GameObject.Find(temp2).transform.position = GameObject.Find(temp1).transform.position + new Vector3(0, -GameObject.Find(temp1).transform.localScale.y, 0);
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < SCItem.itemNum; i++)
                                {
                                    if (i == 0)
                                    {
                                        String temp = "item" + i;
                                        GameObject.Find(temp).transform.position = worldPosition + new Vector3(-3f, 0, 0);
                                    }
                                    else
                                    {
                                        String temp2 = "item" + i;
                                        String temp1 = "item" + (i - 1);
                                        GameObject.Find(temp2).transform.position = GameObject.Find(temp1).transform.position + new Vector3(-GameObject.Find(temp1).transform.localScale.x, 0, 0);
                                    }
                                }
                            }
                            
                        }
                    }

                }
                
            }
            else if(mode == 2)// 임의 위치 모드, z축의 입력은 무의미하다고 판단해서 넣지 않았다.
            {
                if(IsVertical)
                {
                    for (int i = 0; i < SCItem.itemNum; i++)
                    {
                        if (i == 0)
                        {
                            String temp = "item" + i;
                            GameObject.Find(temp).transform.position = _initPos + this.trakedCamera.transform.position + new Vector3(-3f, 0, 5f) + userPos;
                        }
                        else
                        {
                            String temp2 = "item" + i;
                            String temp1 = "item" + (i - 1);

                            GameObject.Find(temp2).transform.position = GameObject.Find(temp1).transform.position + new Vector3(0, -GameObject.Find(temp1).transform.localScale.y, 0);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < SCItem.itemNum; i++)
                    {
                        if (i == 0)
                        {
                            String temp = "item" + i;
                            GameObject.Find(temp).transform.position = _initPos + this.trakedCamera.transform.position + new Vector3(-3f, 0, 5f) + userPos;
                        }
                        else
                        {
                            String temp2 = "item" + i;
                            String temp1 = "item" + (i - 1);

                            GameObject.Find(temp2).transform.position = GameObject.Find(temp1).transform.position + new Vector3(GameObject.Find(temp1).transform.localScale.x, 0, 0);
                        }
                    }
                }
                
            }
        }

	}
}
