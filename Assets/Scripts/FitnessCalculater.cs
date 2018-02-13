using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitnessCalculater : MonoBehaviour
{
    public static FitnessCalculater calculater;
    public float fitness;
    public int index;
    public int isOut;
    private bool inRange;
    private bool pulse = true;

    // Use this for initialization
    void Start()
    {
        index = int.Parse(gameObject.transform.parent.gameObject.tag);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetData.data.dataGathered)
        {
            fitness = 0;
        }

        if (inRange)
        {
            fitness += Time.deltaTime;
            
        }
        else if (!inRange)
        {
            
            isOut = 1;
            Debug.Log(index.ToString() + ':' + fitness.ToString());
            return;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "SaveZone" + gameObject.transform.parent.gameObject.tag)
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SaveZone" + gameObject.transform.parent.gameObject.tag)
        {
            inRange = false; ;
        }
    }
}
