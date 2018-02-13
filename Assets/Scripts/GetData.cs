using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GetData : MonoBehaviour
{

    public static GetData data;

    public int[] parentsIndices;
    public GameObject[] subjects ;
    public int isOutSum;
    public bool dataGathered = false;

    public bool pulse = true;
    public bool pulse1 = true;


    FitnessCalculater[] calculaters ;
    float[] scores;


    // Use this for initialization
	void Start ()
    {
        data = this;
        parentsIndices = new int[LoadGenreation.generator.parentsNb];
        subjects = new GameObject[LoadGenreation.generator.population];
        calculaters = new FitnessCalculater[LoadGenreation.generator.population];
        scores = new float[LoadGenreation.generator.population];
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Gather Scoring Information
        if (pulse)
        {
            for (int i = 0; i < LoadGenreation.generator.population; i++)
            {
                subjects[i] = GameObject.FindGameObjectWithTag(i.ToString());
                calculaters[i] = subjects[i].GetComponentInChildren<FitnessCalculater>();
            }
            pulse = false;
        }

        isOutSum = calculaters[0].isOut + calculaters[1].isOut + calculaters[2].isOut + calculaters[3].isOut + calculaters[4].isOut + calculaters[5].isOut + calculaters[6].isOut + calculaters[7].isOut + calculaters[8].isOut + calculaters[9].isOut;


        // Score Sorting and parents designation
        if (pulse1 && isOutSum == 10 )
        {

            for (int i = 0; i < LoadGenreation.generator.population; i++)
            {
                scores[i] = calculaters[i].fitness;
            }
            Array.Sort(scores);
            Array.Reverse(scores);
            for (int i = 0; i < parentsIndices.Length; i++)
            {
                foreach ( FitnessCalculater calc in calculaters)
                {
                    if (scores[i] == calc.fitness)
                    {
                        parentsIndices[i] = calc.index;
                    }
                }
            }
            pulse1 = false;
            dataGathered = true;
        }
    }

    IEnumerator DataGathering ()
    {
        for (int i = 0; i < LoadGenreation.generator.population; i++)
        {
            subjects[i] = GameObject.FindGameObjectWithTag(i.ToString());
            calculaters[i] = subjects[i].GetComponentInChildren<FitnessCalculater>();
        }
        pulse = false;
        yield return new WaitUntil(() => isOutSum == 10 );
        while (true)
        {
            for (int i = 0; i < LoadGenreation.generator.population; i++)
            {
                scores[i] = calculaters[i].fitness;
            }
            Array.Sort(scores);
            Array.Reverse(scores);
            for (int i = 0; i < parentsIndices.Length; i++)
            {
                foreach (FitnessCalculater calc in calculaters)
                {
                    if (scores[i] == calc.fitness)
                    {
                        parentsIndices[i] = calc.index;
                    }
                }
            }
            dataGathered = true;

            yield return new WaitUntil(() => isOutSum == 10);
        }
    }
}
