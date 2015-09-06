﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tatabouf.Domain
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// I got my lunch !
        /// </summary>
        public bool IGotMyLunch { get; set; }

        /// <summary>
        /// number of available seats in my car
        /// </summary>
        public byte? AvailableSeats { get; set; }
        
        public DateTime InscriptionDate { get; set; }

        public string IpAddress { get; set; }

        public User()
        {
            InscriptionDate = DateTime.Now;
            SelectedPlaces = new HashSet<Place>();
        }

        public virtual ICollection<Place> SelectedPlaces { get; set; }        
    }
}