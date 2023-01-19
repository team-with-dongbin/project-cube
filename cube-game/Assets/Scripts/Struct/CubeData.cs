using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/CubeData", fileName = "Cube Data")]
public class CubeData : ItemData
{
    public static Color[] colorList = { Color.black, Color.red, Color.green, Color.blue, 
        Color.yellow, Color.magenta, Color.cyan, Color.white };

    [Dropdown("colorList", "ToString()")]
    public Color color;

    public PlayerStatus statusChanging;
    public float initialHp = 100f;
    public AudioClip strikeSound;
    public AudioClip destroySound;
}
