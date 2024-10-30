namespace PasswordGenerator
{
    namespace PasswordGenerator
    {
        internal class Program
        {
            class PasswordGenerator
            {
                static void Main(string[] args)
                {
                    Console.WriteLine("Welcome to the Password Generator!");

                    while (true)
                    {
                        // Get password length
                        int passwordLength = GetPasswordLength();

                        // Get character types
                        bool includeUppercase = GetCharacterType("uppercase letters (A-Z)");
                        bool includeLowercase = GetCharacterType("lowercase letters (a-z)");
                        bool includeNumbers = GetCharacterType("numbers (0-9)");
                        bool includeSpecial = GetCharacterType("special characters (!@#$%^&*)");

                        // Check that at least one character type is selected
                        if (!includeUppercase && !includeLowercase && !includeNumbers && !includeSpecial)
                        {
                            Console.WriteLine("At least one character type must be selected.");
                            continue;
                        }

                        // Generate passwords
                        int numberOfPasswords = GetNumberOfPasswords();
                        for (int i = 0; i < numberOfPasswords; i++)
                        {
                            string password = GeneratePassword(passwordLength, includeUppercase, includeLowercase, includeNumbers, includeSpecial);
                            Console.WriteLine($"Generated Password {i + 1}: {password}");
                        }

                        // Ask if the user wants to generate more passwords
                        if (!AskToContinue())
                        {
                            break;
                        }
                    }
                }

                static int GetPasswordLength()
                {
                    while (true)
                    {
                        Console.Write("Enter the desired password length (minimum 4): ");
                        if (int.TryParse(Console.ReadLine(), out int length) && length >= 4)
                        {
                            return length;
                        }
                        Console.WriteLine("Invalid input. Please enter a number greater than or equal to 4.");
                    }
                }

                static bool GetCharacterType(string characterType)
                {
                    Console.Write($"Include {characterType}? (y/n): ");
                    return Console.ReadLine()?.ToLower() == "y";
                }

                static int GetNumberOfPasswords()
                {
                    while (true)
                    {
                        Console.Write("How many passwords would you like to generate? ");
                        if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
                        {
                            return count;
                        }
                        Console.WriteLine("Invalid input. Please enter a positive number.");
                    }
                }

                static bool AskToContinue()
                {
                    Console.WriteLine("Do you want to generate more passwords? (y/n)");
                    return Console.ReadLine()?.ToLower() == "y";
                }

                static string GeneratePassword(int length, bool includeUppercase, bool includeLowercase, bool includeNumbers, bool includeSpecial)
                {
                    const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
                    const string numberChars = "0123456789";
                    const string specialChars = "!@#$%^&*";

                    // Build character set based on user selection
                    string characterSet = "";
                    if (includeUppercase) characterSet += upperChars;
                    if (includeLowercase) characterSet += lowerChars;
                    if (includeNumbers) characterSet += numberChars;
                    if (includeSpecial) characterSet += specialChars;

                    // Check if the character set is empty
                    if (string.IsNullOrEmpty(characterSet))
                    {
                        Console.WriteLine("Error: No characters available for password generation.");
                        return string.Empty;
                    }

                    Random random = new Random();
                    char[] password = new char[length];

                    // Ensure at least one character from each selected type is included
                    int index = 0;
                    if (includeUppercase) password[index++] = upperChars[random.Next(upperChars.Length)];
                    if (includeLowercase) password[index++] = lowerChars[random.Next(lowerChars.Length)];
                    if (includeNumbers) password[index++] = numberChars[random.Next(numberChars.Length)];
                    if (includeSpecial) password[index++] = specialChars[random.Next(specialChars.Length)];

                    // Fill the rest of the password with random characters from the character set
                    for (int i = index; i < length; i++)
                    {
                        password[i] = characterSet[random.Next(characterSet.Length)];
                    }

                    // Shuffle the password to ensure randomness
                    Shuffle(password, random);
                    return new string(password);
                }

                static void Shuffle(char[] array, Random random)
                {
                    for (int i = array.Length - 1; i > 0; i--)
                    {
                        int j = random.Next(i + 1);
                        (array[i], array[j]) = (array[j], array[i]); // Swap
                    }
                }
            }
        }
    }

}


