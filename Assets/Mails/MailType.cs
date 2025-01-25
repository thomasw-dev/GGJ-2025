public class MailType
{
    public enum Colors
    {
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        Purple,
        None
    }

    public Colors Merge(Colors c1, Colors c2)
    {
        if ((c1 == Colors.Red && c2 == Colors.Yellow) || (c2 == Colors.Red && c1 == Colors.Yellow))
            return Colors.Orange;

        if ((c1 == Colors.Red && c2 == Colors.Blue) || (c2 == Colors.Red && c1 == Colors.Blue))
            return Colors.Purple;

        if ((c1 == Colors.Yellow && c2 == Colors.Blue) || (c2 == Colors.Yellow && c1 == Colors.Blue))
            return Colors.Green;

        return Colors.None;
    }
}
