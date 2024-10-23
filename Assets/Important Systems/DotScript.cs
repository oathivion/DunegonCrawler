using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotScript : MonoBehaviour
{
    //This dictionary contains all the objects which have stacks. The key is the gameObecjt and the values is an array that stores the stacks of each debuff
    public Dictionary<GameObject, int[]> stackDict = new Dictionary<GameObject, int[]>();
    //This array contains the default values for each DOT effect
    readonly int[] defualtStacks = { 0, 0, 0 };
    //These constants are the indexes of which stack is where so its easier to access/understand if we add more DOT effects
    const int PoisonStackIndex = 0;
    const int BurnStackIndex = 1;
    const int BleedStackIndex = 2;

    //Able to change the damage each dot does

    
    string[] damageTypes = { "Poison", "Burn", "Bleed" };

    [SerializeField] float occuranceDelay;
    //Array of all the damages. fits the same order as the rest of the lists
    [SerializeField] float[] damageAmounts;

    bool activeDot = false;

    public void InflictDoT(int stackAmount, string damageType, GameObject target)
    {
        /*This fucntion is how Damage over time works. It takes in the parameters: stack Amount, damage type, and target which are provided by the trap that called it
          */
        //This adds the target to the dictionry if they arent already in it and gives them the default stacks as defined above
        if (!stackDict.ContainsKey(target))
        {
            stackDict.Add(target, new int[]{defualtStacks[0],defualtStacks[1],defualtStacks[2]});
        }
        for (int i = 0; i < damageTypes.Length; i++)
        {
            if (damageTypes[i].Equals(damageType))
            {
                stackDict[target][i] += stackAmount;
            }
        }
        //StartCoroutine(DealDot);
        Debug.Log("Stacks: " + stackAmount + ", Damage Type: " + damageType + ", Target: " + target);
    }

    public void Update()
    {
        if (!activeDot)
        {
            
            StartCoroutine(DealDot());
        }

    }
    GameObject keyToRemove;
    private IEnumerator DealDot()
    {
        activeDot = true;
        while (stackDict.Count > 0)
        {
            foreach (GameObject key in stackDict.Keys)
            {
                HealthScript health = key.GetComponent<HealthScript>();


                for (int i = 0; i < stackDict[key].Length; i++)
                {
                    if (stackDict[key][i] > 0)
                    {
                        health.TakeDamage(damageAmounts[i]);
                        stackDict[key][i] -= 1;
                    }
                }
                if (stackDict[key].Equals(defualtStacks))
                {
                    stackDict.Remove(key);

                }
            }
            yield return new WaitForSeconds(occuranceDelay);
        }
        activeDot = false;
        
    }
}
