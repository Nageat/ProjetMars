using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SolarEnergy : MonoBehaviour
{
    public int MaxEnergy;
    public int CurrentEnergy;
    public enum PlayerStat {Light, Shadow }
    PlayerStat PlayerIsIn;
    public Slider EnergySlider;
    public controller controller;

    // Start is called before the first frame update

    private void Start()
    {
        CurrentEnergy = MaxEnergy;
        InvokeRepeating("Check", 1.0f, 1.0f);

    }

    private void Update()
    {
        if (CurrentEnergy > MaxEnergy)
        {
            CurrentEnergy = MaxEnergy;
        }
        if (CurrentEnergy < 1)
        {
            Debug.Log("Plus d'énergie");
        }
        EnergySlider.value = CurrentEnergy;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Shadow")
        {
            PlayerIsIn = PlayerStat.Shadow;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Shadow")
        {
            PlayerIsIn = PlayerStat.Light;
        }
    }
    void Check()
    {
        if (PlayerIsIn == PlayerStat.Light)
        {
            CurrentEnergy += 5;
        }
        else if (PlayerIsIn == PlayerStat.Shadow && controller.CurentView == controller.Cam.TPS)
        {
            CurrentEnergy -= 2;
        }
    }


}
