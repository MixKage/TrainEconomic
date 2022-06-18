namespace TrainEconomic;

public class Main
{
    private int creadit = 0;
    private int stepsCount = 0;
    private int 
    private List<valuta> valutes = new List<valuta>();

    public Main()
    {
        StartGame();

    }

    private void Step()
    {
        Random random = new Random();

        foreach (var val in valutes) { val.value = random.Next(-5, 5); }
        stepsCount++;

        if (stepsCount % 3 == 0)
        {

        }

    }

    private void StartGame()
    {
        valutes.Add(new valuta("Coin 1"));
        valutes.Add(new valuta("Coin 2"));
        valutes.Add(new valuta("Coin 3"));
    }
}