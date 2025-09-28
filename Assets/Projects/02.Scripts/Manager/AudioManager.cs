using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonBehaviour<AudioManager>
{
    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [Space]
    [SerializeField] private AudioClip breadPickUpSound;
    [SerializeField] private AudioClip breadDropSound;
    [SerializeField] private AudioClip moneySound;
    [SerializeField] private AudioClip checkOutSound;
    [SerializeField] private AudioClip contentsOpenSound;
    [SerializeField] private AudioClip trashSound;
    
    public void BreadPickUpSound()
    {
        audioSource.PlayOneShot(breadPickUpSound);
    }

    public void BreadPutDownSound()
    {
        audioSource.PlayOneShot(breadDropSound);
    }
    
    public void MoneySound()
    {
        audioSource.PlayOneShot(moneySound);
    }

    public void CheckOutSound()
    {
        audioSource.PlayOneShot(checkOutSound);
    }

    public void ContentsOpenSound()
    {
        audioSource.PlayOneShot(contentsOpenSound);
    }

    public void TrashSound()
    {
        audioSource.PlayOneShot(trashSound);
    }
}
