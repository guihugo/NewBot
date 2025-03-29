using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ContainerInterface 
{
    public void OnDrop(PointerEventData eventData);
}
