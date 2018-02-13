using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGenreation : MonoBehaviour
{
    public static LoadGenreation generator;
    public Transform trainingSubject;
    public int population;
    public int parentsNb;

	// Use this for initialization
	void Awake ()
    {
        for (int i = 0; i < population; i++)
        {
            if (i < 5)
            {
                Instantiate (trainingSubject, new Vector3(i * 20f, 0f, 0f), Quaternion.identity);
                trainingSubject.gameObject.tag = i.ToString();
                trainingSubject.GetChild(2).GetChild(0).gameObject.tag = "SaveZone" + i.ToString();
            }
            else
            {
                Instantiate (trainingSubject, new Vector3((i-5) * 20f, 0f, 20f), Quaternion.identity);
                trainingSubject.gameObject.tag = i.ToString();
                trainingSubject.GetChild(2).GetChild(0).gameObject.tag = "SaveZone" + i.ToString();
            }
        }

        generator = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
