using UnityEngine;

public readonly struct NpcData 
{
    public readonly string tag;
    public readonly Vector3 position;

    public NpcData(string tag, Vector3 position)
    {
        this.tag = tag;
        this.position = position;
    }
}