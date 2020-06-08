using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRatio : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField, Tooltip("that mesh will show static and hold runtime")]
    private GameObject ConstantMesh;

    [SerializeField, Tooltip("that mesh will show animation and destroy after play animation")]
    private GameObject AnimatedMesh;
#pragma warning restore 649


    private void Start()
    {
        Active(false)
;    }

    public Transform GetRatio
    {
        get
        {
            return this.transform;
        }
    }


    public void SetTexture(Texture2D texture)
    {
        ConstantMesh.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
        AnimatedMesh.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
    }

    public void Set_Position(Vector3 pos)
    {
        this.transform.position = pos;
     
    }

    public void Active(bool enable)
    {
        ConstantMesh.SetActive(enable);
        AnimatedMesh.SetActive(enable);
    }


    public Vector3 Size
    {
        get
        {
            return this.transform.localScale;
        }
        set 
        {
            this.transform.localScale = value;
        }
    
    }
   
}
