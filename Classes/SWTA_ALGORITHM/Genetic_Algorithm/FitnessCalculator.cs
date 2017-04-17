using UnityEngine;
using System.Collections;

public class FitnessCalculator {

    static int[] solution = new int[64];

    static float maxFitness = 0.0f;

    // individual health( target health contained by individual)
    //int

    /* Public methods */
    // Set a candidate solution as a byte array
    public static void setSolution(int[] newSolution)
    {
        solution = newSolution;
    }

    // To make it easier we can use this method to set our candidate solution 
    // with string of 0s and 1s
    static void setSolution()
    {
        //solution = new byte[newSolution.length()];
        //// Loop through each character of our string and save it in our byte 
        //// array
        //for (int i = 0; i < newSolution.length(); i++)
        //{
        //    String character = newSolution.substring(i, i + 1);
        //    if (character.contains("0") || character.contains("1"))
        //    {
        //        solution[i] = Byte.parseByte(character);
        //    }
        //    else
        //    {
        //        solution[i] = 0;
        //    }
        //}
    }

    // initialise individual's target Health
    public static void getIndividualHealth(  Individual individual, GameObject[] Targets ,ref float[] H,ref float [] hl)
    {
        for (int i = 0; i < Targets.Length;i++)
        {
            H[i] = Targets[i].GetComponent<TankHealth>().m_StartingHealth;
            hl[i] = Targets[i].GetComponent<TankHealth>().m_CurrentHealth;
        }
    }

    // Calculate inidividuals fittness by comparing it to our candidate solution
    public static float getFitness(Individual individual, GameObject[] Weapons, GameObject[] Targets)
    {
        float fitness = 1.0f;

        float[] H = new float[Targets.Length];
        float[] hl = new float[Targets.Length];

        FitnessCalculator.getIndividualHealth(individual,Targets ,ref H,ref hl);

        // Loop through our individuals genes and compare them to our cadidates
        for (int i = 0; i < individual.getSize(); i++)
        {
            GameObject ally = Weapons[i];
            TargetSystem targetSystem = ally.GetComponent<TargetSystem>();
            float dmg = ally.GetComponent<TankShooting>().damage;
            float minValue = 100000;
            int allocated = -1;
            // target index
            int indTarget = individual.getSolution(i);
            GameObject ennemy = Targets[indTarget];

            if (hl[indTarget] <= 0.0f)
                continue;

            float reduct = 1 - (hl[indTarget] - dmg) / hl[indTarget];
            // update health left
            hl[indTarget] -= dmg;

            
            if (hl[indTarget] <= 0.0f)
                 hl[indTarget] =0.0f;
            

            // add reduction to fitness

         //   fitness+=reduct;
        }

        for (int i = 0; i < Targets.Length;i++ )
        {
            if (!IndividualUtils.isTargetExitsInSolution(i, individual))
                continue;

            float reduction =   1.0f - ( hl[i] / H[i]);
            fitness *= reduction;
        }

        //for (int i = 0; i < individual.getSize(); i++)
        //{
        //    int indTarget = individual.getSolution(i);
        //    GameObject ennemy = Targets[indTarget];

        //    if (hl[indTarget] <= 0.0f)
        //        continue;

        //    float reduction = 1.0f - (hl[i] / H[i]);
        //    fitness *= reduction;

        //}

        /*if (fitness < 0.000001f)
            fitness = 0.000001f;*/

        return  fitness;
    }



    // Get optimum fitness
    public static float getMaxFitness()
    {
        //float maxFitness = solution.Length;
        return maxFitness;
    }

    // must be called at beginning
    public static void computeMaxFitness(GameObject[] Weapons, GameObject[] Targets)
    {
        float TotalDmg = 0.0f;
        float TotalHealth = 0.0f;
        for (int i = 0; i < Weapons.Length;i++)
        {
            TotalDmg += Weapons[i].GetComponent<TankShooting>().damage;
        }

        for (int i = 0; i <Targets.Length; i++)
        {
            TotalHealth += Weapons[i].GetComponent<TankHealth>().m_CurrentHealth;
        }

        maxFitness = TotalDmg / TotalHealth;
    }

}
