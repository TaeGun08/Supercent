using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ContentOpener : OnTriggerInteraction
{
    [Header("ContentOpener Settings")]
    [SerializeField] private GameObject[] openContents;
    [Space] 
    [SerializeField] private bool activeFalseOn;
    [SerializeField] private GameObject[] activeFalseObjects;
    [Space] 
    [SerializeField] private int moneyCost;
    private int currentMoneyCost;
    [SerializeField] private TMP_Text moneyText;
    
    private bool inside;
    
    [SerializeField] private Tutorial tutorial;
    [SerializeField] private Tutorial nextTutorial;
    
    public Tutorial Tutorial => tutorial;
    public Tutorial NextTutorial => nextTutorial;

    private void Awake()
    {
        currentMoneyCost = moneyCost;
        UpdateText();
    }

    protected override void TriggerEnter(Collider other)
    {
        inside = true;
        StartCoroutine(OpenContentCoroutine(other));
    }

    private IEnumerator OpenContentCoroutine(Collider other)
    {
        WaitForSeconds wfs = new WaitForSeconds(0.05f);
        
        Player player = other.GetComponent<Player>();
        
        while (inside && currentMoneyCost > 0)
        {
            yield return wfs;
            
            if(player.GetHasMoney() <= 0) continue;
            Money money = player.Pay();
            money.SetCurveMovement(transform, transform.position.y, 0f, 5f, transform);
            currentMoneyCost--;
            UpdateText();
        }

        if (currentMoneyCost > 0) yield break;
        AudioManager.Instance.ContentsOpenSound();
        TutorialManager.Instance.SetPoints(Tutorial, NextTutorial);
        OpenContents();
        ActivateFalseObjects();
    }

    private void OpenContents()
    {
        foreach (var openContent in openContents)
        {
            openContent.SetActive(true);
        }
    }

    private void ActivateFalseObjects()
    {
        if (!activeFalseOn) return;
        
        foreach (var obj in activeFalseObjects)
        {
            obj.gameObject.SetActive(false);
        }
    }

    private void UpdateText()
    {
        moneyText.text = currentMoneyCost.ToString();
    }
    
    protected override void TriggerExit(Collider other)
    {
        inside = false;
    }
}
