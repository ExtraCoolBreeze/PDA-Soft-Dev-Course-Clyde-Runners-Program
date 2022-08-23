//Craig McMillan
//06/06/2022
//Glasgow Runners Club Final Project - Fully integrated system


using System;
using System.IO; 

namespace Craig_McMillan___Glasgow_Runners_Club_Final_Project
{
    class Program
    {
        static void get_userdetails(ref string username, ref string login)
        //Gets user details. Asking for username and password and setting to a variable
        {
            Console.WriteLine("Please enter username: ");
            username = Console.ReadLine();
            Console.WriteLine("Please enter password: ");
            login = Console.ReadLine();

        }

        static void print_message(string password, string login, ref int count)
        {
            //Validating password and then printing the main menu
            if (login == password)
            {

                Console.WriteLine();
                Console.WriteLine("\t\t\t MAIN MENU");
                Console.WriteLine("\t\t\t 1. Read and display file");
                Console.WriteLine("\t\t\t 2. Sort and print recorded times");
                Console.WriteLine("\t\t\t 3. Find and print the fastest time ");
                Console.WriteLine("\t\t\t 4. Find and print the slowest time ");
                Console.WriteLine("\t\t\t 5. Search ");
                Console.WriteLine("\t\t\t 6. Time occurrence ");
                Console.WriteLine("\t\t\t 7. Exit Program ");

            }
            else
            {

                count += 1;   //count password attempts
                Console.WriteLine($"Invalid password and username - attempt {count}");
            }
        }

        static int get_choice(ref int choice)
        {
            while (choice < 1 | choice > 7) // printing the choices, asking for user input and checking if in choice range
            {
                Console.WriteLine("\t\t\t ");
                Console.WriteLine("\t\t Enter a Choice (1,2,3,4,5,6 or 7)\n");
                choice = Convert.ToInt32(Console.ReadLine());
            }
            return choice;
        }

        static void act_on_choice(int choice)
        {
            int[] racetimes = { 70, 90, 75, 70, 95, 103, 80, 110, 68, 120, 80, 140, 90, 72, 78, 97 };
            int[] sortedarray = { 68, 70, 70, 72, 75, 78, 80, 80, 90, 90, 95, 97, 103, 110, 120, 140 };
            //Takes user input from main menu choice and selects correct choice
            switch (choice)
            {
                case 1: //prints message and calls readfromfile function before breaking out of case
                    {
                        Console.WriteLine("You have selected read and display file");
                        ReadFromFile();
                        Console.WriteLine("Press any key to continue....");

                        Console.ReadKey();
                        break;
                        // end case 1
                    }
                case 2: //prints message and calls sortarray function and passes in unsorted array, write the sorted array to file before breaking out of case
                    {
                        Console.WriteLine("You have selected Sort and print recorded times");
                        SortArray(racetimes);
                        writeToFile(sortedarray);
                        Console.WriteLine("Press any key to continue....");
                        Console.ReadKey();
                        break;
                    }//end case 2
                case 3: //prints message, calls get_min function and passes in unsorted array before breaking out of case
                    {
                        Console.WriteLine("You have selected find and print the fastest time ");
                        get_min(racetimes);
                        Console.WriteLine("Press any key to continue....");
                        Console.ReadKey();
                        break;
                    }//end case 3
                case 4: //prints message, calls get_max function and passes in unsorted array before breaking out of case
                    {
                        Console.WriteLine("You have selected find and print the slowest time");
                        get_max(racetimes);
                        Console.WriteLine("Press any key to continue....");
                        Console.ReadKey();
                        break;
                    }//end case 4
                case 5: //prints message, calls search function and passes in sorted array, get output and sets to variable then prints message before breaking out of case
                    {
                        Console.WriteLine("You have selected search");
                        int result = SearchArray(sortedarray);
                        Console.WriteLine($"The position of the number is: {result}");
                        Console.WriteLine("Press any key to continue....");
                        Console.ReadKey();
                        break;
                    }//end case 5
                case 6: //prints message, calls countoccurrences function and passes in unsorted array, get output and sets to variable then prints message before breaking out of case
                    {
                        Console.WriteLine("You have selected time occurrence");
                        int count = countOccurrences(racetimes);
                        Console.WriteLine($"The number has occurred: {count} times ");
                        Console.WriteLine("Press any key to continue....");
                        Console.ReadKey();
                        break;
                    }//end case 6
                case 7: // exits out of program
                    {
                        Console.WriteLine("You have selected exit program");
                        Console.WriteLine("Press any key to continue....");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;
                    }//end case 7
            }//end case statement
        }//end act on choice method


        static void SortArray(int[] racetimes) // sort array loops for size of array, If first number > than being compared, swap out each in order from smallest to largest and print results
        {

            for (int i = 0; i < racetimes.Length - 1; i++)     //move boundary of unsorted array (take 1 away each time).The first array element with the new sorted number will be ignored (hence -1) 
            {                                            
                int index = i;
                for (int j = i + 1; j < racetimes.Length; j++)   //find minimum element in unsorted array then looks at the index and goes through each susequent index in the array to compare
                {                                       
                    if (racetimes[j] < racetimes[index])
                    {
                        index = j;//searching for minimum element  
                    }
                }
                int smallerNumber = racetimes[index]; //swaps minimum element with first element
                racetimes[index] = racetimes[i];
                racetimes[i] = smallerNumber;
            }

            for (int i = 0; i < racetimes.Length; i++)
            {
                Console.WriteLine(racetimes[i]);
            }
        }

        static void get_min(int[] racetimes) //loop for size of array, set min to variable to first element in array, compare each element to min variable, if value < than min variable, set value to min variable
        {

            int seconds;

            seconds = racetimes[0]; //set min to first element in array
            for (int i = 1; i <= racetimes.Length; i++)
            {
                if (racetimes[i] < seconds)
                {
                    seconds = racetimes[i];
                }
            }

            Console.WriteLine("The fastest time is : " + seconds);
        }

        static void get_max(int[] racetimes) //loop for size of array, set max to variable to first element in array, compare each element to max variable, if value > than max variable, set value to max variable
        {
            int max = 0;

            max = racetimes[0]; //set max to first element in array
            for (int i = 1; i < racetimes.Length; i++) //loop number of elements (start at 2nd element)
            {
                if (racetimes[i] > max)
                {
                    max = racetimes[i];
                }
            }

            Console.WriteLine("The slowest time is : " + max);
        }

         static int SearchArray(int[] sortedArray) //loop for length of array, if time specified matches value of element in array, set element position to variable and return the value
        {
            int item;
            Console.WriteLine("Enter number to search for");// 1. Get item being searched for
            item = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < sortedArray.Length; i++) //2.For each item in the array
            {
                if (item == sortedArray[i])//3.If this item matches the item being searched for
                {
                    return i;//3.1Store the position
                }//4.End if
            }//5.End loop
            return -1;
        }

        static int countOccurrences(int[] racetimes) //loop for length of array, if time specified matches value of element in array add +1 to number count, then return the number count
        {
            int item = 0;
            int count = 0;

            Console.WriteLine("Enter number to find how many times it has occurred");// 1. Get item being searched for
            item = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < racetimes.Length; i++)//2.For each item in the array
            {
                if (item == racetimes[i])//3.If this item matches the item being searched for
                {
                    count = count + 1;//3.1 add one to the count
                }//4.End if
            }//5.End loop
            return count;//6.Print out the number of times the item was found

            //end of count function

        }


        static void ReadFromFile() // opens the text file, creates empty 2d array, spits up the text string and places it into collums and rows of 2d array.
        //reads from text file into a 2D array
        {
            string[,] person = new string[16, 2];
            StreamReader sr = File.OpenText("C:\\Users\\Craig\\Desktop\\Program text file\\race results.txt");

            string s;  //variable that takes each line of text from file
            string[] textSplit = { " ", " " };  //create an array that will split a string up
            int i = 0;  //loop counter for each row of the 2D array
            while ((s = sr.ReadLine()) != null)
            {
                int t = s.LastIndexOf(' '); //returns the position in the string of the last space
                                            //                             //e.g. Bobby Jones 800 - will return the space between Jones and 800 -pos 11
                int l = (s.Length - t);   //calculate the length of the string and take away index pos - 12 - 10
                textSplit[0] = s.Substring(0, t);   // copy up to second " " 
                textSplit[1] = s.Substring(t, l);//copy from " " to nd of string

                for (int j = 0; j < 2; j++)  //loop for each column
                {
                    person[i, j] = textSplit[j]; //for each row, name will be read and then age
                                                 //this loop witl iterate twice - 0,0 and then 0,1
                                                 //i will become 1 - 1,0 and then 1,1
                                                 //i will become 2 - 2,0 and then 2,1

                    Console.Write(person[i, j] + " ");//prints name and age and leaves a space to look nice
                }
                i++;  // add 1 onto i so that each row can be dealth with
                Console.WriteLine();  //takes a new line after each name and age are printed.
            }//end while
        }

        static void writeToFile(int[] sortedarray)//sets up StreamReader class so that we can write to a file
        {
            using (StreamWriter sw = new StreamWriter("C:\\Users\\Craig\\Desktop\\Program text file\\SortedArray.txt"))  //creates a new file object called sw and uses path to write to file
            {
                for (int i = 0; i < sortedarray.Length; i++)  //forloop to write each value in the array to file
                {
                    sw.WriteLine(sortedarray[i]);   //uses sw object to write the numbers too. sw is recognised as a file
                }
            }
        }

        static void Contloop()
        {
           // print_message(password, login, ref count); // calls pinrt_message and passes varaibles
           // int exit = get_choice(ref choice); //calls get_choice and passes variables
           // act_on_choice(choice); //calls act_on_choice and passes varaibles

        }


        static void Main(string[] args) //sets some variables, helps validate user login inputs, calls functions
        {
            string username = ""; 
            string login = "";
            string password = "clyderunners";
            int count = 0;
            int choice = 0;
            int exit = 0;
            Console.WriteLine("Welcome to Clyde Runners");
            while (login != password && count < 3) //helps validate inputs
            {
                get_userdetails(ref username, ref login); //calls get_userdetails and passes variables
                print_message(password, login, ref count); // calls pinrt_message and passes variables
                get_choice(ref choice); //calls get_choice and passes variables
                act_on_choice(choice); //calls act_on_choice and passes variablebles

                //loop until password is correct
            }//end while

            if (count == 3) //checks login attempts and shows locked out message
            {
                Console.WriteLine("Locked out after 3 goes - contact Admin");
            }
        }//end main
    }
}
