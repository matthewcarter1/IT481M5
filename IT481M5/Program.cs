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
            // Prompt user to input the size of the dataset they want to sort
    Console.WriteLine("Enter the size of the dataset you want to sort (10, 1000, or 10000): ");

    int size = 0;
    try
    {
        size = int.Parse(Console.ReadLine());
        if (size != 10 && size != 1000 && size != 10000)
        {
            Console.WriteLine("Invalid input. Please enter 10, 1000, or 10000.");
            return;
        }
    }
    catch (Exception)
    {
        Console.WriteLine("Invalid input. Please enter 10, 1000, or 10000.");
        return;
    }
            // Initialize the dataset array to null
            int[] dataset = null;

            try
            {
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
                    default:
                        // Invalid input, display error message
                        Console.WriteLine("Invalid input. Please enter 10, 1000, or 10000.");
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
                Console.WriteLine("Error: " + ex.Message);
            }
        }


        // Method to sort the dataset using bubble sort algorithm
        static void BubbleSort(int[] input)
        {
                if (input == null || input.Length == 0)
    {
        throw new ArgumentException("Input array cannot be null or empty.");
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
        static void QuickSort(int[] input, int low, int high)
        {
            if (input == null || input.Length == 0)
            {
                throw new ArgumentException("Input array cannot be null or empty.");
            }
            if (low < 0 || low >= input.Length || high < 0 || high >= input.Length)
            {
                throw new ArgumentException("Low and high values must be within the bounds of the input array.");
            }
            if (low >= high)
            {
                throw new ArgumentException("Low value cannot be greater than or equal to the high value.");
            }

            // Check if the portion of the array to be sorted is valid (more than 1 element)
            if (low < high)
            {
                // Partition the portion of the array and return the partition index
                int partitionIndex = Partition(input, low, high);


                // Recursively sort the portion of the array to the left of the partition index
                QuickSort(input, low, partitionIndex - 1);
                // Recursively sort the portion of the array to the right of the partition index
                QuickSort(input, partitionIndex + 1, high);
            }
        }
        static int Partition(int[] input, int low, int high)
        {
            if (input == null || input.Length == 0)
            {
                throw new ArgumentException("Input array cannot be null or empty.");
            }
            if (low < 0 || low >= input.Length || high < 0 || high >= input.Length)
            {
                throw new ArgumentException("Low and high values must be within the bounds of the input arary.");
            }

            // Choose the pivot element as the last element in the portion of the array
            int pivot = input[high];
            // Initialize the index of the smaller element
            int i = low - 1;
            // Iterate through the portion of the array
            for (int j = low; j < high; j++)
            {
                // If the current element is smaller than or equal to the pivot element, increment the index of the smaller element
                // and swap the current element with the element at the index of the smaller element
                if (input[j] <= pivot)
                {
                    i++;
                    int temp = input[i];
                    input[i] = input[j];
                    input[j] = temp;
                }
            }
            // Swap the pivot element with the element at the index of the smaller element + 1
            int temp1 = input[i + 1];
            input[i + 1] = input[high];
            input[high] = temp1;
            // Return the partition index
            return i + 1;
        }

        static void HeapSort(int[] input)
        {
            if (input == null || input.Length == 0)
            {
                throw new Exception("Input array is empty or null.");
            }
            // Build a max heap from the input array
            int n = input.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(input, n, i);


            // One by one extract elements from the heap
            for (int i = n - 1; i >= 0; i--)
            {
                // Move the current root to the end
                int temp = input[0];
                input[0] = input[i];
                input[i] = temp;


                // Call Heapify on the reduced heap
                Heapify(input, i, 0);
            }
        }

        static void Heapify(int[] input, int n, int i)
        {
            if (input == null || input.Length == 0)
            {
                throw new Exception("Input array is empty or null.");
            }
            if (n <= 0)
            {
                throw new Exception("Size of the heap should be greater than 0.");
            }
            if (i < 0)
            {
                throw new Exception("Index should be greater than or equal to 0.");
            }
            // Initialize the largest element as the root
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;


            // If the left child is larger than the root
            if (left < n && input[left] > input[largest])
                largest = left;


            // If the right child is larger than the root
            if (right < n && input[right] > input[largest])
                largest = right;


            // If the largest is not the root
            if (largest != i)
            {
                // Swap the root with the largest
                int swap = input[i];
                input[i] = input[largest];
                input[largest] = swap;


                // Recursively Heapify the affected sub-tree
                Heapify(input, n, largest);
            }
        }

        // Method to generate dataset
        static int[] GenerateDataset(int size)
        {
            if (size <= 0)
            {
                throw new Exception("Size of the dataset should be greater than 0.");
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
    }
}

