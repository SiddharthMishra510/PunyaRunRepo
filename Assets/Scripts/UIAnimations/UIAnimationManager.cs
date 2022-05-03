using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIAnimationManager : MonoBehaviour
{
    public static UIAnimationManager Instance;

    [SerializeField]
    private GameObject coinFlyingImageGeneratorPrefab;
    [SerializeField]
    private GameObject punyaFlyingImageGeneratorPrefab;
    [SerializeField]
    private List<Sprite> emoticonSprites;
    [SerializeField]
    private GameObject emoticonPrefab;
    [SerializeField]
    private Transform coinTransform;
    [SerializeField]
    private Transform punyaTransform;
    [SerializeField]
    private UICanvas UICanvas;
    [SerializeField]
    private GameObject coinTrailPrefab;
    [SerializeField]
    private GameObject punyaTrailPrefab;

    private Image image;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Start()
    {
        image = emoticonPrefab.GetComponent<Image>();
    }

    public GameObject GetRandomEmoticon()
    {
        image.sprite = emoticonSprites[Random.Range(0, emoticonSprites.Count)];
        return emoticonPrefab;
    }

    public void SpawnCoinFlyingImageGenerator(Transform houseTransform)
    {
        Transform firstControlPoint  = UICanvas.GetFirstControlPoint();
        Transform secondControlPoint = UICanvas.GetSecondControlPoint();

        GameObject flyingImageGeneratorGO = Instantiate(coinFlyingImageGeneratorPrefab, UICanvas.transform);
        FlyingImageGenerator2D flyingImageGenerator = flyingImageGeneratorGO.GetComponent<FlyingImageGenerator2D>();
        flyingImageGenerator.Initialize(houseTransform, coinTransform, UICanvas.transform, firstControlPoint, secondControlPoint, coinTrailPrefab);
    }

    public void SpawnPunyaFlyingImageGenerator(Transform foundationTransform)
    {
        Transform firstControlPoint = UICanvas.GetFirstControlPoint();
        Transform secondControlPoint = UICanvas.GetSecondControlPoint();

        GameObject flyingImageGeneratorGO = Instantiate(punyaFlyingImageGeneratorPrefab, UICanvas.transform);
        FlyingImageGenerator2D flyingImageGenerator = flyingImageGeneratorGO.GetComponent<FlyingImageGenerator2D>();
        flyingImageGenerator.Initialize(foundationTransform, punyaTransform, UICanvas.transform, firstControlPoint, secondControlPoint, punyaTrailPrefab);
    }
}