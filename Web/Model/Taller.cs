﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model
{
    public class Taller
    {
        public int Id { get; set; }
        public int ComunaId { get; set; }
        public Comuna Comuna { get; set; }
        public string Nombre { get; set; }
    }
}
