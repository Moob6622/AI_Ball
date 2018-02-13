using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreedingCenter : MonoBehaviour
{

    public static BreedingCenter breeder;

    public int maxCrossOverLocuses;
    public int maxCLocusSize;
    public int minCLocusSize;

    public int maxMutLocuses;
    public int maxMLocusSize;
    public int minMLocusSize;

    public bool isBreeding;
    public int gen;


    Network[] networks;
    Network[] parents;

	// Use this for initialization
	void Start ()
    {
        networks = new Network[LoadGenreation.generator.population];
        parents = new Network[LoadGenreation.generator.parentsNb];
        breeder = this;
        StartCoroutine(Breeding());
	}
	
	// Update is called once per frame
	void Update ()
    {

       /* Debug.Log(GetData.data.isOutSum);
        Debug.Log(GetData.data.dataGathered);
        if (GetData.data.isOutSum == 10 && GetData.data.dataGathered)
        {
            GetData.data.isOutSum = 0;
            GetData.data.pulse = true;
            GetData.data.pulse1 = true;
            GetData.data.dataGathered = false;
            isBreeding = true;
            Breed();
            isBreeding = false;
            gen++;
            foreach(Network net in networks)
            {
                net.bred = true;
            }
        }*/
	}

    #region Breeding

    public void Breed ()
    {
        // importing parents
        for (int i = 0; i < LoadGenreation.generator.population; i++)
        {
            networks[i] = GetData.data.subjects[i].GetComponentInChildren<Network>();
        }

        for (int i = 0; i <parents.Length; i++)
        {
            foreach (Network net in networks)
            {
                if (GetData.data.parentsIndices[i] == net.index)
                {
                    parents[i] = net;
                }
            }
        }

        // parents crossover & children generation

        for (int j = 0; j < Mathf.CeilToInt(LoadGenreation.generator.population / parents.Length); j++)
        {
            for (int i = 0; i < parents.Length; i++)
            {
                
                networks[i].network.chromosome = Crossover(parents, i);
            }
        }

        // children mutation
        foreach (Network net in networks)
        {
            net.network.chromosome= Mutate(net);
        }
        

    }
     #endregion

    #region Crossover & Mutation

    public float[] Crossover(Network[] parents, int i)
    {
        int locusStart;
        int locusSize;

        for (int x = 0; x < Mathf.CeilToInt(Random.value * maxCrossOverLocuses); x++)
        {
            locusStart = Mathf.CeilToInt(Random.Range(0, parents[0].network.chromosome.Length));
            locusSize = locusStart + Mathf.CeilToInt(Random.Range(minCLocusSize, maxCLocusSize));
            for (int y = locusStart; y <= locusSize; y++)
            {
                if (i != parents.Length -1)
                {
                    Debug.Log(i);
                    parents[i].network.chromosome[y] = parents[i + 1].network.chromosome[y];
                }
                else if (i == parents.Length-1)
                {
                    parents[i].network.chromosome[y] = parents[i - 1].network.chromosome[y];
                }
            }
        }
        return parents[i].network.chromosome;
    }

    public float [] Mutate (Network network)
    {
        int locusStart;
        int locusSize;

        for (int x = 0; x < Mathf.CeilToInt(Random.value * maxMutLocuses); x++)
        {
            locusStart = Mathf.CeilToInt(Random.Range(0, parents[0].network.chromosome.Length));
            locusSize = locusStart + Mathf.CeilToInt(Random.Range(minMLocusSize, maxMLocusSize));

            for (int y = locusStart; y <= locusSize; y++)
            {
                network.network.chromosome[y] = Random.Range(-2f, 2f);
            }
        }
        return network.network.chromosome;
    }
    #endregion

    IEnumerator Breeding()
    {
        yield return new WaitUntil(() => GetData.data.dataGathered);
        while (true)
        {
            isBreeding = true;
            Breed();
            isBreeding = false;
            GetData.data.isOutSum = 0;
            GetData.data.pulse = true;
            GetData.data.pulse1 = true;
            GetData.data.dataGathered = false;
            gen++;
            foreach (Network net in networks)
            {
                net.bred = true;
            }
            yield return new WaitUntil(() => GetData.data.dataGathered);
        }
    }
     


}
