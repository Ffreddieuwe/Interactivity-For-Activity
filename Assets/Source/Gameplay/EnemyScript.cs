using UnityEngine;
using TMPro;

public class EnemyScript : MonoBehaviour
{
    public class EnemyStats
    {
        public int m_currentHP;
        public int m_currentAP;
        public int m_maxAP;
        public float m_hitChance;
        public int m_attackDamage;
        public int m_blockAmount;
        public int m_currentBlock;
    }

    [SerializeField]
    private int m_BaseEnemyHP;
    [SerializeField]
    private int m_BaseEnemyAP;
    [SerializeField]
    private int m_BaseEnemyBlock;
    [SerializeField]
    private int m_BaseEnemyAttackDamage;
    [SerializeField]
    private float m_BaseEnemyHitChance;

    [SerializeField]
    private GameObject m_HealthText;
    [SerializeField]
    private GameObject m_ShieldText;

    private JSONHandler m_jsonhandler;
    private JSONHandler.EnemyDataList m_JSONStats;

    private EnemyStats m_stats = new EnemyStats();

    public void Init(int level)
    {
        m_JSONStats = m_jsonhandler.enemyDataList;
        JSONHandler.PlayerData enemyData = m_JSONStats.enemyData[level];

        m_stats.m_currentHP = m_BaseEnemyHP + (enemyData.Level / 1000);
        m_stats.m_maxAP = m_BaseEnemyAP + (enemyData.Stamina / 1000);
        m_stats.m_currentAP = m_stats.m_maxAP;
        m_stats.m_hitChance = m_BaseEnemyHitChance + (enemyData.Agility / 1000f);
        m_stats.m_attackDamage = m_BaseEnemyAttackDamage + (enemyData.Strength / 1000);
        m_stats.m_blockAmount = m_BaseEnemyBlock + (enemyData.Stability / 1000);
        m_stats.m_currentBlock = m_stats.m_blockAmount;

        m_HealthText.GetComponent<TextMeshProUGUI>().text = m_stats.m_currentHP.ToString();
        m_ShieldText.GetComponent<TextMeshProUGUI>().text = m_stats.m_currentBlock.ToString();
    }

    public EnemyStats GetStats()
    {
        return m_stats;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_jsonhandler = GameObject.FindFirstObjectByType(typeof(JSONHandler)) as JSONHandler;
    }

    private void UpdateUI()
    {
        m_HealthText.GetComponent<TextMeshProUGUI>().text = m_stats.m_currentHP.ToString();
        m_ShieldText.GetComponent<TextMeshProUGUI>().text = m_stats.m_currentBlock.ToString();
    }

    public void SetBlock(int newBlock)
    {
        m_stats.m_currentBlock = newBlock;
        UpdateUI();
    }

    public void SetHealth(int newHealth)
    {
        m_stats.m_currentHP = newHealth;
        UpdateUI();
    }

    public void UseAction()
    {
        m_stats.m_currentAP--;
    }

    public void StartTurn()
    {
        m_stats.m_currentAP = m_stats.m_maxAP;
    }
}
