using UnityEngine;
using UnityEngine.UI;

public class TvMonoVieja : MonoBehaviour
{
    public float speed = 0.1f;
    private RawImage rawImage;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
    }

    void Update()
    {
        if (rawImage != null)
        {
            rawImage.uvRect = new Rect(rawImage.uvRect.x, rawImage.uvRect.y + speed * Time.deltaTime, rawImage.uvRect.width, rawImage.uvRect.height);
        }
    }
}
