using UnityEngine;
using System.Collections;

public class GeneticAlgorithm : AssignmentAlgorithm {

    const double uniformRate = 0.5;
    const double mutationRate = 0.015;
    const int tournamentSize = 5;
    const bool elitism = true;
    public bool running = false;

    public GeneticAlgorithm()
    {
        running = false;
        m_TimeExecution = 5.0f;
        m_CurrentTimeExecution = m_TimeExecution;
        
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public override void assignWeaponToTarget(Team other)
    {
    }


    // 
    public override void assignWeaponToTarget(GameObject[] Weapons, GameObject[] Targets)
    {
        if (Targets.Length <= 0)
            return;
        //if (running)
          //  return;
        Population population = new Population(50, Weapons, Targets,true);
        float maxRedInit = population.getFittest().getFitNess();
        float currentRed = maxRedInit;
        float precRed = 0.0f;
        bool optimum = false;
        int nIter = 0;
        m_InitialFitness = maxRedInit;
        //m_CurrentFitness = m_InitialFitness;
        ////////////// iteration /////////////

        //population = evolvePopulation(population, Targets.Length);
        m_CurrentFitness = population.getFittest().getFitNess(); 


        //m_CurrentFitness = 0.0f;

        /*while ( currentRed >= precRed)
        {         
            precRed = currentRed;
            population = evolvePopulation(population,Targets.Length);
            currentRed = population.getFittest().getFitNess();
            nIter++;
            m_CurrentTimeExecution -= Time.deltaTime; ;
            if (m_CurrentTimeExecution <=0.0f)
            {
                    m_CurrentTimeExecution = m_TimeExecution;
                break;
            }
        }*/
        //m_CurrentFitness = currentRed;

        //if (nIter <= 5)
          //  optimum = true;
        //if (optimum)
        IndividualUtils.assignWeaponToTarget(population.getFittest(), Weapons, Targets);
        running = true;
    }

    public void assignWeaponToTarget(GameObject[] Weapons, GameObject[] Targets,Team team)
    {
        if (Targets.Length <= 0)
            return;
        //if (running)
        //  return;
        Population population = new Population(50, Weapons, Targets, true,team);
        float maxRedInit = population.getFittest().getFitNess();
        float currentRed = maxRedInit;
        float precRed = 0.0f;
        bool optimum = false;
        int nIter = 0;
        m_InitialFitness = maxRedInit;
      
        m_CurrentFitness = population.getFittest().getFitNess();
   
        IndividualUtils.assignWeaponToTarget(population.getFittest(), Weapons, Targets);
        running = true;
    }

    Population evolvePopulation(Population pop,int size )
    {
	    // new population
	    Population newPopulation = new Population(pop.getSize(), false);

        //m_CurrentFitness += 20.0f;
	    // Keep our best individual
	    if (elitism) {
		    newPopulation.saveIndividual(0, pop.getFittest());
	    }

	    // Crossover population
	    int elitismOffset;
	    if (elitism) {
		    elitismOffset = 1;
	    }
	    else {
		    elitismOffset = 0;
	    }
	    // Loop over the population size and create new individuals with
	    // crossover
	    for (int i = elitismOffset; i < pop.getSize(); i++) {
            //m_CurrentFitness++;
		    Individual indiv1 = tournamentSelection(pop);
		    Individual indiv2 = tournamentSelection(pop);

		    Individual child = crossOver(indiv1, indiv2);
		    newPopulation.saveIndividual(i, child);
            //newPopulation.saveIndividual(i, indiv1);
	    }

	    // Mutate population
	    for (int i = elitismOffset; i < newPopulation.getSize(); i++) {
		    mutate(newPopulation.getIndividual(i) ,size);
	    }
	    return newPopulation;
    }

    // refer to article : An empirical investigation of the
    // static weapon target allocation problem
    Individual crossOver(Individual parent1, Individual parent2)
    {
	    // using one point cross

	    Individual child = new Individual(parent1);
	    int randomPos =  Random.Range(0  ,  parent2.getSize() );
	    for (int i = randomPos + 1; i < child.getSize(); i++)
	    {
		    int sol = parent2.getSolution(i);
		    child.setSolution(i, sol);
	    }
	    return child;
    }

    // refer to article : An empirical investigation of the
    // static weapon target allocation problem
    Individual crossOver(Individual indiv1, Individual indiv2,
		                 Individual child1, Individual child2)
    {
	    // using one point cross
	    int random =   Random.Range(0,indiv1.getSize() );

	    child1 =  new Individual(indiv1);
	    child2 =  new Individual(indiv2);

	    // swapping position at randomPos
	    int a = child1.getSolution(random);
	    int b = child2.getSolution(random);
	    int temp = a;
	    a = b;
	    b = temp;
	    child1.setSolution(random, b);
	    child2.setSolution(random, a);
	    return null;
    }

    // size : target
    void mutate(Individual indiv, int size)
    {
	    for (int i = 0; i < indiv.getSize(); i++) {
		    if (Random.Range(0,1) <= mutationRate) {
			    // Create random gene
			    int sol =  Random.Range(0, size);
			    indiv.setSolution(i, sol);
		    }
	    }
    }

    Individual tournamentSelection(Population pop)
    {
	    // Create a tournament population
	    Population tournament = new Population(tournamentSize, false);
	    // For each place in the tournament get a random individual
	    for (int i = 0; i < tournamentSize; i++) {
		    int randomId = (int)(  Random.Range( 0, pop.getSize() ));
		    tournament.saveIndividual(i, pop.getIndividual(randomId));
	    }
	    // Get the fittest
	    Individual fittest = tournament.getFittest();
	    return fittest;
    }

}
