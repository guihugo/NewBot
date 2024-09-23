using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisplay : MonoBehaviour
{
    public ButtonAttr button;

    public Image artworkImage;

    public int direction;

    void Start()
    {
        artworkImage.sprite = button.Image;

        

        
    }

}
