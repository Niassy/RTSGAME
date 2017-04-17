using UnityEngine;
using System.Collections;

public class Population 
{
    Individual[] m_Individual;

    ////////////// Method ///////////////////

    /// <summary>
    /// /////////////
    /// </summary>
    /// <param name="size"></param>
    /// <param name="initialise"></param>
	public Population(int size, bool initialise = false)
    {
        m_Individual = new Individual[size];
        // Initialise population
        if (initialise)
        {
            // Loop and create individuals
            for (int i = 0; i < getSize(); i++)
            {
                Individual newIndividual = new Individual();
                //newIndividual.generateIndividual();
                saveIndividual(i, newIndividual);
            }
        }
    }

    public Population(int sizePop, GameObject[] Weapons, GameObject[] Targets, bool initialise = false)
    {
        m_Individual = new Individual[sizePop];
        // Initialise population
        if (initialise)
        {
            // Loop and create individuals
            for (int i = 0; i < sizePop; i++)
            {
                Individual newIndividual = new Individual( Weapons,Targets);
                //newIndividual.generateIndividual();
                saveIndividual(i, newIndividual);
            }
        }
    }

    public Population(int sizePop, GameObject[] Weapons, GameObject[] Targets, bool initialise ,Team team )
    {
        m_Individual = new Individual[sizePop];
        // Initialise population
        if (initialise)
        {
            // Loop and create individuals
            for (int i = 0; i < sizePop; i++)
            {
                Individual newIndividual = new Individual(Weapons, Targets,team);
                //newIndividual.generateIndividual();
                saveIndividual(i, newIndividual);

                team.reduceTimeExec(Time.deltaTime);
                if (team.getTimeExec() < 0)
                {
                    team.setTimeExec(team.TimeExecAlgo);
                    // outTime = true;
                    break;
                }
            }
        }
    }


    // get fittest individual
	public Individual getFittest()
    {
        Individual fittest = m_Individual[0];
        for (int i = 1; i < m_Individual.Length;i++)
        {
            if (m_Individual[i].getFitNess() > fittest.getFitNess())
                fittest = m_Individual[i];
        }
        return fittest;
    }

	public void saveIndividual(int index, Individual indiv){ m_Individual[index] = indiv; }
	public int getSize(){ return  m_Individual.Length; }

	//////////// Getters and setters //////////

	public Individual getIndividual(int index){ return m_Individual[index]; }

}
