using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

[CustomEditor(typeof(AIWaypointNetwork))]

public class AIWaypointNetworkEditor : Editor
{

    private void OnSceneGUI()
    {
        AIWaypointNetwork network = (AIWaypointNetwork)target;

        for (int i=0; i<network.Waypoints.Count;i++)
        {
            if (network.Waypoints[i]!=null)
                Handles.Label (network.Waypoints[i].position, "Waypoint " + i.ToString ());
        }

        if (network.DisplayMode == PathDisplayMode.Connections)
        {
            //draw a polyline
            Vector3[] linePoints = new Vector3[network.Waypoints.Count + 1];

            for (int i = 0; i <= network.Waypoints.Count; i++)
            {
                int index = i != network.Waypoints.Count ? i : 0;

                if (network.Waypoints[index] != null)
                    linePoints[i] = network.Waypoints[index].position;

                else
                    linePoints[i] = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);

            }
            Handles.color = Color.green;
            Handles.DrawPolyLine(linePoints);
        }
        else
        if (network.DisplayMode==PathDisplayMode.Path)
        {
            NavMeshPath path = new NavMeshPath();

            Vector3 from = network.Waypoints[network.UIStart].position;
            Vector3 to = network.Waypoints[network.UIEnd].position;

            NavMesh.CalculatePath(from, to, NavMesh.AllAreas, path);
            Handles.color=Color.yellow;
            Handles.DrawPolyLine(path.corners);
        }
    }
}
