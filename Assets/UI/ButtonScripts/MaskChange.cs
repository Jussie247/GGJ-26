using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskChange : MonoBehaviour
{
    public GameObject playerSprite;
    public Sprite detective;
    public Sprite waiter;
    public Sprite security;
    public Sprite lady;
 
    public void ChangeToDetective()
    {
        if (DialogManager.GetInstance() != null &&
         DialogManager.GetInstance().dialogIsPlaying)
            return;

        playerSprite.GetComponent<SpriteRenderer>().sprite = detective;
    }
    public void ChangeToWaiter()
    {
            if (DialogManager.GetInstance() != null &&
            DialogManager.GetInstance().dialogIsPlaying)
                return;

        playerSprite.GetComponent<SpriteRenderer>().sprite = waiter;
    }
    public void ChangeToSecurity()
    {
            if (DialogManager.GetInstance() != null &&
            DialogManager.GetInstance().dialogIsPlaying)
                return; 

        playerSprite.GetComponent<SpriteRenderer>().sprite = security;
    }
    public void ChangeToLady()
    {
            if (DialogManager.GetInstance() != null &&
            DialogManager.GetInstance().dialogIsPlaying)
                return;
                
        playerSprite.GetComponent<SpriteRenderer>().sprite = lady;
    }
}