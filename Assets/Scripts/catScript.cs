using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class catScript : MonoBehaviour
{
    private Rigidbody mCatRB;
    private Transform mCatT;
    private int mForwardPressed = 0;
    private int numberOfPoints = 0;
    public GameObject wall;
    private GameObject wallGO;

    public Text txt;
    public float mSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        mCatRB = gameObject.GetComponent<Rigidbody>();
        mCatT = gameObject.GetComponent<Transform>();
        wallGO = Instantiate(wall);
        //at first, the cat sit 
    }

   
    // Update is called once per frame
    void FixedUpdate()
    {
        if (mCatT.position.y < -9)
        {
            numberOfPoints = 0;
            txt.text = numberOfPoints.ToString();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            mCatT.position = mCatT.position + (Vector3.left * Time.deltaTime * 10* mSpeed);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            mCatT.position = mCatT.position + (Vector3.right * Time.deltaTime * 10* mSpeed);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {

            mCatT.position = mCatT.position + (Vector3.forward * Time.deltaTime * 10* mSpeed);
            
            mForwardPressed++;

        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            mCatRB.Sleep();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            mCatRB.AddForce(Vector3.up * 50f);
        }


    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            numberOfPoints++;
            txt.text = numberOfPoints.ToString();
            //Destroy(other.gameObject);
            wallGO.transform.position = new Vector3(0,0,wallGO.transform.position.z+100);
        }
    }

}
