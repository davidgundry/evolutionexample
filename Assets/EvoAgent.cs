using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoAgent : MonoBehaviour
{
    public bool running = true;
    public float fitness = 0;
    public string genome;

    public float speed;
    public float certainty;
    public float energy;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector2(Random.value-0.5f, Random.value-0.5f);
        direction.Normalize();
        StartCoroutine(WaitThenStop());

        if (genome[0] == 'a')
            speed = 0.8f;
        else if (genome[0] == 'b')
            speed = 1.5f;
        if (genome[1] == 'a')
            certainty = 0.5f;
        else if (genome[1] == 'b')
            certainty = 0.9f;
        if (genome[2] == 'a')
            energy = 0.4f;
        else if (genome[2] == 'b')
            energy = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            if (Random.value > certainty)
            {
                direction = new Vector2(Random.value, Random.value);
                direction.Normalize();
            }

            if (energy > 0)
                transform.Translate(direction*speed*Time.deltaTime);
            energy -= Time.deltaTime;

            fitness = Vector2.Distance(transform.position, new Vector2(0,0));
        }
    }


    IEnumerator WaitThenStop()
    {
        yield return new WaitForSeconds(1);
        running = false;
    }

    public void setGenome(string genome)
    {
        this.genome = genome;
    }

}
