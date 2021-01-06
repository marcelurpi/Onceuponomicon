using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu]
public class FightStatsPage : ScriptableObject
{
    [SerializeField] private bool center;
    [SerializeField] private string bossName;
    [SerializeField] private FightStat[] bossFightStats;

    private const int STATS_PER_ROW = 3;

    public bool GetCenterText() => center;

    public PageTextModule[] GetPageTextModules()
    {
        PageTextModule[] modules = new PageTextModule[(bossFightStats.Length + (STATS_PER_ROW - 1))/STATS_PER_ROW + 2];

        modules[0] = new PageTextModule("<size=100>" + bossName + "</size>\n");
        for (int i = 0; i < (bossFightStats.Length + (STATS_PER_ROW - 1)) / STATS_PER_ROW; i++)
        {
            StringBuilder builder = new StringBuilder("\n");
            for (int j = i * 3; j < i * 3 + STATS_PER_ROW; j++)
            {
                if (j == bossFightStats.Length)
                {
                    break;
                }
                if (j != i * 3)
                {
                    builder.Append("   ");
                }
                builder.Append(bossFightStats[j].GetStatType().ToString()).Append(": ").Append(bossFightStats[j].GetStatNumber());
            }
            modules[i + 1] = new PageTextModule(builder.ToString());
        }
        modules[(bossFightStats.Length + (STATS_PER_ROW - 1)) / STATS_PER_ROW + 1] = new PageTextModule("\n\n<size=80>Fight!</size>\n");

        return modules;
    }
}
