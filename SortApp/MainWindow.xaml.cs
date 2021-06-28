﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace SortApp
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[] data;
        int numberOfItems;
        int maxValue;
        UIEngine uiEngine;
        IEngine engine;
        const int LINE_THICKNESS = 10;
        Task t;

        CancellationTokenSource tokenSource;
        CancellationToken token;

        public MainWindow()
        {
            InitializeComponent();
            uiEngine = new UIEngine(cvsGraphic, LINE_THICKNESS);
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            if (engine != null)
            {
                if (engine.IsRunning())
                {
                    return;
                }
            }

            numberOfItems = (int)cvsGraphic.ActualWidth / LINE_THICKNESS;
            maxValue = (int)cvsGraphic.ActualHeight;
            data = new int[numberOfItems];
            Random random = new Random();

            for (int i = 0; i < numberOfItems; i++)
            {
                data[i] = random.Next(maxValue);
            }

            uiEngine.DrawGraphic(data);
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (engine != null)
            {
                if (engine.IsRunning())
                {
                    return;
                }
            }
            if (data == null)
            {
                btnGenerate_Click(null, null);
            }

            switch (cboxAlgo.SelectedIndex)
            {
                case 0:
                    engine = new EngineBubbleSort();
                    break;
                case 1:
                    engine = new EngineSelectionSort();
                    break;
                default:
                    break;
            }

            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;

            t = new Task(() => engine.Compute(data, uiEngine, token));
            t.Start();

            try
            {
                await t;
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {ex.Message}");
            }
            finally
            {
                tokenSource.Dispose();
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (t != null && tokenSource != null && tokenSource.IsCancellationRequested == false)
            {
                tokenSource.Cancel();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            btnStop_Click(null, null);
        }
    }
}
