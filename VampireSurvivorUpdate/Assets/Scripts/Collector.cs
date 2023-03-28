using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Collectible_Items;
using Managers;
using UI;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [Tooltip("The attraction speed of the magnet on the xp")]
    [SerializeField] private float attractionSpeed = 1f;
    
    [SerializeField] private GoldCounter_SO goldCounterSo;
    
    private PlayerStats playerStats;
    private List<XP> xpToAttract = new List<XP>();
    private bool isMagnetActivated = false;

    // Start is called before the first frame update
    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CollectiblePool.instance.blueXpPool.Get();
            CollectiblePool.instance.greenXpPool.Get();
            CollectiblePool.instance.redXpPool.Get();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            CollectiblePool.instance.magnetPool.Get();
        }
        
        AttractXp();
    }

    //TODO - Maybe Move Everything directly to the PlayerStats script so that we can avoid calling PlayerStats in this script and since the OnTriggerEnter2D doesn't provide any information like the amount of XP or Gold
    private void OnEnable()
    {
        //Subscribe to every Collectible Event
        XP.OnXPCollected += AddXP;
        Gold.OnGoldCollected += AddGold;
        Heal.OnHealCollected += AddHealth;
        Magnet.OnMagnetCollected += XPMagnet;
    }
    
    private void OnDisable()
    {
        //Unsubscribe to every Collectible Event
        XP.OnXPCollected -= AddXP;
        Gold.OnGoldCollected -= AddGold;
        Heal.OnHealCollected -= AddHealth;
        Magnet.OnMagnetCollected -= XPMagnet;
    }

    /// <summary>
    /// Add a certain amount of XP to the player
    /// </summary>
    /// <param name="xpAmount"></param>
    private void AddXP(int xpAmount)
    {

        if (playerStats.xp+xpAmount > playerStats.maxXp)
        {
            int xpAfterIncrease = playerStats.xp + xpAmount;
            int xpRestAfterLeveling = xpAfterIncrease - playerStats.maxXp;
            
            playerStats.xp = xpRestAfterLeveling;
            playerStats.maxXp = Mathf.RoundToInt(playerStats.maxXp * 1.1f);
            AddLevel(1);
            UpgradeSystemCS.SpawnUpgradeMenu();
        }
        else
        {
            playerStats.xp += xpAmount;
        }
        
        UIManager.instance.UpdateExpBar();
    }
    
    /// <summary>
    /// Add a level to the player
    /// </summary>
    /// <param name="lvlAmount"></param>
    private void AddLevel(int lvlAmount)
    {
        playerStats.level += lvlAmount;
        UIManager.instance.UpdateExpBar();
    }

    /// <summary>
    /// Add a certain amount of Gold to the player
    /// </summary>
    /// <param name="goldAmount"></param>
    private void AddGold(int goldAmount)
    {
        playerStats.gold += goldAmount;
        UIManager.instance.UpdateGoldCounter();
        goldCounterSo.goldAccumulated = goldCounterSo.goldAccumulated + goldAmount;
        UIManager.instance.UpdateGoldAccumulated();
    }

    [ContextMenu("GoldInfinite")]
    public void TestGold()
    {
        AddGold(20000);
    }

    /// <summary>
    /// Heal the player for 10% of his max health
    /// </summary>
    private void AddHealth()
    {
        playerStats.currentHealth += Mathf.Clamp(playerStats.maxHealth * 0.1f, 0, playerStats.maxHealth);
    }
    
    /// <summary>
    /// Attract every XP on the map to the player
    /// </summary>
    private void XPMagnet()
    {
        xpToAttract.Clear();
        xpToAttract = CollectiblePool.instance.onMapXpList.ToList(); //Bug - It tracks the onMapXpList permanently if we don't use ToList()
        isMagnetActivated = true;
    }

    /// <summary>
    /// Attract every XP on the map to the player
    /// </summary>
    private void AttractXp()
    {
        
        if (!isMagnetActivated)
            return;
        
        if (xpToAttract.Count <= 0) //If this doesn't work use a While instead
        {
            isMagnetActivated = false;
            xpToAttract.Clear();
            return;
        }
        
        foreach (var xp in xpToAttract)
        {
            xp.transform.position = Vector3.MoveTowards(xp.transform.position, transform.position, attractionSpeed);
        }
        
    }
    
    
    private void OnTriggerEnter(Collider col)
    {
        if (col == null)
            return;

        if (col.transform.TryGetComponent(out ICollectible collectible)) //If the collider doesn't have a ICollectible component it do not get it and return false
        {
            collectible.Collect();
            
            if (!col.transform.TryGetComponent(out XP xpCollectible)) //We try to get the XP Component if there is one
                return;
            
            if (!xpToAttract.Contains(xpCollectible)) //And the check if it's in the list 
                return;
            
            xpToAttract.Remove(xpCollectible); //If it is we remove it from the list of xp to attract
        }
    }
}
