// TODO: declare a constant to represent the max size of the values
// and dates arrays. The arrays must be large enough to store
// values for an entire month.
const int MAX = 31;
int logicalSize = 0;

// TODO: create a double array named 'values', use the max size constant you declared
// above to specify the physical size of the array.
double[] values = new double[MAX];

// TODO: create a string array named 'dates', use the max size constant you declared
// above to specify the physical size of the array.
string[] dates = new string[MAX];

bool goAgain = true;
while (goAgain)
{
    try
    {
        DisplayMainMenu();
        string mainMenuChoice = Prompt("\nEnter a Main Menu Choice: ").ToUpper();
        if (mainMenuChoice == "L")
            logicalSize = LoadFileValuesToMemory(dates, values);
        if (mainMenuChoice == "S")
            SaveMemoryValuesToFile(dates, values, logicalSize);
        if (mainMenuChoice == "D")
            DisplayMemoryValues(dates, values, logicalSize);
        if (mainMenuChoice == "A")
            logicalSize = AddMemoryValues(dates, values, logicalSize);
        if (mainMenuChoice == "E")
            EditMemoryValues(dates, values, logicalSize);
        if (mainMenuChoice == "Q")
        {
            goAgain = false;
            throw new Exception("Bye, hope to see you again.");
        }
        if (mainMenuChoice == "R")
        {
            while (true)
            {
                if (logicalSize == 0)
                    throw new Exception("No entries loaded. Please load a file into memory");
                DisplayAnalysisMenu();
                string analysisMenuChoice = Prompt("\nEnter an Analysis Menu Choice: ").ToUpper();
                if (analysisMenuChoice == "A")
                    FindAverageOfValuesInMemory(values, logicalSize);
                if (analysisMenuChoice == "H")
                    FindHighestValueInMemory(values, logicalSize);
                if (analysisMenuChoice == "L")
                    FindLowestValueInMemory(values, logicalSize);
                if (analysisMenuChoice == "G")
                    GraphValuesInMemory(dates, values, logicalSize);
                if (analysisMenuChoice == "R")
                    throw new Exception("Returning to Main Menu");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{ex.Message}");
    }
}

void DisplayMainMenu()
{
    Console.WriteLine("\nMain Menu");
    Console.WriteLine("L) Load Values from File to Memory");
    Console.WriteLine("S) Save Values from Memory to File");
    Console.WriteLine("D) Display Values in Memory");
    Console.WriteLine("A) Add Value in Memory");
    Console.WriteLine("E) Edit Value in Memory");
    Console.WriteLine("R) Analysis Menu");
    Console.WriteLine("Q) Quit");
}

void DisplayAnalysisMenu()
{
    Console.WriteLine("\nAnalysis Menu");
    Console.WriteLine("A) Find Average of Values in Memory");
    Console.WriteLine("H) Find Highest Value in Memory");
    Console.WriteLine("L) Find Lowest Value in Memory");
    Console.WriteLine("G) Graph Values in Memory");
    Console.WriteLine("R) Return to Main Menu");
}

string Prompt(string prompt)
{
    string response = "";
    Console.Write(prompt);
    response = Console.ReadLine();
    return response;
}

string GetFileName()
{
    string fileName = "";
    do
    {
        fileName = Prompt("Enter file name including .csv or .txt: ");
    } while (string.IsNullOrWhiteSpace(fileName));
    return fileName;
}

int LoadFileValuesToMemory(string[] dates, double[] values)
{
    string fileName = GetFileName();
    int logicalSize = 0;
    string filePath = $"./data/{fileName}";
    if (!File.Exists(filePath))
        throw new Exception($"The file {fileName} does not exist.");
    string[] csvFileInput = File.ReadAllLines(filePath);for (int i = 0; i < csvFileInput.Length; i++)
    {
        string[] items = csvFileInput[i].Split(',');
        if (i != 0)
        {
            dates[logicalSize] = items[0];
            values[logicalSize] = double.Parse(items[1]);
            logicalSize++;
        }
    }
    Console.WriteLine($"Load complete. {fileName} has {logicalSize} data entries");
    return logicalSize;
}

void DisplayMemoryValues(string[] dates, double[] values, int logicalSize)
{
    if (logicalSize == 0)
        throw new Exception($"No Entries loaded. Please load a file to memory or add a value in memory");
    Console.WriteLine($"\nCurrent Loaded Entries: {logicalSize}");
    Console.WriteLine($"   Date     Value");
    for (int i = 0; i < logicalSize; i++)
        Console.WriteLine($"{dates[i]}   {values[i]}");
}

double FindHighestValueInMemory(double[] values, int logicalSize)
{
    double highest = values[0];
    for (int i = 1; i < logicalSize; i++)
    {
        if (values[i] > highest)
            highest = values[i];
    }
    return highest;
}

double FindLowestValueInMemory(double[] values, int logicalSize)
{
    double lowest = values[0];
    for (int i = 1; i < logicalSize; i++)
    {
        if (values[i] < lowest)
            lowest = values[i];
    }
    return lowest;
}

void FindAverageOfValuesInMemory(double[] values, int logicalSize)
{
    double average = 0;
    for (int i = 0; i < logicalSize; i++)
    {
        average += values[i];
    }
    Console.WriteLine($"The average value is: {average/logicalSize}");
}

void SaveMemoryValuesToFile(string[] dates, double[] values, int logicalSize)
{
    string fileName = GetFileName();
    string filePath = $"./data/{fileName}";
    using (StreamWriter writer = new StreamWriter(filePath))
    {
        for (int i = 0; i < logicalSize; i++)
        {
            writer.WriteLine($"{dates[i]},{values[i]}");
        }
    }
    Console.WriteLine($"File saved to {fileName}");
}

int AddMemoryValues(string[] dates, double[] values, int logicalSize)
{
    if (logicalSize == MAX)
    {
        throw new Exception("Memory is full");
    }
    Console.WriteLine("Enter the date:");
    string date = Console.ReadLine();
    Console.WriteLine("Enter the value:");
    double value = double.Parse(Console.ReadLine());
    dates[logicalSize] = date;
    values[logicalSize] = value;
    logicalSize++;
    return logicalSize;
}

void EditMemoryValues(string[] dates, double[] values, int logicalSize)
{
    Console.WriteLine("Enter the index of the value to edit:");
    int index = int.Parse(Console.ReadLine());
    Console.WriteLine("Enter the new value:");
    double newValue = double.Parse(Console.ReadLine());
    values[index] = newValue;
}

void GraphValuesInMemory(string[] dates, double[] values, int logicalSize)
{
    Console.WriteLine("Not implemented.");







}