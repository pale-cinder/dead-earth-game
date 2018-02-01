using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PathDisplayMode { None, Connections, Path}

public class AIWaypointNetwork : MonoBehaviour
{

    [HideInInspector]

    public PathDisplayMode DisplayMode=PathDisplayMode.Connections;

    [HideInInspector]

    public int UIStart = 0;

    [HideInInspector]

    public int UIEnd = 0;

    [HideInInspector]


    public List<Transform> Waypoints = new List<Transform>();

}
