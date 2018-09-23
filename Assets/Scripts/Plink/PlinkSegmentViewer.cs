using UnityEngine;
using System.Collections;

public class PlinkSegmentViewer : MonoBehaviour
{
    public PlinkController plinkController;
    public GameObject Visuals;
    public Transform PositionIndicator;
    public Transform Bar1;
    public Transform Bar2;
    public Transform Bar3;
    public Transform OctaveBarHigh;
    public Transform OctaveBarLow;
    public Material MaterialA;
    public Material MaterialB;

    Renderer barRenderer1;
    Renderer barRenderer2;
    Renderer barRenderer3;


    Transform player;
    float segmentSize {  get { return plinkController.instrumentController.instrument.SegmentSize; } }
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;

        //Visuals.SetActive(false);

        barRenderer1 = Bar1.GetComponent<Renderer>();
        barRenderer2 = Bar2.GetComponent<Renderer>();
        barRenderer3 = Bar3.GetComponent<Renderer>();
    }

    void Update()
    {
        var activeSegment = plinkController.instrumentController.instrument.GetSegmentIndex(plinkController.transform.position);

        //Set scale
        var scale = transform.localScale;
        scale.y = segmentSize;
        //scale.x = segmentSize;
        transform.localScale = scale;

        //Set rotation
        Vector3 delta;
        delta = transform.position - player.position;
        delta.y = 0;
        transform.rotation = Quaternion.LookRotation(delta);

        //Position
        //Overall
        var position = transform.position;
        position.y = plinkController.instrumentController.instrument.baseline.position.y + activeSegment * segmentSize + segmentSize / 2;
        transform.position = position;

        //Center bar
        position = PositionIndicator.position;
        position.y = plinkController.transform.position.y;
        PositionIndicator.position = position;

        //Octave indicators
        int count = ScaleProvider.Instance.Notes.Length + 1;
        //int count = plinkController.instrumentController.instrument.AudioClips.Length;
        int nextOctaveDownIndex = ScaleProvider.Instance.GetLowerBoundsIndex(activeSegment, plinkController.instrumentController.instrument.AudioClips.Length);
        int nextOctaveUpIndex = ScaleProvider.Instance.GetUpperBoundsIndex(activeSegment, plinkController.instrumentController.instrument.AudioClips.Length);

        position = OctaveBarHigh.position;
        position.y = plinkController.instrumentController.instrument.baseline.position.y + nextOctaveUpIndex * segmentSize;
        OctaveBarHigh.position = position;

        position = OctaveBarLow.position;
        position.y = plinkController.instrumentController.instrument.baseline.position.y + nextOctaveDownIndex * segmentSize;
        OctaveBarLow.position = position;

        //Set materials
        MaterialA = plinkController.instrumentController.instrument.DrawMaterial;
        MaterialB = plinkController.instrumentController.instrument.DrawMaterialInactive;

        barRenderer1.material = activeSegment % 2 == 0 ? MaterialA : MaterialB;
        barRenderer2.material = activeSegment % 2 == 1 ? MaterialA : MaterialB;
        barRenderer3.material = activeSegment % 2 == 0 ? MaterialA : MaterialB;
    }

    void oldUpdate ()
    {
        return;
        var isActive = plinkController.instrumentController != null;
        //Visuals.SetActive(isActive);
        if (isActive)
        {
            var activeSegment = plinkController.instrumentController.instrument.GetSegmentIndex(plinkController.transform.position);
            var scale = transform.localScale;
            scale.y = segmentSize;
            transform.localScale = scale;

            var position = transform.position; // plinkController.transform.position + plinkController.transform.forward * -0.1f;
            

            Vector3 delta;
            position.y = plinkController.instrumentController.instrument.baseline.position.y + activeSegment * segmentSize + segmentSize / 2;
            transform.position = position;

            delta = position - player.position;
            delta.y = 0;
            //transform.rotation = Quaternion.LookRotation(delta);

            position = PositionIndicator.position;
            position.y = plinkController.transform.position.y;
            PositionIndicator.position = position;

            MaterialA = plinkController.instrumentController.instrument.DrawMaterial;
            MaterialB = plinkController.instrumentController.instrument.DrawMaterialInactive;

            barRenderer1.material = activeSegment % 2 == 0 ? MaterialA : MaterialB;
            barRenderer2.material = activeSegment % 2 == 1 ? MaterialA : MaterialB;
            barRenderer3.material = activeSegment % 2 == 0 ? MaterialA : MaterialB;

            int count = plinkController.instrumentController.instrument.AudioClips.Length;
            int nextOctaveDownIndex = ((int)(activeSegment / count)) * count;
            int nextOctaveUpIndex = (((int)(activeSegment / count)) + 1) * count;

            position = OctaveBarHigh.position;
            position.y = nextOctaveUpIndex * segmentSize;
            OctaveBarHigh.position = position;

            position = OctaveBarLow.position;
            position.y = nextOctaveDownIndex * segmentSize;
            OctaveBarLow.position = position;

        }
    }
}
