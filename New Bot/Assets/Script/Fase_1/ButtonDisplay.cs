using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisplay : MonoBehaviour
{
    public ButtonAttr button;

    public Image artworkImage;

    void Start()
    {
        artworkImage.sprite = button.Image;

        
    }

}
