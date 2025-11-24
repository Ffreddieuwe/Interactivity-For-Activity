using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public class GameplayStats
    {
        public int m_currentHP;
        public int m_maxHP;
        public int m_currentAP;
        public int m_maxAP;
        public float m_dodgeChance;
        public float m_hitChance;
        public int m_attackDamage;
        public int m_blockAmount;
    }

    private GameplayStats m_stats = new GameplayStats();

    public GameObject fillBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void InitStats(int maxHP, int maxAP, float dodgeChance, float hitChance, int attackDamage, int blockAmount)
    {
        m_stats.m_currentHP = maxHP;
        m_stats.m_maxHP = maxHP;
        m_stats.m_currentAP = maxAP;
        m_stats.m_maxAP = maxAP;
        m_stats.m_dodgeChance = dodgeChance;
        m_stats.m_hitChance = hitChance;
        m_stats.m_attackDamage = attackDamage;
        m_stats.m_blockAmount = blockAmount;

        UpdateHPFill();
    }

    private void UpdateHPFill()
    {
        float fillAmount = (float)m_stats.m_currentHP / (float)m_stats.m_maxHP;
        fillBar.GetComponent<RectTransform>().localScale = new Vector3(fillAmount, 1, 1);
    }

    public GameplayStats GetStats()
    {
        return m_stats;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
