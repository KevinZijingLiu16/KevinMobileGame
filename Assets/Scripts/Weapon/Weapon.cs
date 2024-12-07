using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] string AttachSlotTag;
    [SerializeField] float AttackRateMultiplier = 1f;
    [SerializeField] AnimatorOverrideController overrideController;

    [SerializeField] AudioClip fireAudio;
    AudioSource fireAudioSource;
    [SerializeField] float fireVolume = 1.0f;



    private void Awake()
    {
        fireAudioSource = GetComponent<AudioSource>();
    }
    public abstract void Attack();

    public void PlayFireAudio()
    {
        fireAudioSource.PlayOneShot(fireAudio, fireVolume);
    }

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
        Owner.GetComponent<Animator>().SetFloat("AttackRateMultiplier", AttackRateMultiplier);
    }
    public void Unequip()
    {
        gameObject.SetActive(false);
    }

    public void DamageGameObject(GameObject objToDamage, float amt)
    {
        HealthComponent healthComp = objToDamage.GetComponent<HealthComponent>();
        if (healthComp != null)
        {
            healthComp.ChangeHealth(-amt, Owner);
        }
    }
}
