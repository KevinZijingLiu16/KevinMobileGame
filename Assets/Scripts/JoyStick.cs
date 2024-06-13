using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;




public class JoyStick : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField]  RectTransform ThumbStickTrans;
    [SerializeField]  RectTransform BackGroundTrans;
    [SerializeField] RectTransform CenterTrans;

    public delegate void OnStickInputVlaueUpdated(Vector2 inputVal);

    public event OnStickInputVlaueUpdated OnStickValueUpdated;

    public void OnDrag(PointerEventData eventData)
    {

       // Debug.Log($"OnDrag Fire {eventData.position}");
        Vector2 TouchPostion = eventData.position;
        Vector2 centerPos = BackGroundTrans.position;

        Vector2 LocalOffset = Vector2.ClampMagnitude (TouchPostion - centerPos, BackGroundTrans.sizeDelta.x/2);
        Vector2 inputVal = LocalOffset / (BackGroundTrans.sizeDelta.x / 2);
        ThumbStickTrans.position = LocalOffset + centerPos;

        OnStickValueUpdated?.Invoke(inputVal);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log($"OnPointerDown Fire ");
        BackGroundTrans.position = eventData.position;
        ThumbStickTrans.position = eventData.position;

        OnStickValueUpdated?.Invoke(Vector2.zero);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
      // Debug.Log($"OnPointerUp Fire ");
      BackGroundTrans.position = CenterTrans.position;
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
