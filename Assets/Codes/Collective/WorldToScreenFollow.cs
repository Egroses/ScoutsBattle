using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldToScreenFollow : MonoBehaviour
{
    public float offset;
    Transform cam;
    RectTransform rectTransform;
    public bool follow;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        cam = Camera.main.transform;

    }

    public Vector3 pos_offset;
    public Transform target;

    void Update()
    {
        //if (Time.frameCount % 15 == 0)
        //{
        transform.rotation = Quaternion.LookRotation(Vector3.forward) * Quaternion.AngleAxis(offset, Vector3.right);
        if (follow)
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(target.position + pos_offset);
            rectTransform.anchoredPosition = (pos);
        }
        //}
    }
}