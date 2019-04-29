﻿using CommonServiceLocator;
using FoodIdentifier.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodIdentifier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();

            BindingContext = ServiceLocator.Current.GetInstance<MainViewModel>();
        }
    }
}