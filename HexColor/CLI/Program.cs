Console.WriteLine("Enter RGB values (0-255):");

Console.Write("Red: ");
byte red = GetRGBInput();

Console.Write("Green: ");
byte green = GetRGBInput();

Console.Write("Blue: ");
byte blue = GetRGBInput();

Console.WriteLine($"\nYou entered: R={red}, G={green}, B={blue}");
Console.WriteLine($"Hexadecimal color: {GetHexFromRGB(red, green, blue)}");

byte GetRGBInput()
{
    while (true)
    {
        string input = Console.ReadLine();
        if (Byte.TryParse(input, out byte value)) return value;
        Console.Write($"RGB input must be between {Byte.MinValue} and {Byte.MaxValue}");
    }
}
string GetHexFromRGB(byte red, byte green, byte blue) => $"#{red:X2}{green:X2}{blue:X2}";
