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
        playerSprite.GetComponent<SpriteRenderer>().sprite = detective;
    }
    public void ChangeToWaiter()
    {
        playerSprite.GetComponent<SpriteRenderer>().sprite = waiter;
    }
    public void ChangeToSecurity()
    {
        playerSprite.GetComponent<SpriteRenderer>().sprite = security;
    }
    public void ChangeToLady()
    {
        playerSprite.GetComponent<SpriteRenderer>().sprite = lady;
    }
}