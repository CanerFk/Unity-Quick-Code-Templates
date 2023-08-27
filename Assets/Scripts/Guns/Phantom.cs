using UnityEngine;

class Phantom : Gun
{
    public string name { get; private set; } = "Phantom";
    public int maxMagazineAmmo { get; private set; } = 30;
    public int maxAmmo { get; private set; } = 90;
    public float fireRatePerSecond { get; private set; } = 11f;
    public float fallOfDistance { get; private set; } = 140f;
    public float recoilResetTimeSeconds { get; private set; } = 0.35f;
    public Vector3[] recoilPattern { get; private set; } = new Vector3[25]
    {
        new Vector3(-0.5f,0,0),
        new Vector3(-0.5f,0,0),
        new Vector3(-0.8f,0,0),
        new Vector3(-0.8f,0,0),
        new Vector3(-0.8f,0,0),
        new Vector3(-0.8f,0,0),
        new Vector3(-0.8f,0,0),
        new Vector3(-0.8f,0,0),
        new Vector3(-0.8f,0,0),
        new Vector3(-0.8f,0,0),
        new Vector3(-0.8f,0,0),
        new Vector3(-0.8f,0,0),
        new Vector3(0,0.8f,0),
        new Vector3(0,0.8f,0),
        new Vector3(0,0.8f,0),
        new Vector3(0,0.8f,0),
        new Vector3(0,-0.8f,0),
        new Vector3(0,-0.8f,0),
        new Vector3(0,-0.8f,0),
        new Vector3(0,-0.8f,0),
        new Vector3(0,-0.8f,0),
        new Vector3(0,-0.8f,0),
        new Vector3(0,-0.8f,0),
        new Vector3(0,-0.8f,0),
        new Vector3(0,-0.8f,0),
    };

}
