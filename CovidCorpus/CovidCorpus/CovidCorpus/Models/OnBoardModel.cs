﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CovidCorpus.Models
{
    public class OnBoardModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageAddress { get; set; }
        public View Image { get; set; }
    }
}
