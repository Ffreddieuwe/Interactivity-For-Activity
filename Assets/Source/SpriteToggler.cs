using UnityEngine;
using UnityEngine.UI;

public class SpriteToggler : MonoBehaviour
{
    private bool initialSprite;

    public GameObject target;

    public Sprite sprite1;
    public Sprite sprite2;

    public void Start()
    {
        initialSprite = true;
    }

    public void ToggleSprite()
    {
        if (initialSprite)
        {
            target.GetComponent<Image>().sprite = sprite2;
            initialSprite = false;
        }
        else
        {
            target.GetComponent<Image>().sprite = sprite1;
            initialSprite = true;
        }
    }

    public void SetInitialSprite()
    {
        target.GetComponent<Image>().sprite = sprite1;
        initialSprite = true;
    }

    public void SetSecondarySprite()
    {
        target.GetComponent<Image>().sprite = sprite2;
        initialSprite = false;
    }
}
