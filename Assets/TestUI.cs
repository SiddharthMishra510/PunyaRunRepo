using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUI : MonoBehaviour
{
    [SerializeField]
    List<Transform> Buildings;
    [SerializeField]
    Camera UICamera;
    [SerializeField]
    GameObject imageToInstantiate;
    [SerializeField]
    public Transform UI_Canvas;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Vector3 screenPoint = UICamera.WorldToScreenPoint(Buildings[0].position);
            // Vector2 result;
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(UI_Canvas.GetChild(0).GetComponent<RectTransform>(), screenPoint, UICamera, out result);
          
            // Vector2 screenPoint = Camera.main.WorldToScreenPoint(Buildings[0].position);
            print("Possssssss :" + screenPoint);
            
            GameObject obj = Instantiate(imageToInstantiate, screenPoint, Quaternion.identity, UI_Canvas);
            RectTransform trans = obj.GetComponent<RectTransform>();
             trans.position = Buildings[0].position;
        }
    }
}
