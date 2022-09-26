using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour,IPointerEnterHandler,IDeselectHandler,IPointerDownHandler
{
    public void OnDeselect(BaseEventData eventData)
    {

        GetComponent<Selectable>().OnPointerExit(null);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.selectedObject.GetComponent<Button>()!=null)
        {
            GetComponent<Button>().onClick.Invoke();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Selectable>().Select();
    }
}
