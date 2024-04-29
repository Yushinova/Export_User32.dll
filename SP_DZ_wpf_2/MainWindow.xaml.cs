using System;
using System.Collections.Generic;
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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using System.Management;
using System.Collections.ObjectModel;

namespace SP_DZ_wpf_2
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<MyPath> programs = new ObservableCollection<MyPath>();
        public ObservableCollection<string> processes = new ObservableCollection<string>();
        public MyPath temp_path; 
        //public MyWinApi api = new MyWinApi();
        public IntPtr ptr = IntPtr.Zero;
        public int count_ = 0;
        public MainWindow()
        {
            InitializeComponent();
            GetExe();
            ListPrograms.ItemsSource = programs;
            ListProcesses.ItemsSource = processes;

        }
        public void GetExe()//получаем все exe данного решения
        {

           //var hh = new FileInfo(Application.ResourceAssembly).FullName;
            //название файла основной папки
            string name_solution = "SP_DZ_1";
            string except = Process.GetCurrentProcess().MainModule.FileName;//запущенная программа
            DirectoryInfo dir = new DirectoryInfo(except);

            string exe_main = dir.Name;//exe запущенной программы
            string solution = dir.FullName;//полный путь исполняемого файла

            int ind = solution.IndexOf(name_solution);
            int ind_2 = solution.IndexOf('\\', ind);//нужно отсечь путь до папки Решение
            solution = solution.Substring(0, ind_2);


            //получаем все *.exe файлы из данного решения
            // кроме запускаемого проекта
            List<MyPath> temp = new List<MyPath>();
            string[] files = Directory.GetFiles(solution, "*.exe", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                string fileName = new FileInfo(file).FullName;
                if (fileName.IndexOf(exe_main) == -1)
                {
                    temp.Add(new MyPath { short_path = System.IO.Path.GetFileName(fileName), full_path = fileName });
                }

            }
            temp = temp.GroupBy(p => p.short_path).Select(p => p.First()).ToList();//убираем дубликаты
            foreach (var item in temp)
            {
                programs.Add(item);
            }
            ff.Text = programs[0].full_path;
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ListPrograms.SelectedItems.Count > 0)
            {
                Process proc = Process.Start(programs[ListPrograms.SelectedIndex].full_path);
                temp_path = programs[ListPrograms.SelectedIndex];
                processes.Add(proc.ProcessName);
                programs.RemoveAt(ListPrograms.SelectedIndex);
               
                // ff.Text = proc.MainWindowTitle.ToString();
                count_++;
            }
        }

        private void Rename_Click(object sender, RoutedEventArgs e)//переименование Title окна
        {
            if (ListProcesses.SelectedIndex != -1)
            {
                ptr = MyWinApi.FindWindow(null, ListProcesses.SelectedItem.ToString());
                ff.Text = ptr.ToString();
                MyWinApi.SendMessage(ptr, MyWinApi.WM_SETTEXT, IntPtr.Zero, $"Дочерний процесс{count_}");
                processes[ListProcesses.SelectedIndex] = $"Дочерний процесс{count_}";
            }

        }

        private void Close_Child(object sender, RoutedEventArgs e)
        {
            if (ListProcesses.SelectedIndex != -1)
            {
                ptr = MyWinApi.FindWindow(null, ListProcesses.SelectedItem.ToString());
                MyWinApi.SendMessage(ptr, MyWinApi.WM_CLOSE, IntPtr.Zero, "Закрыто");
                processes.Remove(ListProcesses.SelectedItem.ToString());
                programs.Add(temp_path);
                count_--;
               
            }
            //ff.Text = ptr.ToString();

        }

        private void Close_Main_Click(object sender, RoutedEventArgs e)
        {
            if (processes.Count > 0)
            {
                foreach (var item in processes)
                {
                    ptr = MyWinApi.FindWindow(null, item);
                    MyWinApi.SendMessage(ptr, MyWinApi.WM_CLOSE, IntPtr.Zero, "Закрыто");
                    count_--;
                }
            }
            this.Close();
        }
    }
}
