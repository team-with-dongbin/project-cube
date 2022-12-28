using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    public static DragSlot instance;
    public Slot dragSlot;

    [SerializeField]
    private Image dragImage;
    [SerializeField]
    private Text dragText;

    void Start()
    {
        instance = this;
    }
    
    public void SetDragImage(Image itemImage)
    {
        dragImage.sprite = itemImage.sprite;
    }

    public void SetDragText(string countText)
    {
        dragText.text = countText;
    }

    public void SetTransform(Vector3 position)
    {
        dragImage.transform.position = position;
    }

    public void SetAlpha(float newAlpha)
    {
        Color color = dragImage.color;
        color.a = newAlpha;
        dragImage.color = color;
    }
}
