using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NodeExample : EditorWindow {

    public List<NodeBaseClass> myNodes = new List<NodeBaseClass>();

    public int nodeAttachID = -1;
    [MenuItem("Node Editor/ Editor")]
	public static void showWindow()
    {
        GetWindow<NodeExample>();
    }

    public void OnGUI()
    {
        for (int i = 0; i < myNodes.Count; i++)
        {
            for (int j = 0; j < myNodes[i].linkedNodes.Count; j++)
            {
                DrawNodeCurve(myNodes[i].rect, myNodes[i].linkedNodes[j].rect);
            }
        }
        if (GUILayout.Button("Node A"))
        {
           myNodes.Add(new NodeA(new Rect(10,40,100,100),myNodes.Count));
           myNodes[myNodes.Count-1].closeFunction += RemoveNode;
            myNodes[myNodes.Count - 1].nodeEditor = this;
        }
        if (GUILayout.Button("Node B"))
        {
            myNodes.Add(new NodeB(new Rect(110, 40, 100, 100), myNodes.Count));
            myNodes[myNodes.Count-1].closeFunction += RemoveNode;
            myNodes[myNodes.Count - 1].nodeEditor = this;
        }

       BeginWindows();
        for(int i = 0; i < myNodes.Count; i++)
        {
            myNodes[i].rect = GUI.Window(i, myNodes[i].rect, myNodes[i].DrawGUI, "Node " + i);
        }
        EndWindows();

       for (int i = 0; i < myNodes.Count; i++)
        {
			 if (GUI.Button(new Rect(myNodes[i].rect.xMax - 10, myNodes[i].rect.y + myNodes[i].rect.height / 2, 20, 20), "+"))
            {
                BeginAttachment(i);
            }

            if (GUI.Button(new Rect(myNodes[i].rect.xMin - 10, myNodes[i].rect.y + myNodes[i].rect.height / 2, 20, 20), "O"))
            {
                EndAttachment(i);
            }
        }
    }

    public void RemoveNode(int id)
    {
        for (int i = 0; i < myNodes.Count; i++)
        {
            myNodes[i].linkedNodes.RemoveAll(item => item.id == id);
        }
        myNodes.RemoveAt(id);
        UpdateNodeIDs();
    }

    public void UpdateNodeIDs()
    {
        for(int i = 0; i < myNodes.Count; i++)
        {
            myNodes[i].ReassignID(i);
        }
    }

    public void BeginAttachment(int winID)
    {
        nodeAttachID = winID;
    }
        
    public void EndAttachment(int winID)
    {
        if (nodeAttachID > -1)
        {
            myNodes[nodeAttachID].AttachComplete(myNodes[winID]);
        }
        nodeAttachID = -1;
    }

    void DrawNodeCurve(Rect start, Rect end)
    {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + (start.height / 2)+10, 0);
        Vector3 endPos = new Vector3(end.x, end.y + (end.height / 2)+ 10, 0);
        Vector3 startTan = startPos + Vector3.right * 100;
        Vector3 endTan = endPos + Vector3.left * 100;
        Color shadowCol = new Color(0, 0, 0, 0.06f);

       
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 5);
    }

}
