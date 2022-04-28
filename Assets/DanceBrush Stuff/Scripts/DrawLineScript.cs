using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;
using HTC.UnityPlugin.Utility;
using UnityEngine.UI;

public class DrawLineScript : MonoBehaviour
{

    public VivePoseTracker tracker1;
    float position1X;
    float position1Y;
    float position1Z;

    public VivePoseTracker tracker2;
    float position2X;
    float position2Y;
    float position2Z;

    public VivePoseTracker tracker3;
    float position3X;
    float position3Y;
    float position3Z;

    public VivePoseTracker tracker4;
    float position4X;
    float position4Y;
    float position4Z;

    //public Canvas FrogImage;
    //public Canvas Mush2Image;
    //public Canvas Mush3Image;
    //public Canvas StrawberryImage;

    private MeshLineRenderer Line1, Line2, Line3, Line4;

    public Material color1, color2, color3, color4;

    public InputField color1R, color1G, color1B;
    public InputField color2R, color2G, color2B;
    public InputField color3R, color3G, color3B;
    public InputField color4R, color4G, color4B;


    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObject1 = new GameObject();
        gameObject1.AddComponent<MeshFilter>();
        gameObject1.AddComponent<MeshRenderer>();
        Line1 = gameObject1.AddComponent<MeshLineRenderer>();

        Line1.lmat = color1;
        Line1.setWidth(.05f);

        GameObject gameObject2 = new GameObject();
        gameObject2.AddComponent<MeshFilter>();
        gameObject2.AddComponent<MeshRenderer>();
        Line2 = gameObject2.AddComponent<MeshLineRenderer>();

        Line2.lmat = color2;
        Line2.setWidth(.05f);

        GameObject gameObject3 = new GameObject();
        gameObject3.AddComponent<MeshFilter>();
        gameObject3.AddComponent<MeshRenderer>();
        Line3 = gameObject3.AddComponent<MeshLineRenderer>();

        Line3.lmat = color3;
        Line3.setWidth(.05f);

        GameObject gameObject4 = new GameObject();
        gameObject4.AddComponent<MeshFilter>();
        gameObject4.AddComponent<MeshRenderer>();
        Line4 = gameObject4.AddComponent<MeshLineRenderer>();

        Line4.lmat = color4;
        Line4.setWidth(.05f);

    } //end Start

    // Update is called once per frame
    void Update()
    {
        Vector3 position1 = VivePose.GetPoseEx(TrackerRole.Tracker1).pos;
        Vector3 position2 = VivePose.GetPoseEx(TrackerRole.Tracker2).pos;
        Vector3 position3 = VivePose.GetPoseEx(TrackerRole.Tracker3).pos;
        Vector3 position4 = VivePose.GetPoseEx(TrackerRole.Tracker4).pos;
        //Debug.Log("Tracker 1 Position: " + position1 + "\nTracker 2 Position: " + position2 + "\nTracker 3 Position: " + position3 + "\nTracker 4 Position: " + position4);
        //Debug.Log("Tracker 3 Position: " + position3 + "\nTracker 4 Position: " + position4);

        // FrogImage.transform.position = new Vector3(position1.x, position1.y, position1.z);
        // Mush2Image.transform.position = new Vector3(position2.x, position2.y, position2.z);
        // Mush3Image.transform.position = new Vector3(position3.x, position3.y, position3.z);
        // StrawberryImage.transform.position = new Vector3(position4.x, position4.y, position4.z);

        Line1.AddPoint(position1);
        Line2.AddPoint(position2);
        Line3.AddPoint(position3);
        Line4.AddPoint(position4);

        /* var position1 = VivePose.GetPoseEx(TrackerRole.Tracker1).pos; //gets position of tracker
        GameObject line = new GameObject(); //created a GameObject that will be the line
        currentLine = line.AddComponent<LineRenderer>(); //adds the Line Renderer component to the currentLine variable
        currentLine.startWidth = .1f; //changes the start width of the line
        currentLine.endWidth = .1f; //changes the end width of the line
        currentLine.positionCount = clicks + 1; //increases the line index
        currentLine.SetPosition(0, position1); //sets the position of the line to the position of the tracker
        clicks++;
        */

    } //end Update

    public void SubmitButtonClick()
    {
        int c1R = int.Parse(color1R.text);
        int c1G = int.Parse(color1G.text);
        int c1B = int.Parse(color1B.text);

       color1.color = new Color32((byte)c1R, (byte)c1G, (byte)c1B, 225);

        int c2R = int.Parse(color2R.text);
        int c2G = int.Parse(color2G.text);
        int c2B = int.Parse(color2B.text);

       color2.color = new Color32((byte)c2R, (byte)c2G, (byte)c2B, 225);

        int c3R = int.Parse(color3R.text);
        int c3G = int.Parse(color3G.text);
        int c3B = int.Parse(color3B.text);

        color3.color = new Color32((byte)c3R, (byte)c3G, (byte)c3B, 225);

        int c4R = int.Parse(color4R.text);
        int c4G = int.Parse(color4G.text);
        int c4B = int.Parse(color4B.text);

        color4.color = new Color32((byte)c4R, (byte)c4G, (byte)c4B, 225);
    }

} //end class
