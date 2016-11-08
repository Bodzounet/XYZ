using UnityEngine;
using System.Collections;

public class EnemyId : MonoBehaviour
{
    public enum e_EnemyID
    {
        CrystalCrawler,
        ShimmeringConstruct,
        PrismaticGoliath,
        ChromaticAberration
    }

    public enum e_EnemyElite
    {
        Red,
        Green,
        Blue,
        None
    }

    public e_EnemyID id;
    public e_EnemyElite elite;
}
