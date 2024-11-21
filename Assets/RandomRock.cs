using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRock : MonoBehaviour
{
    [SerializeField] GameObject rockDad;
    public int rockKids;
    // Start is called before the first frame update
    void Start()
    {
        rockKids = rockDad.transform.childCount;
        GameObject chosenRockKid = null;
        do
        {
            int randomIndex = Random.Range(0, rockKids);
            chosenRockKid = rockDad.transform.GetChild(randomIndex).gameObject;
        }
        while (!chosenRockKid.activeSelf);
        
        transform.position = chosenRockKid.transform.position;
        chosenRockKid.SetActive(false);
    }

}
