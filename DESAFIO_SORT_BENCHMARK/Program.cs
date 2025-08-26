using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace SortBenchmark {
    class Program {
        static void Main(string[] args) {
            // Caminho fixo para o arquivo input.json
            string filePath = @"C:\Vinicius\Estudos\Curso DevSuperior\Estruturas de Dados\DESAFIO_SORT_BENCHMARK\DESAFIO_SORT_BENCHMARK\data\input.json";

            if(!File.Exists(filePath)) {
                Console.WriteLine($"Arquivo não encontrado: {filePath}");
                return;
            }

            // Lendo os dados do arquivo input.json
            List<string> originalList = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(filePath));

            var resultados = new Dictionary<string, long>();

            Stopwatch sw = new Stopwatch();

            // Insertion Sort
            List<string> insertionList = new List<string>(originalList);
            sw.Start();
            InsertionSort(insertionList);
            sw.Stop();
            resultados["Insertion sort"] = sw.ElapsedMilliseconds;

            // Bubble Sort
            List<string> bubbleList = new List<string>(originalList);
            sw.Restart();
            BubbleSort(bubbleList);
            sw.Stop();
            resultados["Bubble sort"] = sw.ElapsedMilliseconds;

            // Quick Sort
            List<string> quickList = new List<string>(originalList);
            sw.Restart();
            QuickSort(quickList, 0, quickList.Count - 1);
            sw.Stop();
            resultados["Quick sort"] = sw.ElapsedMilliseconds;

            // Saída no formato solicitado
            foreach(var item in resultados) {
                Console.WriteLine($"{item.Key}: {item.Value}ms");
            }
        }

        // Algoritmo Insertion Sort
        static void InsertionSort(List<string> list) {
            for(int i = 1; i < list.Count; i++) {
                string key = list[i];
                int j = i - 1;

                while(j >= 0 && string.Compare(list[j], key, StringComparison.Ordinal) > 0) {
                    list[j + 1] = list[j];
                    j--;
                }
                list[j + 1] = key;
            }
        }

        // Algoritmo Bubble Sort
        static void BubbleSort(List<string> list) {
            int n = list.Count;
            for(int i = 0; i < n - 1; i++) {
                for(int j = 0; j < n - i - 1; j++) {
                    if(string.Compare(list[j], list[j + 1], StringComparison.Ordinal) > 0) {
                        string temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
        }

        // Algoritmo Quick Sort
        static void QuickSort(List<string> list, int low, int high) {
            if(low < high) {
                int pi = Partition(list, low, high);
                QuickSort(list, low, pi - 1);
                QuickSort(list, pi + 1, high);
            }
        }

        static int Partition(List<string> list, int low, int high) {
            string pivot = list[high];
            int i = low - 1;

            for(int j = low; j < high; j++) {
                if(string.Compare(list[j], pivot, StringComparison.Ordinal) < 0) {
                    i++;
                    (list[i], list[j]) = (list[j], list[i]);
                }
            }
            (list[i + 1], list[high]) = (list[high], list[i + 1]);
            return i + 1;
        }
    }
}
