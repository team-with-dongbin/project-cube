using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureCube : MonoBehaviour
{
    //private GameObject pictureCube;
    private bool isActive = false;
    public Color color;
    Material outline;

    // Update is called once per frame
    private void Awake()
    {
        outline = new Material(Shader.Find("Draw/OutlineShader"));
    }

    public void MakeActive()
    {
        Color color = GetComponents<Renderer>()[0].material.color;
        color.a = 1.0f;
        GetComponents<Renderer>()[0].material.color = color;

        isActive = true;
    }

    public void MakeInactive()
    {
        Color color = GetComponents<Renderer>()[0].material.color;
        color.a = 0.0f;
        GetComponents<Renderer>()[0].material.color = color;

        isActive = false;
    }

    public void DrawOutline()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null) return;

        List<Material> materialList = new();
        materialList.AddRange(renderer.sharedMaterials);
        if (!materialList.Contains(outline))
            materialList.Add(outline);
        renderer.materials = materialList.ToArray();
    }

    public void EraseOutline()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null) return;

        List<Material> materialList = new();
        materialList.AddRange(renderer.sharedMaterials);
        if (materialList.Contains(outline))
            materialList.Remove(outline);
        renderer.materials = materialList.ToArray();
    }
}
