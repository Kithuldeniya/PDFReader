using Microsoft.Win32;
using PDFReader.Helpers;
using PDFReader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using forms = System.Windows.Forms;
//using TikaOnDotNet.TextExtraction;

namespace PDFReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowModel Model;
        public MainWindow()
        {
            Model = new MainWindowModel
            {
                OutputPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                UploadedFiles = new List<FileModel>()
            };
            InitializeComponent();
            this.DataContext = Model;
        }

        private void SelectOutputPath_Click(object sender, RoutedEventArgs e)
        {
            var foldeDialog = new forms.FolderBrowserDialog();
            forms.DialogResult result = foldeDialog.ShowDialog();
            if (result == forms.DialogResult.OK)
            {
                OutputPath.Content = foldeDialog.SelectedPath;
                Model.OutputPath = foldeDialog.SelectedPath;
            }
            
        }

        private void SelectPDFFiles_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Title = "Select PDF files to convert",
                Multiselect = true,
                Filter = "PDF files (*.pdf)|*.pdf"
            };
            var res = fileDialog.ShowDialog();

            if (res.HasValue && res.Value)
            {
                var files = fileDialog.FileNames.Select((f, i) => new FileModel
                {
                    Path = f,
                    Name = fileDialog.SafeFileNames[i]
                });

                Model.UploadedFiles.RemoveAll(f => true);
                Model.UploadedFiles.AddRange(files);

                var fileNames = string.Join("\n", files.Select(f => f.Name).ToArray());
                MessageBox.Show(files.Count() + " File(s) uploaded \n\n" + fileNames, "File Uploaded", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void ConvertPDF_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(Model.OutputPath))
            {
                MessageBox.Show("Please select a output path", "Empty Data", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            if (Model.UploadedFiles.Count == 0)
            {
                MessageBox.Show("Please select a PDF files", "Empty Data", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            var output = new List<OutputModel>();

            foreach (var file in Model.UploadedFiles)
            {
                var text = PdfHelper.ManipulatePdf(file.Path);
                var fileOutput = new OutputModel(text)
                {
                    FileName = file.Name
                };

                output.Add(fileOutput);
            }

            var textFilename = "Output_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".csv";
            var outputPath = System.IO.Path.Combine(Model.OutputPath, textFilename);
            CsvFileHelper.WriteToFile(outputPath, output);

            MessageBox.Show("Success! \n \n Find the file at \"" + outputPath + "\"", "Converted", MessageBoxButton.OK, MessageBoxImage.Information);

        }

    }
}
