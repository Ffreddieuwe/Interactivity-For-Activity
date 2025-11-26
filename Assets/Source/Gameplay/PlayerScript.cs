using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private int m_basePlayerHP;
    [SerializeField]
    private int m_basePlayerAP;
    [SerializeField]
    private int m_basePlayerBlock;
    [SerializeField]
    private int m_basePlayerAttackDamage;
    [SerializeField]
    private float m_basePlayerHitChance;
    [SerializeField]
    private float m_basePlayerDodgeChance;
    [SerializeField]
    private float m_playerCritChance;

    public JSONHandler.PlayerData yourStats;


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
        public int m_currentBlock;
        public float m_critChance;
    }

    private GameplayStats m_stats = new GameplayStats();

    [SerializeField]
    private GameObject fillBar;
    [SerializeField]
    private GameObject m_APText;
    [SerializeField]
    private GameObject m_BlockText;
    [SerializeField]
    private GameObject m_HPText;

    public void InitStats()
    {
        JSONHandler jsonhandler = GameObject.FindFirstObjectByType(typeof(JSONHandler)) as JSONHandler;
        jsonhandler.ReadJSON();
        yourStats = jsonhandler.playerDataList.playerData[0];

        m_stats.m_maxHP = yourStats.Level / 1000 + m_basePlayerHP;
        m_stats.m_currentHP = m_stats.m_maxHP;
        m_stats.m_maxAP = yourStats.Stamina / 1000 + m_basePlayerAP;
        m_stats.m_currentAP = m_stats.m_maxAP;
        m_stats.m_dodgeChance = (float)(yourStats.Agility / 1000) / 2 + m_basePlayerDodgeChance;
        m_stats.m_hitChance = (float)(yourStats.Agility / 1000) / 2 + m_basePlayerHitChance;
        m_stats.m_attackDamage = yourStats.Strength / 1000 + m_basePlayerAttackDamage;
        m_stats.m_blockAmount = yourStats.Stability / 1000 + m_basePlayerBlock;
        m_stats.m_currentBlock = 0;
        m_stats.m_critChance = m_playerCritChance;

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

    public void NewTurn()
    {
        m_stats.m_currentAP = m_stats.m_maxAP;

        m_APText.GetComponent<TextMeshProUGUI>().text = m_stats.m_currentAP + "/" + m_stats.m_maxAP;
        m_BlockText.GetComponent<TextMeshProUGUI>().text = m_stats.m_currentBlock.ToString();
        m_HPText.GetComponent<TextMeshProUGUI>().text = m_stats.m_currentHP + "/" + m_stats.m_maxHP;
    }

    public void ActionUsed()
    {
        m_stats.m_currentAP--;
        m_APText.GetComponent<TextMeshProUGUI>().text = m_stats.m_currentAP + "/" + m_stats.m_maxAP;
    }

    public void SetBlock(int newBlock)
    {
        m_stats.m_currentBlock = newBlock;
        m_BlockText.GetComponent<TextMeshProUGUI>().text = m_stats.m_currentBlock.ToString();
    }

    public void SetHealth(int newHealth)
    {
        newHealth = newHealth < 0 ? 0 : newHealth;

        m_stats.m_currentHP = newHealth;
        m_HPText.GetComponent<TextMeshProUGUI>().text = m_stats.m_currentHP + "/" + m_stats.m_maxHP;
        UpdateHPFill();
    }
}
