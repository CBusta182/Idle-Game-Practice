using UnityEngine;
using UnityEngine.UI; 

public class IdleTutorialGame : MonoBehaviour
{
    public Text coinText;
    public Text clickValueText; 
    public double coins;
    //we create a variable here so it is easier to manipulate 
    public double coinsClickValue; 

    public Text coinsPerSecondText;
    public Text clickBtn1Text;
    public Text clickBtn2Text; 
    public Text productionUpgradeText;
    public Text productionUpgrade2Text;

    public double coinsPerSecond;
    public double clickUpgrade1Cost;
    public int clickUpgrade1Level;

    public double productionUpgradeCost;
    public int productionUpgradeLevel;

    public double clickUpgrade2Cost;
    public int clickUpgrade2Level;

    public double productionUpgrade2Cost;
    public double productionUpgrade2Power;
    public int productionUpgrade2Level;

    public Text gemText;
    public Text gemBoostText;
    public Text gemsToGetText;

    public double gems; 
    public double gemBoost;
    public double gemsToGet;

    public Image ClickProgressBar;
    public GameObject Panel; 

    //this method will load once the app is opened 
    public void Start()
    {
        Load();
    }

    public void Load()
    {
        /*we need to call the playerprefs.GetString(variable, default) metod in order to load up the last state the game was in.
        note we can only save int, floats, and strings
        this is why we use double.Parse() this converts doubles into strings*/
        coins = double.Parse(PlayerPrefs.GetString("coins", "0"));
        coinsClickValue = double.Parse(PlayerPrefs.GetString("coinsClickValue", "1"));
        clickUpgrade1Cost = double.Parse(PlayerPrefs.GetString("clickUpgrade1Cost", "10"));
        clickUpgrade2Cost = double.Parse(PlayerPrefs.GetString("clickUpgrade2Cost", "100"));
        productionUpgradeCost = double.Parse(PlayerPrefs.GetString("productionUpgradeCost", "25"));
        productionUpgrade2Cost = double.Parse(PlayerPrefs.GetString("productionUpgrade2Cost", "250"));
        productionUpgrade2Power = double.Parse(PlayerPrefs.GetString("productionUpgrade2Power", "5"));
        gems = double.Parse(PlayerPrefs.GetString("gems", "0"));
        gemBoost = double.Parse(PlayerPrefs.GetString("gemBoost", "1.00"));

        clickUpgrade1Level = PlayerPrefs.GetInt("clickUpgrade1Level", 0);
        clickUpgrade2Level = PlayerPrefs.GetInt("clickUpgrade2Level", 0);
        productionUpgradeLevel = PlayerPrefs.GetInt("productionUpgradeLevel", 0);
        productionUpgrade2Level = PlayerPrefs.GetInt("productionUpgrade2Level", 0);
        
    }

    public void Save()
    {
        PlayerPrefs.SetString("coins", coins.ToString());
        PlayerPrefs.SetString("coinsClickValue", coinsClickValue.ToString());
        PlayerPrefs.SetString("clickUpgrade1Cost", clickUpgrade1Cost.ToString());
        PlayerPrefs.SetString("clickUpgrade2Cost", clickUpgrade2Cost.ToString());
        PlayerPrefs.SetString("productionUpgradeCost", productionUpgradeCost.ToString());
        PlayerPrefs.SetString("productionUpgrade2Cost", productionUpgrade2Cost.ToString());
        PlayerPrefs.SetString("productionUpgrade2Power", productionUpgrade2Power.ToString());
        PlayerPrefs.SetString("gems", gems.ToString());
        PlayerPrefs.SetString("gemBoost", gemBoost.ToString()); 

        PlayerPrefs.SetInt("clickUpgrade1Level", clickUpgrade1Level);
        PlayerPrefs.SetInt("clickUpgrade2Level", clickUpgrade2Level);
        PlayerPrefs.SetInt("productionUpgradeLevel", productionUpgradeLevel);
        PlayerPrefs.SetInt("productionUpgrade2Level", productionUpgrade2Level);
    }

    public void Update()
    {
        /*this is the equation for how many gems you can get per prestige 
         we are using the square root function so the growth of this will not be liner. 
        if it was linear then you would advance in the game too quickly*/
        gemsToGet = (150 * System.Math.Sqrt(coins / 1e7));
        //this means that each gem is a 5% boost
        gemBoost = (gems * 0.05) + 1;
        //we are using the System.Math.Ceiling method to round down to the nearest whole number
        gemsToGetText.text = "Prestige:\n+" + System.Math.Floor(gemsToGet) + " Gems";
        gemText.text = "Gems: " + System.Math.Floor(gems);
        gemBoostText.text = gemBoost.ToString("F2") + "x boost";

        /*if you have other upgrades you can just add it onto the this variable. 
        if you have an upgrade that upgrades the coinspersecond by 10, then just do productionupgradelevel*10*/
        coinsPerSecond = (productionUpgradeLevel + (productionUpgrade2Power * productionUpgrade2Level)) * gemBoost;
        coinsPerSecondText.text = coinsPerSecond.ToString("F0") + " Coins/s";

        //this will make everything into an exponenet
        if(coinsClickValue > 1000)
        {
            //we use System.math so we can keep all variables casted as a double instead of a float
            //the exponent is found by looking for the number of digits there are minus one (e^04 is the exponent) 
            var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(coinsClickValue))));
            //the matissa is found by getting the first few digits and dividing by how many decimals (aka 10^exponent) you want [1.23 is the mantissa] 
            var mantissa = (coinsClickValue / System.Math.Pow(10, exponent));
            clickValueText.text = "Click\n+" + mantissa.ToString("F2") + "e" + exponent + " Coins";
        } else
            clickValueText.text = "Click\n+" + coinsClickValue.ToString("F0") + " Coins";


        /*we use the ToString("F0") method so that the text will round to the nearest whole number.
         if we wanted to have more decimal places we can do something like F3*/
        if (coins > 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(coins))));
            var mantissa = (coins / System.Math.Pow(10, exponent));
            coinText.text = "Coins: " + mantissa.ToString("F2") + "e" + exponent;
        }
        else
            coinText.text = "Coins: " + coins.ToString("F0");

        string ClickUpgrade1CostString;
        if (clickUpgrade1Cost > 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(clickUpgrade1Cost))));
            var mantissa = (clickUpgrade1Cost / System.Math.Pow(10, exponent));
            ClickUpgrade1CostString = "Coins: " + mantissa.ToString("F2") + "e" + exponent;
        }
        else
            ClickUpgrade1CostString = clickUpgrade1Cost.ToString("F0");

        string ClickUpgrade1LevelString;
        if (clickUpgrade1Level > 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(clickUpgrade1Level))));
            var mantissa = (clickUpgrade1Level / System.Math.Pow(10, exponent));
            ClickUpgrade1LevelString = "Coins: " + mantissa.ToString("F2") + "e" + exponent;
        }
        else
            ClickUpgrade1LevelString = "Coins: " + clickUpgrade1Level.ToString("F0");

        clickBtn1Text.text = "Click Upgrade 1 \nCost: " + ClickUpgrade1CostString + " coins\nPower: +1 Click\n" + "Level: " + ClickUpgrade1LevelString;
        clickBtn2Text.text = "Click Upgrade 2 \nCost: " + clickUpgrade2Cost.ToString("F0") + " coins\nPower: +5 Click\n" + "Level: " + clickUpgrade2Level;

        productionUpgradeText.text = "Production Upgrade 1 \nCost: " + productionUpgradeCost.ToString("F0") + 
            " coins/s\nPower:" + gemBoost.ToString("F2") + "Coins/s\nLevel: " + productionUpgradeLevel;
        productionUpgrade2Text.text = "Production Upgrade 1 \nCost: " + productionUpgrade2Cost.ToString("F0") +
            " coins/s\nPower: +" + (productionUpgrade2Power * gemBoost).ToString("F2") + "Coins/s\nLevel:" + productionUpgrade2Level;

        /*time.deltaTime is a method that returns the time in miliseconds between the previous and the current frame*/
        coins += coinsPerSecond * Time.deltaTime;
       
        if (coins / clickUpgrade1Cost < 0.01)
        {
            ClickProgressBar.fillAmount = 0;
        }
        else if (coins / clickUpgrade1Cost > 1)
        {
            ClickProgressBar.fillAmount = 1;
        }
        else
            //note that the fill amount only uses floats
            ClickProgressBar.fillAmount = (float)(coins / clickUpgrade1Cost);

        Save();

    }

    public void Prestige()
    {
        if(coins> 1000)
        {
            coins = 0;
            coinsClickValue = 1;
            clickUpgrade1Cost = 10;
            clickUpgrade2Cost = 100;
            productionUpgradeCost = 25;
            productionUpgrade2Cost = 250;
            productionUpgrade2Power = 5;

            clickUpgrade1Level = 0;
            clickUpgrade2Level = 0;
            productionUpgradeLevel = 0;
            productionUpgrade2Level = 0;

            gems += gemsToGet;
        }
    }

    //Buttons
    public void Click()
    {
        //we use plus equals to 1 so we can change how much the button adds by later on 
        coins += coinsClickValue; 
    }

    public void BuyClickUpgrade1()
    {
        if(coins >= clickUpgrade1Cost)
        {
            clickUpgrade1Level++;
            coins -= clickUpgrade1Cost;
            //remember this is a percentage increase. 1.07 = 7% increase in price
            clickUpgrade1Cost *= 1.07;
            coinsClickValue++;
        }

    }
    public void BuyClickUpgrade2()
    {
        if (coins >= clickUpgrade2Cost)
        {
            clickUpgrade2Level++;
            coins -= clickUpgrade2Cost;
            //remember this is a percentage increase. 1.07 = 7% increase in price
            clickUpgrade2Cost *= 1.1;
            coinsClickValue += 5;
        }

    }
    public void BuyProductionUpgrade1()
    {
        if (coins >= productionUpgradeCost)
        {
            productionUpgradeLevel++;
            coins -= productionUpgradeCost;
            //remember this is a percentage increase. 1.07 = 7% increase in price
            productionUpgradeCost *= 1.07;

        }

    }
    public void BuyProduction2Upgrade1()
    {
        if (coins >= productionUpgrade2Cost)
        {
            productionUpgrade2Level++;
            coins -= productionUpgrade2Cost;
            //remember this is a percentage increase. 1.07 = 7% increase in price
            productionUpgrade2Cost *= 1.1;

        }

    }

    //this method is good for cloning things of the same exact object such as chickens from egg inc
    //public void AddObject()
    // {
    //    Instantiate(Shells, Vector3.zero, Quaternion.identity);
    // }

    public void openPanel()
    {
        //check if the panel is assigned
        if(Panel != null)
        {
            Panel.SetActive(true);
        }
    }
}
