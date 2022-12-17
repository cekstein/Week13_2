using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lever : MonoBehaviour
{
    public Transform startOrientation = null;
    public Transform endOrientation = null;
    public GameObject _object;
    public Color color1, color2;
    

    MeshRenderer meshRenderer = null;
    MeshRenderer meshObject = null;
    float _leverAmount;
   

    //private Material _materialOrigin = null;
    private Color _origionalColor;
    private Color lerpedColor;
    //private Color _colorMix;

    

    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();  
    }

    private void OnTriggerEnter(Collider other)
    {
        meshObject = _object.GetComponentInChildren<MeshRenderer>();
        _origionalColor = meshObject.material.color;
    }
    public void OnLeverPullStart()
    {
        meshRenderer.material.SetColor("_Color", Color.red);
        lerpedColor = Color.Lerp(color1, color2, _leverAmount);
        
    }
    public void OnLeverPullStop()
    {
        meshRenderer.material.SetColor("_Color", Color.white);
        //meshObject.material.SetColor("_Color", color2);
        if (meshObject != null)
        {
            meshObject.material.SetColor("_Color", lerpedColor);
        }
    }
    public void UpdateLever(float percent)
    {
        transform.rotation = Quaternion.Slerp(startOrientation.rotation, endOrientation.rotation, percent);
        _leverAmount = percent;
        
            //meshObject.material.SetColor("_Color", _origionalColor);
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player") && meshObject != null)
        {
            meshObject.material.SetColor("_Color", _origionalColor);
 
        }
    }



}
//lerpedColor = Color.Lerp(Color.red, Color.green, controlAmount);

//gameObject.renderer.material.SetColor("_Color", lerpedColor);