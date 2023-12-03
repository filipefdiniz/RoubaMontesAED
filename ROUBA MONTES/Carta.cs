class Carta
{
    public string Valor;
    public char Naipe;
    public Carta(string valor, char naipe)
    {
        this.Valor = valor;
        this.Naipe = naipe;

    }
    public Carta()
    {
        this.Valor = "default";
        this.Naipe = 'd';

    }
}