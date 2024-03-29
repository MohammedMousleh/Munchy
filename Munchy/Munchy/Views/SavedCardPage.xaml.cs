﻿using Munchy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Munchy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SavedCardPage : ContentPage
    {
        SavedCardVM viewModel;
        public SavedCardPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new SavedCardVM();
        }
    }
}