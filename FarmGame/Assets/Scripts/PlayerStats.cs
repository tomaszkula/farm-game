using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static int Money;
    public int startMoney = 3000;

    public static int FarmSize = 10*4;

    private void Start()
    {
        Money = startMoney;
    }
}
