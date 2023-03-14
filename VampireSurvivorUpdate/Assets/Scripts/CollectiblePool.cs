using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CollectiblePool : MonoBehaviour
{
    public static CollectiblePool instance;

    [Header("Xp Pools")]
    public ObjectPool<XP> blueXpPool;
    [Tooltip("The prefab of the Blue Xp")]
    [SerializeField] private GameObject blueXpPrefab;
    public ObjectPool<XP> greenXpPool;
    [Tooltip("The prefab of the Green Xp")]
    [SerializeField] private GameObject greenXpPrefab;
    public ObjectPool<XP> redXpPool;
    [Tooltip("The prefab of the Red Xp")]
    [SerializeField] private GameObject redXpPrefab;
    [HideInInspector] public List<XP> onMapXpList = new List<XP>(); //Will help with the Magnet item, instead of finding and attracting every xp to the player it will only attract those who are actually on the map and we don't need to search for them anymore

    [Header("Gold Pools")]
    public ObjectPool<Gold> smallGoldPool;
    [Tooltip("The prefab of the Small Gold")]
    [SerializeField] private GameObject smallGoldPrefab;
    public ObjectPool<Gold> midGoldPool;
    [Tooltip("The prefab of the Mid Gold")]
    [SerializeField] private GameObject midGoldPrefab;
    public ObjectPool<Gold> bigGoldPool;
    [Tooltip("The prefab of the Big Gold")]
    [SerializeField] private GameObject bigGoldPrefab;
    
    [Header("Heal Pool")]
    public ObjectPool<Heal> healPool;
    [Tooltip("The prefab of the Heal")]
    [SerializeField] private GameObject healPrefab;
    
    [Header("Magnet Pool")]
    public ObjectPool<Magnet> magnetPool;
    [Tooltip("The prefab of the Magnet")]
    [SerializeField] private GameObject magnetPrefab;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InitializePool();
    }

    /// <summary>
    /// Initialize every item pool when called
    /// </summary>
    private void InitializePool()
    {
        //--- Xp Pools ---
        blueXpPool = new ObjectPool<XP>(CreateBlueXp, OnTakeXpFromPool, OnReturnXpToPool, OnDestroyXp, true, 150, 300);
        greenXpPool = new ObjectPool<XP>(CreateGreenXp, OnTakeXpFromPool, OnReturnXpToPool, OnDestroyXp, true, 150, 300);
        redXpPool = new ObjectPool<XP>(CreateRedXp, OnTakeXpFromPool, OnReturnXpToPool, OnDestroyXp, true, 150, 300);
        
        //--- Gold Pools ---
        smallGoldPool = new ObjectPool<Gold>(CreateSmallGold, OnTakeGoldFromPool, OnReturnGoldToPool, OnDestroyGold, true, 15, 30);
        midGoldPool = new ObjectPool<Gold>(CreateMidGold, OnTakeGoldFromPool, OnReturnGoldToPool, OnDestroyGold, true, 15, 30);
        bigGoldPool = new ObjectPool<Gold>(CreateBigGold, OnTakeGoldFromPool, OnReturnGoldToPool, OnDestroyGold, true, 15, 30);
        
        //--- Magnet Pool ---
        magnetPool = new ObjectPool<Magnet>(CreateMagnet, OnTakeMagnetFromPool, OnReturnMagnetToPool, OnDestroyMagnet, true, 2, 4);
        
        //--- Heal Pool ---
        healPool = new ObjectPool<Heal>(CreateHeal, OnTakeHealFromPool, OnReturnHealToPool, OnDestroyHeal, true, 10, 20);
    }
    
    //Contains all of the function needed for the Xp Pools
    #region XP_Pool

    /// <summary>
    /// Create a Blue XP game object and add it to the pool
    /// </summary>
    /// <returns></returns>
    private XP CreateBlueXp()
    {
        XP xp = Instantiate(blueXpPrefab, transform).GetComponent<XP>();

        return xp;
    }
    
    /// <summary>
    /// Create a Green XP game object and add it to the pool
    /// </summary>
    /// <returns></returns>
    private XP CreateGreenXp()
    {
        XP xp = Instantiate(greenXpPrefab, transform).GetComponent<XP>();

        return xp;
    }
    
    /// <summary>
    /// Create a Red XP game object and add it to the pool
    /// </summary>
    /// <returns></returns>
    private XP CreateRedXp()
    {
        XP xp = Instantiate(redXpPrefab, transform).GetComponent<XP>();

        return xp;
    }

    /// <summary>
    /// Take a XP Game object from the pool
    /// </summary>
    /// <param name="xp"></param>
    private void OnTakeXpFromPool(XP xp)
    {
        xp.gameObject.SetActive(true);
        onMapXpList.Add(xp);
    }

    /// <summary>
    /// Return the XP game object to the pool
    /// </summary>
    /// <param name="xp"></param>
    private void OnReturnXpToPool(XP xp)
    {
        xp.gameObject.SetActive(false);
        onMapXpList.Remove(xp);
    }

    /// <summary>
    /// Destroy the XP Game Object if it go over the max amount of item allowed by the pool.
    /// </summary>
    /// <param name="xp"></param>
    private void OnDestroyXp(XP xp)
    {
        Destroy(xp);
        onMapXpList.Remove(xp);
    }

    #endregion

    //Contains all of the function needed for the Gold Pools
    #region Gold_Pool

    /// <summary>
    /// Create a small Gold game object and add it to the pool
    /// </summary>
    /// <returns></returns>
    private Gold CreateSmallGold()
    {
        Gold gold = Instantiate(smallGoldPrefab, transform).GetComponent<Gold>();

        return gold;
    }
    
    /// <summary>
    /// Create a mid Gold game object and add it to the pool
    /// </summary>
    /// <returns></returns>
    private Gold CreateMidGold()
    {
        Gold gold = Instantiate(midGoldPrefab, transform).GetComponent<Gold>();

        return gold;
    }
    
    /// <summary>
    /// Create a big Gold game object and add it to the pool
    /// </summary>
    /// <returns></returns>
    private Gold CreateBigGold()
    {
        Gold gold = Instantiate(bigGoldPrefab, transform).GetComponent<Gold>();

        return gold;
    }

    /// <summary>
    /// Take a Gold Game object from the pool
    /// </summary>
    /// <param name="gold"></param>
    private void OnTakeGoldFromPool(Gold gold)
    {
        gold.gameObject.SetActive(true);
    }

    /// <summary>
    /// Return the Gold game object to the pool
    /// </summary>
    /// <param name="gold"></param>
    private void OnReturnGoldToPool(Gold gold)
    {
        gold.gameObject.SetActive(false);
    }

    /// <summary>
    /// Destroy the Gold Game Object if it go over the max amount of item allowed by the pool.
    /// </summary>
    /// <param name="gold"></param>
    private void OnDestroyGold(Gold gold)
    {
        Destroy(gold);
    }


    #endregion

    //Contains all of the function needed for the Magnet Pool
    #region Magnet_Pool

    /// <summary>
    /// Create a Magnet game object and add it to the pool
    /// </summary>
    /// <returns></returns>
    private Magnet CreateMagnet()
    {
        Magnet magnet = Instantiate(magnetPrefab, transform).GetComponent<Magnet>();

        return magnet;
    }

    /// <summary>
    /// Take a Magnet Game object from the pool
    /// </summary>
    /// <param name="magnet"></param>
    private void OnTakeMagnetFromPool(Magnet magnet)
    {
        magnet.gameObject.SetActive(true);
    }

    /// <summary>
    /// Return the Magnet game object to the pool
    /// </summary>
    /// <param name="magnet"></param>
    private void OnReturnMagnetToPool(Magnet magnet)
    {
        magnet.gameObject.SetActive(false);
    }

    /// <summary>
    /// Destroy the Magnet Game Object if it go over the max amount of item allowed by the pool.
    /// </summary>
    /// <param name="magnet"></param>
    private void OnDestroyMagnet(Magnet magnet)
    {
        Destroy(magnet);
    }

    #endregion

    //Contains all of the function needed for the Heal Pool
    #region Heal_Pool

    /// <summary>
    /// Create a Heal game object and add it to the pool
    /// </summary>
    /// <returns></returns>
    private Heal CreateHeal()
    {
        Heal heal = Instantiate(healPrefab, transform).GetComponent<Heal>();

        return heal;
    }

    /// <summary>
    /// Take a Heal Game object from the pool
    /// </summary>
    /// <param name="heal"></param>
    private void OnTakeHealFromPool(Heal heal)
    {
        heal.gameObject.SetActive(true);
    }

    /// <summary>
    /// Return the Heal game object to the pool
    /// </summary>
    /// <param name="heal"></param>
    private void OnReturnHealToPool(Heal heal)
    {
        heal.gameObject.SetActive(false);
    }

    /// <summary>
    /// Destroy the Heal Game Object if it go over the max amount of item allowed by the pool.
    /// </summary>
    /// <param name="heal"></param>
    private void OnDestroyHeal(Heal heal)
    {
        Destroy(heal);
    }

    #endregion
    
}
