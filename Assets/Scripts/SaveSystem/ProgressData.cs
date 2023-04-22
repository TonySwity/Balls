[System.Serializable]
public class ProgressData
{
    public int Coins;
    public int Level;
    //Color можно преобразовать в массив float[]
    public float[] BackgroundColor;


    public ProgressData(ProgressGame progressGame)
    {
        Coins = progressGame.Coins;
        Level = progressGame.Level;
        BackgroundColor = new float[4];
        BackgroundColor[0] = progressGame.BackgroundColor.r;
        BackgroundColor[1] = progressGame.BackgroundColor.g;
        BackgroundColor[2] = progressGame.BackgroundColor.b;
        BackgroundColor[3] = progressGame.BackgroundColor.a;
    }

}
