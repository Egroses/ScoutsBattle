using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasScript : MonoBehaviour
{
    
    public void setActiveCanvasTrue()
    {
        transform.gameObject.SetActive(true);
    }
    public void setActiveCanvasFalse()
    {
        transform.gameObject.SetActive(false);
    }
}
