using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
public class CinemachineScript : MonoBehaviour
{
    [SerializeField] GameObject cinemachine;
    
    TakenStackList takenStackList;
    CinemachineVirtualCamera cinemachineVirtualCamera;
    CinemachineTransposer transposer;
    Vector3 startPos;
    float xPos = 30;
    void Start()
    {
        takenStackList = GetComponent<TakenStackList>();
        cinemachineVirtualCamera = cinemachine.GetComponent<CinemachineVirtualCamera>();
        transposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        startPos = transposer.m_FollowOffset;
    }

    public IEnumerator ZoomOutCamera()
    {
        if (takenStackList.getCountOfList()-xPos>10)
        {
            Vector3 target = transposer.m_FollowOffset + Vector3.up * 9 - Vector3.forward * 6;
            float _time = 0f;
            Vector3 startValue = transposer.m_FollowOffset;
            while (_time<1f)
            {
                _time += Time.deltaTime;
                transposer.m_FollowOffset = Vector3.Lerp(startValue, target, _time / 1f);
                yield return new WaitForEndOfFrame();
            }
            xPos += 12;
        }
        

    }
    public void normalCameraSet()
    {
        while (Vector3.Distance(transposer.m_FollowOffset, startPos) > 1)
        {
            transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset,startPos, Time.deltaTime*1/100);
        }
    }
}
