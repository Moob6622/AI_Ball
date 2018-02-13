using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NeuralNet
{
    public int[] netInfo;
    public float [][,] weights;
    public float[][,] biases;
    public float[] chromosome;
}


public class Network : MonoBehaviour
{
    public NeuralNet network;
    public int index;
    public int[] netStruct;
    float [,] inputLayer;
    float[,] output;
    public GameObject ball;

    public bool bred;


    // Use this for initialization
    void Start()
    {
        index = int.Parse(gameObject.transform.parent.gameObject.tag);
        network = NetBuild(netStruct, network);
        ball.transform.localPosition = new Vector3(Random.Range(-5, 5), Random.Range(3, 8), Random.Range(-5, 5));
    }

    // Update is called once per frame
    void Update()
    {
        if (!BreedingCenter.breeder.isBreeding)
        {
            inputLayer = new float[3, 1]
            {
            {ball.transform.position.x},
            {ball.transform.position.y},
            {ball.transform.position.z},
            };
            output = FeedForward(inputLayer, network);
            gameObject.transform.eulerAngles = new Vector3(output[0, 0] * 360, output[1, 0] * 360, output[2, 0] * 360);

            if (bred)
            {
                NetUpdate(network);
                ball.transform.localPosition = new Vector3(Random.Range(-5, 5), Random.Range(3, 8), Random.Range(-5, 5));
                bred = false;
            }
        }


    }

    public NeuralNet NetBuild (int[] netInfo, NeuralNet net)
    { 
        net.netInfo = netInfo;
        int chromosomeLength = 0;
        int counter = 0;
        net.biases = new float[netInfo.Length - 1][,];
        net.weights = new float[netInfo.Length-1][,];

        #region Initalise Network
        for (int i = 1; i < netInfo.Length; i++)
        {
            net.biases[i-1] = new float[netInfo[i], 1];
            net.weights[i-1] = new float[netInfo[i], netInfo[i - 1]];

            for (int x = 0; x < netInfo[i]; x++)
            {
                net.biases[i - 1][x, 0] = Random.Range(-2f, 2f);

                for (int y = 0; y < netInfo[i - 1]; y++)
                {
                    net.weights [i-1][x, y] = Random.Range(-2f, 2f);
                }

                chromosomeLength += netInfo[i] + netInfo[i] * netInfo[i - 1] ;
            }
        }
        #endregion
        #region Create initial Network Chromosome
        net.chromosome = new float[chromosomeLength];
        for (int i = 1; i < netInfo.Length; i++)
        {
            for (int x = 0; x < netInfo[i]; x++)
            {
                for (int y = 0; y < netInfo[i - 1]; y++)
                {
                    net.chromosome[counter] = net.weights[i - 1][x, y];
                    counter++;
                }
                net.chromosome[counter] = net.biases[i - 1][x,0];
                counter++;
            }
        }
        #endregion
        return net;
    }

    public NeuralNet NetUpdate (NeuralNet net)
    {
        int counter = 0;

        for (int i = 1; i < net.netInfo.Length; i++)
       {
            for (int x = 0; x < net.netInfo[i]; x++)
            {
                for (int y = 0; y < net.netInfo[i - 1]; y++)
                {
                    net.weights[i - 1][x, y] = net.chromosome[counter];
                    counter++;
                }
                net.biases[i - 1][x, 0] = net.chromosome[counter];
                counter++;
            }
        }
        return net;
    }

    public float[,] Sigmoid (float[,] z)
    {
        for (int i = 0; i < z.GetLength(0); i++)
        {
            for (int j = 0; j < z.GetLength(1); j++)
            {
                z[i, j] = 1 / (1 + Mathf.Exp(-z[i, j]));
            }
        }
        return z;
    }

    public float[,] Prod (float[,] a, float[,] b)
    {
        float[,] c = new float[a.GetLength(0), b.GetLength(1)];
        if (a.GetLength(1) == b.GetLength(0))
        {
            for (int i = 0; i < c.GetLength(0);i++)
            {
                for (int j = 0; j < c.GetLength(1); j++)
                {
                    c[i, j] = 0;
                    for (int k = 0; k < a.GetLength(1); k++)
                    {
                        c[i, j] = c[i, j] + a[i, k] * b[k, j] ;
                    }
                }
            }
            return c;
        }
        else
        {
            Debug.Log("invalid multiplication");
            return null;
        }
    }

    public float[,] Add (float [,] a , float [,] b)
    {
        float[,] c = new float[a.GetLength(0), a.GetLength(1)];
        if (a.GetLength(0) == b.GetLength(0) && a.GetLength(1) == b.GetLength(1))
        {
            for (int i = 0; i < c.GetLength(0); i++)
            {
                for (int j = 0; j < c.GetLength(1); j++)
                {
                    c[i, j] = a[i, j] + b[i, j];
                }
            }
            return c;
        }
        else
        {
            Debug.Log("invalid multiplication");
            return null;
        }
    }

    public float [,] FeedForward (float [,] a, NeuralNet net)
    {
        for (int i = 0; i < net.weights.Length; i++)
        {
            a = Sigmoid(Add(Prod(net.weights[i], a), net.biases[i]));
        }
        return a;
    }


}
