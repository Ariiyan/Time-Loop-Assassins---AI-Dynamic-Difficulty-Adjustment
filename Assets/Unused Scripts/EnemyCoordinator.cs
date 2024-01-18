/*using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EnemyCoordinator : MonoBehaviour
{
    public List<Enemy> agents;
    public Transform playerTarget;
    public float coordinatedAttackRange = 15f;
    public int groupSize = 3;
    public Vector3 formationDirection = Vector3.forward;
    public float formationSpacing = 2f;

    private bool isCoordinatedAttack = false;
    private List<List<Enemy>> agentGroups;
    private List<Vector3> attackPositions;

    void Start()
    {
        agentGroups = SplitAgentsIntoGroups(agents, groupSize);
        attackPositions = GenerateAttackPositions(coordinatedAttackRange);
        ArrangeInFormation();
    }

    void Update()
    {
        if (PlayerInRangeForCoordinatedAttack() && !isCoordinatedAttack)
        {
            BeginCoordinatedAttack();
        }
        else if (isCoordinatedAttack && !PlayerInRangeForCoordinatedAttack())
        {
            EndCoordinatedAttack();
        }
    }

    private void ArrangeInFormation()
    {
        Vector3 startPosition = transform.position - formationDirection * (agents.Count / 2 * formationSpacing);
        for (int i = 0; i < agents.Count; i++)
        {
            Vector3 position = startPosition + formationDirection * i * formationSpacing;
            agents[i].MoveToFormationPosition(position);
        }
    }

    bool PlayerInRangeForCoordinatedAttack()
    {
        return agents.Any(agent => Vector3.Distance(agent.transform.position, playerTarget.position) < coordinatedAttackRange);
    }

    void BeginCoordinatedAttack()
    {
        isCoordinatedAttack = true;
        for (int i = 0; i < agentGroups.Count; i++)
        {
            Vector3 attackPosition = attackPositions[i % attackPositions.Count];
            foreach (Enemy agent in agentGroups[i])
            {
                agent.CoordinatedAttackMode(attackPosition);
            }
        }
    }

    void EndCoordinatedAttack()
    {
        isCoordinatedAttack = false;
        foreach (Enemy agent in agents)
        {
            agent.NormalAttackMode();
        }
    }

    List<List<Enemy>> SplitAgentsIntoGroups(List<Enemy> allAgents, int size)
    {
        var groups = new List<List<Enemy>>();
        for (int i = 0; i < allAgents.Count; i += size)
        {
            groups.Add(allAgents.GetRange(i, Mathf.Min(size, allAgents.Count - i)));
        }
        return groups;
    }

    List<Vector3> GenerateAttackPositions(float range)
    {
        var positions = new List<Vector3>();
        int numPositions = groupSize;
        for (int i = 0; i < numPositions; i++)
        {
            float angle = i * 360f / numPositions;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            positions.Add(playerTarget.position + direction * range);
        }
        return positions;
    }
}
*/