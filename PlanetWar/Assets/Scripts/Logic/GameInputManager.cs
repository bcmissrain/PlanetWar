using UnityEngine;
using System.Collections;

/// <summary>
/// 处理游戏中星球之间的触控
/// </summary>
public class GameInputManager : MonoBehaviour {
    public static int starFromIndex = -1;                   //出发星球
    public static int starToIndex = -1;                     //目的星球
    public static int starInvalid = -1;
    public static Vector3 touchWorldPos = Vector3.zero;     //当前点击的位置

	void Start () {
	    
	}
	
	void Update () {
        starInvalid = -1;
        //滑动
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var hitStar = hit.collider.gameObject;
                int hitStarIndex = StarPoolManager.instance.GetIndexByGameObj(hitStar);
                touchWorldPos = hit.point;
                touchWorldPos.z = 0;

                //点到了行星
                if (hitStarIndex != -1)
                {
                    var masterObj = hitStar.GetComponent<StarElement>().GetMasterElement();

                    //如果没有设置开始行星 => 设置 starFromIndex
                    if (starFromIndex == -1)
                    {
                        if (masterObj.m_ControllerType == ControllerType.Human)
                        {
                            starFromIndex = hitStarIndex;
                        }
                        else
                        {
                            starInvalid = hitStarIndex;
                        }
                    }
                    else
                    {
                        //如果设置了开始行星
                        //如果点击的不是开始行星
                        if (hitStarIndex != starFromIndex)
                        {
                            starToIndex = hitStarIndex;
                        }
                    }
                }
                else
                {
                    if (starFromIndex != -1)
                    {
                        starToIndex = -1;
                    }
                }
            }
            //没点到行星
            //else
            //{
            //    if (starFromIndex != -1)
            //    {
            //        starToIndex = -1;
            //    }
            //}
        }

        //松手
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (starFromIndex != -1 && starToIndex != -1)
                {
                    EventData eventData = new EventData();
                    eventData.intData1 = starFromIndex;
                    eventData.intData2 = starToIndex;
                    //发送消息
                    GameEventDispatcher.instance.InvokeEvent(EventNameList.GAME_INPUT_RELEASE_EVENT, eventData);
                    //停止绘制
                }
            }

            //重置
            starFromIndex = -1;
            starToIndex = -1;
        }
	}
}
