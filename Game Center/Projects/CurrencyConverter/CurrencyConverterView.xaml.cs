﻿using Game_Center.Projects.CurrencyConverter.Services;
using Microsoft.Extensions.Configuration;
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
using System.Windows.Shapes;

namespace Game_Center.Projects.CurrencyConverter
{
    /// <summary>
    /// Interaction logic for CurrencyConverterView.xaml
    /// </summary>
    public partial class CurrencyConverterView : Window
    {
        private CurrencyService _currency_Service;
        private Dictionary<string, double> _exchangeRates;
        public CurrencyConverterView()
        {
            InitializeComponent();
            _currency_Service= new CurrencyService();
            LoadCurrencies();   
            
        }

        private async void LoadCurrencies()
        {
            try
            {
                _exchangeRates = await _currency_Service.GetExchangeRateAsync();
                string[] strings = _exchangeRates.Keys.ToArray();
                FromCurrencyComboBox.ItemsSource = strings;
                ToCurrencyComboBox.ItemsSource = strings;
            }
            catch
            {
                throw new InvalidOperationException("Failed to run GetExchangeRateAsync");
            }
        }

        private async void OnConvertClick(object sender, RoutedEventArgs e)
        {
            string fromCurrency = FromCurrencyComboBox.SelectedItem.ToString();
            string toCurrency = ToCurrencyComboBox.SelectedItem.ToString();
            double amount = double.Parse(AmountTextBox.Text);
            txtResult.Text =  $"{AmountTextBox.Text} {FromCurrencyComboBox.SelectedItem.ToString()} is equal to {(amount * (_exchangeRates[toCurrency] / _exchangeRates[fromCurrency])).ToString()} {ToCurrencyComboBox.SelectedItem.ToString()} ";
        }
    }
}
