using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public OnlineMaps MapGPS;
    public double firstLat, firstLong, LastLat, LastLong;
    public Slider mapSlider;
    public Texture2D startMarker, endMarker;
    public bool ExploreFinish;
    // Start is called before the first frame update
    void Start()
    {
        ExploreFinish = false;
        MapGPS.latitude = firstLat = GPS.instance.latitude;
        MapGPS.longitude = firstLong = GPS.instance.longitude;
    }

    // Update is called once per frame
    void Update()
    {
        MapGPS.zoom = (int)mapSlider.value;

        if (ExploreFinish)
        {
            ExploreFinish = false;
            MapGPS.AddMarker(new Vector2((float)firstLong, (float)firstLat), startMarker, "Start of Exploration");

            LastLat = GPS.instance.latitude;
            LastLong = GPS.instance.longitude;
            MapGPS.AddMarker(new Vector2((float)LastLong, (float)LastLat), endMarker, "End of Exploration");
        }


    }
}
