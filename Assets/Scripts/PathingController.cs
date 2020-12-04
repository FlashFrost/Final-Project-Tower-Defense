using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathingController : MonoBehaviour
{
    public static List<Target> Path;

    public static void AddTarget(Target t)
    {
        if (Path == null) Path = new List<Target>();

        Path.Add(t);
        for (var i = Path.Count - 2; i >= 0; --i)
        {
            if (Path[i].SequenceNumber > Path[i + 1].SequenceNumber)
            {
                var temp = Path[i];
                Path[i] = Path[i + 1];
                Path[i + 1] = temp;
            }
        }
    }

    public static Target GetNextTarget(Target t)
    {
        if (t == null) return Path[0];
        if (Path.IndexOf(t) + 1 == Path.Count) return null;
        return Path[Path.IndexOf(t) + 1];
    }
}
