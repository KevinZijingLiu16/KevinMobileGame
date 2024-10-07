using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    private Transform _attchPoint;

    public void Init(Transform attachPoint) //know where the owner is
    {
        _attchPoint = attachPoint;
    }

    public void SetHealthSliderValue(float val)
    {
        healthSlider.value = val;
    }

    private void Update()
    {
        Vector3 attachScreenPoint = Camera.main.WorldToScreenPoint(_attchPoint.position);
        transform.position = attachScreenPoint;


    }



}
