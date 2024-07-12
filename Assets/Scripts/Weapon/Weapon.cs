using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] string AttachSlotTag;
    [SerializeField] AnimatorOverrideController overrideController;

    public string GetAttachSlotTag()
    {
        return AttachSlotTag;
    }

   public GameObject Owner { get; private set; }
    public void Init(GameObject owner)
    {
        Owner = owner;
        Unequip();
    }
    public void Equip()
    {
        gameObject.SetActive(true);
        Owner.GetComponent<Animator>().runtimeAnimatorController = overrideController;
    }
    public void Unequip()
    {
        gameObject.SetActive(false);
    }
}
