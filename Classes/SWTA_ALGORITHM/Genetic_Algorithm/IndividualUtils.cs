using UnityEngine;
using System.Collections;

public class IndividualUtils {

    // given an individual containing weapon 
    // target t
    // 
    public static void IndividualToWeaponTarget()
    {
    }

    public static void assignWeaponToTarget(Individual individual , GameObject[] Weapons, GameObject[] Targets)
    {
        for (int i = 0; i < individual.getSize();i++ )
        {
            GameObject ally = Weapons[i];
            TargetSystem targetSystem = ally.GetComponent<TargetSystem>();
            //if (targetSystem.Target != null && targetSystem.Target.activeSelf)
               // continue;

            // target index
            int indTarget = individual.getSolution(i);
            GameObject ennemy = Targets[indTarget];
            ally.GetComponent<TargetSystem>().Target = ennemy;
        }
    }

    // given a target and a individual
    // this function determines if a target
    // is present in a solution presented by an
    // individual
    public static bool isTargetExitsInSolution(int target,Individual individual)
    {
        for (int i = 0; i < individual.getSize(); i++)
        {
            // target index
            int indTarget = individual.getSolution(i);
            if (target == indTarget)
                return true;
        }
        return false;
    }
}
