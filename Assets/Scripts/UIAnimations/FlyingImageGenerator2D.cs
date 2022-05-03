using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum FlyingImageGeneratorType { Coin, Punya, Multiplier };

public class FlyingImageGenerator2D : MonoBehaviour
{
    public bool spawnNewSprite = false;

    [SerializeField]
    private FlyingImageGeneratorType myType;
    [Tooltip("Required only if spawnNewSprite is true")]
    [SerializeField]
    private Sprite newSprite;
    [Tooltip("Fly duration")]
    [SerializeField]
    private float duration = 0.5f;

    private const float INITIAL_SCALE_FACTOR = 1.5f;

    // flyTowards: The icon in UI towards which the item should fly
    // canvasTransform: The UI canvas the flyingImagePrefab should be added to
    public void Activate(Transform originalTransform, Transform flyTowards, Transform canvasTransform, Transform startControl, Transform endControl, GameObject trailGO)
    {
       // Vector3 myPositionOnScreen = GameObject.FindGameObjectWithTag("UI_Camera").GetComponent<Camera>().WorldToViewportPoint(originalTransform.position);
        // get the screen position
       // Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(GameObject.FindGameObjectWithTag("UI_Camera").GetComponent<Camera>(), originalTransform.position);
      //  print("screenPoint: " + screenPoint);
        // convert the screen position to the local anchored position
       // Vector2 anchoredPosition = transform.InverseTransformPoint(screenPoint);
        Vector2 anchoredPosition = transform.InverseTransformPoint(Vector3.zero);

        print("anchoredPosition: " + anchoredPosition);
        //GameObject MyImageGO = SpriteSpawning(screenPoint, canvasTransform);
        GameObject MyImageGO = SpriteSpawning(Vector3.zero, canvasTransform);

        TrailEffect(trailGO, MyImageGO);
        Movement(flyTowards, startControl, endControl, Vector3.zero, MyImageGO);
    }

    private static void TrailEffect(GameObject trailGO, GameObject MyImageGO)
    {
        Instantiate(trailGO, MyImageGO.transform.position, MyImageGO.transform.rotation, MyImageGO.transform); // Trail Instantiation
    }

    private void Movement(Transform flyTowards, Transform startControl, Transform endControl, Vector3 myPositionOnScreen, GameObject MyImageGO)
    {
        // Documentation: http://dentedpixel.com/LeanTweenDocumentation/classes/LTBezierPath.html 
        LTBezierPath ltPath = new LTBezierPath(new Vector3[] { myPositionOnScreen, endControl.position, startControl.position, flyTowards.position });

        LeanTween.move(MyImageGO, ltPath, duration).setEase(LeanTweenType.easeInOutCubic).setOnComplete(() => { OnMovementCompleted(MyImageGO); });
        LeanTween.scale(MyImageGO, Vector3.one * 0.8f, duration).setEase(LeanTweenType.easeInOutCubic);
    }

    private GameObject SpriteSpawning(Vector3 myPosition, Transform canvasTransform)
    {
        GameObject MyImageGO = new GameObject();
        MyImageGO.name = "FlyObject";
        Image myImage = MyImageGO.AddComponent<Image>();

        RectTransform trans = myImage.GetComponent<RectTransform>();
        trans.position = myPosition;

        // MyImageGO.transform.position = myPosition;
        MyImageGO.transform.SetParent(canvasTransform);
        MyImageGO.transform.localScale = Vector3.one * INITIAL_SCALE_FACTOR;

        print(trans.position);

        if (spawnNewSprite)
        {
            myImage.sprite = newSprite;
        }
        else
        {
            myImage.sprite = GetComponent<SpriteRenderer>().sprite;
        }

        return MyImageGO;
    }

    public void Initialize(Transform houseTransform, Transform flyTowards, Transform canvasTransform, Transform firstControlPoint, Transform secondControlPoint, GameObject trailGO)
    {
        print(houseTransform);
        Activate(houseTransform, flyTowards, canvasTransform, firstControlPoint, secondControlPoint, trailGO);
    }

    private void OnMovementCompleted(GameObject completedMovementGameObject)
    {
        // Write any code when the animation completes

        switch (myType)
        {
            case FlyingImageGeneratorType.Coin:
                DeliveryBoy.Instance.IncreaseMoney();
                break;
            case FlyingImageGeneratorType.Punya:
                DeliveryBoy.Instance.IncreasePunya();
                break;
        }

        Destroy(completedMovementGameObject);
    }
}