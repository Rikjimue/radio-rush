using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RacetrackGeneration : MonoBehaviour
{
    [SerializeField]
    private GameObject[] trackPieces;

    private List<GameObject> loadedPieces;

    // Start is called before the first frame update
    void Start()
    {
        loadedPieces = new List<GameObject>();
        GameObject p1 = Instantiate(trackPieces[0]);
        p1.gameObject.name = "Straight";
        loadedPieces.Add(p1);
        //p1.transform.rotation = Quaternion.Euler(-90f,45f,0);
        GenerateStraightSegment();
        //GenerateStraightSegment();
        //GenerateCurvedSegment("Left");
        //GenerateCurvedSegment("Left");
    }

    void GenerateStraightSegment()
    {
        GameObject lastTrackSegment = loadedPieces[loadedPieces.Count - 1];
        GameObject trackSegment = Instantiate(trackPieces[0]);
        trackSegment.gameObject.name = "Straight";
        loadedPieces.Add(trackSegment);
        
        if (lastTrackSegment.gameObject.name == "Straight")
        {
            trackSegment.transform.rotation = lastTrackSegment.transform.rotation;
            trackSegment.transform.position = lastTrackSegment.transform.position + lastTrackSegment.transform.right * -32.158f;
        } else if (lastTrackSegment.gameObject.name == "Left")
        {
            trackSegment.transform.rotation = Quaternion.Euler(-lastTrackSegment.transform.eulerAngles.x,
                                                            -lastTrackSegment.transform.eulerAngles.y,
                                                            -lastTrackSegment.transform.eulerAngles.z);
            trackSegment.transform.position = lastTrackSegment.transform.position + lastTrackSegment.transform.right * 32.158f;
            
        } else if (lastTrackSegment.gameObject.name == "Right")
        {
            trackSegment.transform.rotation = lastTrackSegment.transform.rotation;
            trackSegment.transform.position = lastTrackSegment.transform.position + lastTrackSegment.transform.right * -32.158f;
        }
    }

    void GenerateCurvedSegment(string dir)
    {
        GameObject lastTrackSegment = loadedPieces[loadedPieces.Count - 1];
        GameObject trackSegment = Instantiate(trackPieces[dir == "Left" ? 1 : 2]);
        
        trackSegment.gameObject.name = dir;

        if (lastTrackSegment.gameObject.name == "Straight" && dir == "Left")
        {
            trackSegment.transform.rotation = Quaternion.Euler(180f + lastTrackSegment.transform.eulerAngles.x,
                                                            lastTrackSegment.transform.eulerAngles.y,
                                                            -90f + lastTrackSegment.transform.eulerAngles.z);
            trackSegment.transform.position = lastTrackSegment.transform.position +
                                            lastTrackSegment.transform.right * -9.22f +
                                            lastTrackSegment.transform.up * 9.345f;
        }

        if (lastTrackSegment.gameObject.name == "Straight" && dir == "Right" ||
            lastTrackSegment.gameObject.name == "Right" && dir == "Right") {
            trackSegment.transform.rotation = Quaternion.Euler(lastTrackSegment.transform.eulerAngles.x,
                                                            lastTrackSegment.transform.eulerAngles.y,
                                                            90f + lastTrackSegment.transform.eulerAngles.z);
            trackSegment.transform.position = lastTrackSegment.transform.position +
                                            lastTrackSegment.transform.right * -9.22f +
                                            lastTrackSegment.transform.up * -9.345f;
        }

        if (dir == "Right" && lastTrackSegment.gameObject.name == "Left")
        {
            trackSegment.transform.rotation = Quaternion.Euler(180f + lastTrackSegment.transform.eulerAngles.x,
                                                            lastTrackSegment.transform.eulerAngles.y,
                                                            -lastTrackSegment.transform.eulerAngles.z - 90f);
            trackSegment.transform.position = lastTrackSegment.transform.position +
                                            lastTrackSegment.transform.right * 9.22f +
                                            lastTrackSegment.transform.up * -9.345f;
        }
        
        if (lastTrackSegment.gameObject.name == "Left" && dir == "Left")
        {
            trackSegment.transform.rotation = Quaternion.Euler(lastTrackSegment.transform.eulerAngles.x,
                                                            lastTrackSegment.transform.eulerAngles.y,
                                                            90f + lastTrackSegment.transform.eulerAngles.z);
            trackSegment.transform.position = lastTrackSegment.transform.position +
                                            lastTrackSegment.transform.right * 9.22f +
                                            lastTrackSegment.transform.up * 9.345f;
        }

        loadedPieces.Add(trackSegment);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
