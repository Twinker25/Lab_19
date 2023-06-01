using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab_19
{
    [DataContract]
    public class Music
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Artist { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public int Duration { get; set; }

        [DataMember]
        public string Studio { get; set; }
    }

    internal class Program
    {
        static bool IsPrime(int number)
        {
            if (number < 2)
                return false;

            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }

        static bool IsFibonacci(int number)
        {
            int a = 0;
            int b = 1;

            while (b < number)
            {
                int temp = b;
                b = a + b;
                a = temp;
            }

            return b == number;
        }

        static void Main(string[] args)
        {
            while(true)
            {
                Console.Write("Enter task (1 - 2): ");
                int ch = int.Parse(Console.ReadLine());
                switch(ch)
                {
                    case 1:
                        Console.Write("Enter size of array: ");
                        int size = int.Parse(Console.ReadLine());
                        int[] arr = new int[size];
                        for(int i = 0; i < arr.Length; i++)
                        {
                            Console.Write("Enter number to array: ");
                            arr[i] = int.Parse(Console.ReadLine());
                        }
                        Console.WriteLine();
                        Console.Write("Array: ");
                        for (int i = 0; i < arr.Length; i++)
                        {
                            Console.Write(arr[i] + ", ");
                        }
                        Console.WriteLine("\nSort the array:\n1. Remove prime numbers\n2. Remove Fibonacci numbers");
                        int option = int.Parse(Console.ReadLine());

                        int[] sortArr;
                        int sortArrSize = 0;

                        switch (option)
                        {
                            case 1:
                                for (int i = 0; i < arr.Length; i++)
                                {
                                    if (!IsPrime(arr[i]))
                                    {
                                        sortArrSize++;
                                    }
                                }

                                sortArr = new int[sortArrSize];
                                int index = 0;

                                for (int i = 0; i < arr.Length; i++)
                                {
                                    if (!IsPrime(arr[i]))
                                    {
                                        sortArr[index] = arr[i];
                                        index++;
                                    }
                                }

                                break;
                            case 2:
                                for (int i = 0; i < arr.Length; i++)
                                {
                                    if (!IsFibonacci(arr[i]))
                                    {
                                        sortArrSize++;
                                    }
                                }

                                sortArr = new int[sortArrSize];
                                index = 0;

                                for (int i = 0; i < arr.Length; i++)
                                {
                                    if (!IsFibonacci(arr[i]))
                                    {
                                        sortArr[index] = arr[i];
                                        index++;
                                    }
                                }
                                break;
                            default:
                                Console.WriteLine("Error!");
                                return;
                        }

                        Console.Write("Sorted array: ");
                        for (int i = 0; i < sortArr.Length; i++)
                        {
                            Console.Write(sortArr[i] + ", ");
                        }

                        string path = @"D:\Games\Visual Studio Project\Lab_19\Lab_19\text.dat";
                        using (FileStream fs = new FileStream(path, FileMode.Create))
                        {
                            DataContractSerializer dcs = new DataContractSerializer(typeof(int[]));
                            dcs.WriteObject(fs, sortArr);
                        }
                        Console.WriteLine("\nArray serialized");

                        int[] loadedArray;

                        using (FileStream fs = new FileStream(path, FileMode.Open))
                        {
                            DataContractSerializer dcs = new DataContractSerializer(typeof(int[]));
                            loadedArray = (int[])dcs.ReadObject(fs);
                        }

                        Console.Write("Loaded array from file: ");
                        for (int i = 0; i < loadedArray.Length; i++)
                        {
                            Console.Write(loadedArray[i] + ", ");
                        }
                        Console.WriteLine();
                        break;
                    case 2:
                        string musicFile = @"D:\Games\Visual Studio Project\Lab_19\Lab_19\music.dat";

                        Console.WriteLine("Enter album details:");
                        Console.Write("Title: ");
                        string title = Console.ReadLine();
                        Console.Write("Artist: ");
                        string artist = Console.ReadLine();
                        Console.Write("Year: ");
                        int year = int.Parse(Console.ReadLine());
                        Console.Write("Duration (in minutes): ");
                        int duration = int.Parse(Console.ReadLine());
                        Console.Write("Studio: ");
                        string studio = Console.ReadLine();

                        Music music = new Music()
                        {
                            Title = title,
                            Artist = artist,
                            Year = year,
                            Duration = duration,
                            Studio = studio,
                        };

                        using (FileStream fs = new FileStream(musicFile, FileMode.OpenOrCreate))
                        {
                            DataContractSerializer dcs = new DataContractSerializer(typeof(Music));
                            dcs.WriteObject(fs, music);
                        }
                        Console.WriteLine("Album serialized");

                        using (FileStream fs = new FileStream(musicFile, FileMode.OpenOrCreate))
                        {
                            DataContractSerializer dcs = new DataContractSerializer(typeof(Music));
                            music = (Music)dcs.ReadObject(fs);
                        }

                        Console.WriteLine($"Information about album of music:\nTitle: {music.Title}\nAuthor: {music.Artist}\nYear: {music.Year}\nDuration: {music.Duration}\nStudio: {music.Studio}");
                        break;
                    default: Console.WriteLine("Error!"); break;
                }
            }
        }
    }
}