using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class TipPointer : MonoBehaviour {

    //포인팅 가능한 레이어를 나타냄.
    public LayerMask pointableLayers = ~0;

    //싱글턴 오브젝트
    protected TipPointer _instance;
    //사용자가 가르킬 수 있는 오브젝트의 최대 거리.
    public float pointingObjectDistance = 1.0f;

    protected HandModel hand_model;
    protected Collider active_object;
    protected Vector3[] leap_fingertip;
    //protected FingerList leap_finger_list;
    protected FingerList leap_right_finger_list;
    protected FingerList leap_left_finger_list;
    //public int num_finger_tip;
    
    //어느 손가락에 붙일 것인지 조사
    //이 방식이 에디터가 더 깨끗해진다.
    public bool[] useRightFinger = new bool[5];
    public bool[] useLeftFinger = new bool[5];
    
    //양손에 붙일 것인지 조사
    //public bool isBothHand;

    //왼손잡이인지 조사(한 손만 사용할 경우)
    //public bool isLeftHand;

    protected HandController handcontroller;
    protected Collider active_object_;
    protected Controller leap_controller;
    protected Frame frame;
    protected Vector3 unityposition;
    protected Vector3 worldPosition;
    protected Leap.Vector position;
    protected Vector3 tip;//그래픽 모델 적용을 위한 변수.

    protected Finger.FingerType[] type = new Finger.FingerType[5];
    //public GameObject pointerPrefab;
    
    //오브젝트 풀 방식에 필요한 변수.
    protected List<GameObject> pointerRightPool = new List<GameObject>();
    protected List<GameObject> pointerLeftPool = new List<GameObject>();

    //사용할 포인터의 모양 프리펩
    public string pointerPrefabName;


    void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start()
    {
        handcontroller = GameObject.FindObjectOfType<HandController>();
        leap_controller = new Controller();

        createValue();
        createPointer();
        setPointer();

    }
    

    //편하게 사용할 데이터 만들기.
    protected void createValue()
    {
        type[0] = Finger.FingerType.TYPE_THUMB;
        type[1] = Finger.FingerType.TYPE_INDEX;
        type[2] = Finger.FingerType.TYPE_MIDDLE;
        type[3] = Finger.FingerType.TYPE_RING;
        type[4] = Finger.FingerType.TYPE_PINKY;
    }

    // 손 객체를 미리 동적할당 하는 함수이다.
    public void createPointer()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject prefab = Resources.Load(pointerPrefabName) as GameObject;
            GameObject tip_pointer = (GameObject)Instantiate(prefab);
            tip_pointer.name = "tip_pointer_right"+i;
            pointerRightPool.Insert(i,tip_pointer);
            tip_pointer.SetActive(false);
            
        }

        for(int i = 0; i<5; i++)
        {
            GameObject prefab = Resources.Load(pointerPrefabName) as GameObject;
            GameObject tip_pointer = (GameObject)Instantiate(prefab);
            tip_pointer.name = "tip_pointer_left" + i;
            pointerLeftPool.Insert(i,tip_pointer);
            tip_pointer.SetActive(false);
        }
    }

    // 활성화를 세팅하는 함수이다. 근데 좀 구려보인다.
    public void setPointer()
    {
        
        int i = 0;
        foreach(GameObject rightFinger in pointerRightPool)
        {
            rightFinger.SetActive(useRightFinger[i]);
            i++;
        }
        i = 0;
        foreach (GameObject leftFinger in pointerLeftPool)
        {
            leftFinger.SetActive(useLeftFinger[i]);
            i++;
        }
       
    }

    
    //FixedUpdate를 통해 얻은 현재의 위치로 가장 가까운 클릭가능한 객체를 찾는 함수
    protected Collider FindClosestPointableObject(Vector3 pointing_position)
    {
        Collider closest = null;
        float closest_sqr_distance = pointingObjectDistance * pointingObjectDistance;

        Collider[] close_things = Physics.OverlapSphere(pointing_position, pointingObjectDistance, pointableLayers);

        for (int j = 0; j < close_things.Length; j++)
        {
            float sqr_distance = (pointing_position - close_things[j].transform.position).sqrMagnitude;

            if (close_things[j].GetComponent<Rigidbody>() != null && sqr_distance < closest_sqr_distance &&
                !close_things[j].transform.IsChildOf(transform) &&
                close_things[j].tag != "Not Pointable")
            {
                PointableObject pointable = close_things[j].GetComponent<PointableObject>();
                if (pointable == null || !pointable.IsPointed())
                {
                    closest = close_things[j];
                    closest_sqr_distance = sqr_distance;
                }
            }
        }

        return closest;
    }

    protected void Hover(Vector3 current_point_position)
    {
        Collider hover = FindClosestPointableObject(current_point_position);

        if (hover != active_object_ && active_object_ != null)
        {
            PointableObject old_pointable = active_object_.GetComponent<PointableObject>();

            if (old_pointable != null)
                old_pointable.OnStopHover();
        }

        if (hover != null)
        {
            PointableObject new_pointable = hover.GetComponent<PointableObject>();

            if (new_pointable != null)
                new_pointable.OnStartHover();
        }

        active_object_ = hover;

    }
    
    //FixedUpdate()에서 호출하는 위치 결정 함수.
    protected void UpdatePointPosition()
    {


        frame = leap_controller.Frame(0);
      
        leap_right_finger_list = frame.Hands.Rightmost.Fingers;//오른손가락
        leap_left_finger_list = frame.Hands.Leftmost.Fingers;//왼손가락

        int i = 0;
        if (frame.Hands.Rightmost.IsRight)
        {
            foreach (GameObject rightFinger in pointerRightPool)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (rightFinger.active == true)// 모든 손가락 중에서 활성화 된 손가락만.
                    {
                        if (type[i] == leap_right_finger_list[j].Type())// 원하는 손가락 타입과 감지된 손가락의 타입이 같을 경우.
                        {
                            position = leap_right_finger_list[j].TipPosition;
                            Finger finger = leap_right_finger_list[j];

                            unityposition = position.ToUnityScaled(false);
                            worldPosition = handcontroller.transform.TransformPoint(unityposition);
                            rightFinger.transform.position = worldPosition;
                        }
                    }
                }
                i++;
            }
            i = 0;
        }

        if (frame.Hands.Leftmost.IsLeft)
        {
            foreach (GameObject leftFinger in pointerLeftPool)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (leftFinger.active == true)
                    {
                        if (type[i] == leap_left_finger_list[j].Type())
                        {
                            position = leap_left_finger_list[j].TipPosition;
                            Finger finger = leap_left_finger_list[j];

                            unityposition = position.ToUnityScaled(false);
                            worldPosition = handcontroller.transform.TransformPoint(unityposition);
                            leftFinger.transform.position = worldPosition;
                        }
                    }
                }
                i++;
            }
        }
           
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /* 손가락 위치 테스트
        print("tip.x " + worldPosition.x);
        print("tip.y " + worldPosition.y);
        print("tip.z " + worldPosition.z);
        */
        UpdatePointPosition();
         

    }
}
