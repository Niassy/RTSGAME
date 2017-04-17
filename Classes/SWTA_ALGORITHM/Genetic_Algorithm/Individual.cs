using UnityEngine;
using System.Collections;

public class Individual : MonoBehaviour {

    // array of int pointing to target
	// array refers to weapon
    // size of solution is equals to weapon's size
    // each indice i of array is consideres as weapon
    // each element at indice i is target
	int[] m_solution;

	// fitness
	float fitness = 0.0f;

	// weapopn target solution
	int target;
	int weapon;


    public Individual()
    {
        fitness = 0.0f;
    }

    // ctor
    // size : size of weapon
    public Individual(int sizeWeapon,int sizeTarget)
    {
        m_solution = new int[sizeWeapon];
        generateSolution(sizeTarget);
        //getFitNess()
    }

    public Individual(GameObject[] Weapons, GameObject[] Targets)
    {
        m_solution = new int[Weapons.Length];
        generateSolution(Targets.Length);
        getFitNess(Weapons, Targets);
    }

    public Individual(GameObject[] Weapons, GameObject[] Targets,Team team)
    {
        m_solution = new int[Weapons.Length];
        generateSolution(Targets.Length,team);
        getFitNess(Weapons, Targets);
    }

    public Individual(Individual ind)
    {
        m_solution = new int[ind.getSize()];
        fitness = ind.getFitNess();
    }

    
    // get fitness of our individual
    private float getFitNess(GameObject[] Weapons, GameObject[] Targets)
    {
        fitness = FitnessCalculator.getFitness(this, Weapons, Targets);
        return fitness;
    }

    public float getFitNess()
    {
        return fitness;
    }

	 // Create a random individual
    public void generateSolution()
    {
        for (int i = 0; i < getSize(); i++)
        {
            int random =  Random.Range(0 ,getSize() )  ;
            m_solution[i] = random;
        }
    }

    // Create a random individual
    // params
    // targetsize: size of target to be allocated to weapon( ennemy)
    public void generateSolution(int targetSize)
    {
        for (int i = 0; i < getSize(); i++)
        {
            int random = Random.Range(0, targetSize);
            m_solution[i] = random;
        }
    }

    public void generateSolution(int targetSize,Team team)
    {
        for (int i = 0; i < getSize(); i++)
        {
            int random = Random.Range(0, targetSize);
            m_solution[i] = random;

            //team.reduceTimeExec(Time.deltaTime);
            //if (team.getTimeExec() < 0)
            //{
            //    team.setTimeExec( team.TimeExecAlgo);
            //    // outTime = true;
            //    break;
            //}

        }
    }

	 public void setSolution(int index, int val){ m_solution[index] = val;}
	 public int getSolution(int index){ return m_solution[index]; }
	 public  int getSize(){ return m_solution.Length ;}
}
