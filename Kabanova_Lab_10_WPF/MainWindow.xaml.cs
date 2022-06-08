using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kabanova_Lab_10_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string first;
        private int k, m, n;

        private async Task<List<int[,]>> ReadMatrix(string file)
        {
            List<int[,]> list = new List<int[,]>();
            using (StreamReader reader = new StreamReader(file))
            {
                string? line;
                int[,] matrix = new int[n, m];
                int a = 0;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (!line.Equals(""))
                    {
                        string[] mas = line.Split(" ");
                        for (int i = 0; i < mas.Length - 1; i++)
                        {
                            matrix[a, i] = int.Parse(mas[i]);
                        }
                        a++;
                    }
                    else
                    {
                        list.Add(matrix);
                        a = 0;
                    }
                }
            }
            return list;
        }
        public MainWindow()
        {
            InitializeComponent();
            first = Environment.CurrentDirectory + "\\first.txt";
            FileInfo firstFile = new FileInfo(first);
            if (firstFile.Exists) firstFile.Delete();
            else firstFile.Create();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            k = int.Parse(K.Text);
            m = int.Parse(M.Text);
            n = int.Parse(N.Text);
            GenMatrix(first, k, Table1);

        }
        private async void GenMatrix(string file, int a, TextBlock t)
        {
            Random random = new Random();
            for (int x = 1; x <= a; x++)
            {
                int[,] mas = new int[n, m];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        mas[i, j] = random.Next(10, 100);
                        using (StreamWriter writer = new StreamWriter(file, true))
                        {
                            await writer.WriteAsync(mas[i, j] + " ");
                        }
                    }
                    using (StreamWriter writer = new StreamWriter(file, true))
                    {
                        await writer.WriteLineAsync();
                    }
                }
                using (StreamWriter writer = new StreamWriter(file, true))
                {
                    await writer.WriteLineAsync();
                }
            }
            using (StreamReader reader = new StreamReader(file))
            {
                string text = await reader.ReadToEndAsync();
                t.Text = text;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            int[,] matrix = new int[n, m];
            int sum = 0;
            for (int j = 0; j < n; j++)
            {
                for (int q = 0; q < m; q++)
                {
                    matrix[j, q] = rand.Next(10, 100);

                }
                if ((j + 1) % 2 == 0)

                {
                    for (int i = 0; i < m; i++)
                    {
                        sum += matrix[j, i];
                    }
                }
            }
            
        }

    }
}
