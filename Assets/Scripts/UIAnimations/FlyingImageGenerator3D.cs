using UnityEngine;

public class FlyingImageGenerator3D : MonoBehaviour
{
    [SerializeField]
    private GameObject flyingImagePrefab;
    [Tooltip("The icon in UI towards which the item should fly")]
    [SerializeField]
    private Transform FlyTowards;
    [Tooltip("The UI canvas the flyingImagePrefab should be added to")]
    [SerializeField]
    private Transform canvasTransform;
    [SerializeField]
    private Transform startControl;
    [SerializeField]
    private Transform endControl;

    [Tooltip("Fly duration")]
    [SerializeField]
    private float duration = 2f;

    public void Activate()
    {
        Vector3 myPositionOnScreen = Camera.main.WorldToScreenPoint(transform.position);

        GameObject myFlyingImage = Instantiate(flyingImagePrefab, myPositionOnScreen, Quaternion.identity, canvasTransform);

        // Documentation: http://dentedpixel.com/LeanTweenDocumentation/classes/LTBezierPath.html 
        LTBezierPath ltPath = new LTBezierPath(new Vector3[] { myPositionOnScreen, endControl.position, startControl.position, FlyTowards.position }); 
        var LTDescr = LeanTween.move(myFlyingImage, ltPath, duration).setEase(LeanTweenType.easeInOutQuad);

        LTDescr.destroyOnComplete = true; // Destroy the flyingImagePrefab on completion

        OnComplete();
    }

    private void OnComplete()
    {
        // Write any code when the animation completes
    }
}