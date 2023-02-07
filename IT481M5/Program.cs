using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace IT481M4
{
    internal static class Program
    {
        static void Main()
        {
            try
            {
                // Prompt user to input the size of the dataset they want to sort
                Console.WriteLine("Enter the size of the dataset you want to sort (10, 1000, or 10000): ");
                string input = Console.ReadLine();

                // Validate user input
                if (!int.TryParse(input, out int size) || (size != 10 && size != 1000 && size != 10000))
                {
                    Console.WriteLine("Invalid input. Please enter 10, 1000, or 10000.");
                    return;
                }

                // Initialize the dataset array to null
                int[] dataset = null;

                // Determine the size of the dataset based on the user's input
                switch (size)
                {
                    case 10:
                        // Generate dataset of size 10
                        dataset = GenerateDataset(10);
                        break;
                    case 1000:
                        // Generate dataset of size 1000
                        dataset = GenerateDataset(1000);
                        break;
                    case 10000:
                        // Generate dataset of size 10000
                        dataset = GenerateDataset(10000);
                        break;
                }

                // If dataset is not null, sort and display elapsed time
                if (dataset != null)
                {
                    // Start timer to measure time taken to sort
                    var timer1 = new Stopwatch();
                    timer1.Start();

                    // Sort the dataset using bubble sort algorithm
                    BubbleSort(dataset);

                    // Stop the timer
                    timer1.Stop();

                    // Display the elapsed time
                    Console.WriteLine("Bubblesort Time elapsed: " + timer1.Elapsed);

                    // Start timer to measure time taken to sort
                    var timer2 = new Stopwatch();
                    timer2.Start();

                    // Sort the dataset using Quick sort algorithm
                    QuickSort(dataset, 0, dataset.Length - 1);

                    // Stop the timer
                    timer2.Stop();

                    // Display the elapsed time
                    Console.WriteLine("Quicksort Time elapsed: " + timer2.Elapsed);

                    var timer3 = new Stopwatch();
                    timer3.Start();

                    HeapSort(dataset);

                    timer3.Stop();
                    Console.WriteLine("HeapSort Time elapsed: " + timer3.Elapsed);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
        static void BubbleSort(int[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input", "Input array cannot be null.");
            }

            if (input.Length == 0)
            {
                throw new ArgumentException("Input array cannot be empty.", "input");
            }

            // Flag to check if any item was moved during the sort
            var itemMoved = false;

            // Repeat the sort as long as an item is moved
            do
            {
                itemMoved = false;
                // Iterate through the entire dataset
                for (int i = 0; i < input.Count() - 1; i++)
                {
                    // Compare adjacent items and swap if necessary
                    if (input[i] > input[i + 1])
                    {
                        var lowerValue = input[i + 1];
                        input[i + 1] = input[i];
                        input[i] = lowerValue;
                        itemMoved = true;
                    }
                }
            } while (itemMoved);
        }
        private static void QuickSort(int[] input, int left, int right)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input cannot be null.");
            }

            if (left < 0 || left >= input.Length)
            {
                throw new ArgumentOutOfRangeException("left index is out of range.");
            }

            if (right < 0 || right >= input.Length)
            {
                throw new ArgumentOutOfRangeException("right index is out of range.");
            }

            if (left >= right)
            {
                return;
            }

            int pivot = Partition(input, left, right);
            QuickSort(input, left, pivot - 1);
            QuickSort(input, pivot + 1, right);
        }

        private static int Partition(int[] input, int left, int right)
        {
            int pivot = input[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (input[j] <= pivot)
                {
                    i++;
                    Swap(input, i, j);
                }
            }

            Swap(input, i + 1, right);
            return i + 1;
        }

        private static void Swap(int[] input, int i, int j)
        {
            if (i < 0 || i >= input.Length)
            {
                throw new ArgumentOutOfRangeException("i index is out of range.");
            }

            if (j < 0 || j >= input.Length)
            {
                throw new ArgumentOutOfRangeException("j index is out of range.");
            }

            int temp = input[i];
            input[i] = input[j];
            input[j] = temp;
        }
        static void HeapSort(int[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("Input array cannot be null.");
            }

            if (input.Length == 0)
            {
                throw new ArgumentException("Input array cannot be empty.");
            }

            int n = input.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(input, n, i);

            for (int i = n - 1; i >= 0; i--)
            {
                int temp = input[0];
                input[0] = input[i];
                input[i] = temp;

                Heapify(input, i, 0);
            }
        }


        private static void Heapify(int[] input, int n, int i)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input cannot be null.");
            }

            if (n <= 0 || n > input.Length)
            {
                throw new ArgumentOutOfRangeException("n must be within the range of the input array.");
            }

            if (i < 0 || i >= n)
            {
                throw new ArgumentOutOfRangeException("i index is out of range.");
            }

            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && input[left] > input[largest])
            {
                largest = left;
            }

            if (right < n && input[right] > input[largest])
            {
                largest = right;
            }

            if (largest != i)
            {
                int temp = input[i];
                input[i] = input[largest];
                input[largest] = temp;

                Heapify(input, n, largest);
            }
        }
        // Method to generate dataset
        static int[] GenerateDataset(int size)
        {
            try
            {
                // Check if the size is a positive number
                if (size <= 0)
                {
                    throw new ArgumentException("The size of the dataset must be a positive number.");
                }

                // Name of the file to store the dataset
                string fileName = size + ".txt";


                // Check if the dataset file already exists
                if (File.Exists(fileName))
                {
                    // If the file exists, display a message
                    Console.WriteLine("Dataset file already exists. Loading from file.");


                    // Load the dataset from the file
                    return File.ReadAllLines(fileName).Select(int.Parse).ToArray();
                }


                // Create an array to hold the dataset
                int[] dataset = new int[size];


                // Create a random number generator
                Random random = new Random();


                // Fill the dataset array with random numbers
                for (int i = 0; i < size; i++)
                {
                    dataset[i] = random.Next(0, 1000000);
                }


                // Set the file name for the dataset file
                string datasetFile = size + ".txt";


                // Write the dataset to the file
                File.WriteAllLines(datasetFile, dataset.Select(x => x.ToString()));


                // Display a message saying the dataset has been generated and stored
                Console.WriteLine("Dataset generated and stored in " + fileName);


                // Return the generated dataset
                return dataset;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while generating the dataset: " + ex.Message);
                return null;
            }
        }
    }
}
