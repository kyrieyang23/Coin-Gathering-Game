using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recoin : MonoBehaviour
{
    public float RandSpace = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "FuckingInvisibleFloor")
        {
            Instantiate(this.gameObject, new Vector3(Random.Range(-RandSpace,RandSpace), 0f, Random.Range(-RandSpace,RandSpace)), this.gameObject.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
