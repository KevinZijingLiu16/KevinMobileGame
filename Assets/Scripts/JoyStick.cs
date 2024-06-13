using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class JoyStick : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField]  RectTransform ThumbStickTrans;
    [SerializeField]  RectTransform BackGroundTrans;
    public void OnDrag(PointerEventData eventData)
    {

       // Debug.Log($"OnDrag Fire {eventData.position}");
        Vector2 TouchPostion = eventData.position;
        Vector2 centerPos = BackGroundTrans.position;

        Vector2 LocalOffset = Vector2.ClampMagnitude (TouchPostion - centerPos, BackGroundTrans.sizeDelta.x/2);
        ThumbStickTrans.position = LocalOffset + centerPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"OnPointerDown Fire ");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      // Debug.Log($"OnPointerUp Fire ");

        ThumbStickTrans.position = BackGroundTrans.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
