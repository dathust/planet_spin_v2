using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateEarth : MonoBehaviour
{

    [SerializeField] private GameObject Planet;


    // Use this for initialization
    public Vector3 Direction;
    public float Speed = 20f;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Direction * Time.deltaTime * Speed);
		bl_Planet.Instance.UpdateChangePlayer();
    }
}
