using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hillclimber : MonoBehaviour
{
    public GameObject agentPrefab;
    public GameObject spawnPoint;
    public int generations;
    public int currentGen = 1;
    public string genome;
    public float fitness;

    // Start is called before the first frame update
    void Start()
    {
        genome = new string('a', 10);
        StartCoroutine(hillclimb());
    }

    IEnumerator hillclimb()
    {
        for (currentGen=1;currentGen<=generations;currentGen++)
        {
            string oldGenome = genome;
            genome = mutate(genome);

            GameObject agent = GameObject.Instantiate(agentPrefab);
            agent.transform.position = spawnPoint.transform.position;
            EvoAgent a = agent.GetComponent<EvoAgent>();
            a.setGenome(genome);

            while (a.running)
            {
                yield return null;
            }

            if (a.fitness < fitness)
                genome = oldGenome;
            else 
                fitness = a.fitness;
                
            GameObject.Destroy(agent);
        }
    }

    string mutate(string genome)
    {
        int r = (int) (Random.value*genome.Length-1);
        char[] s = genome.ToCharArray();
        if (s[r] == 'a')
            s[r] = 'b';
        else if (s[r] == 'b')
            s[r] = 'a';
        return new string(s);
    }
}