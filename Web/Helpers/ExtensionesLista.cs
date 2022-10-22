using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ViewModels;

namespace Web.Helpers
{
    public static class ExtensionesLista
    {
        public static SelectList AsSelectList(this List<ItemLista> itemLista)
        {
            return new SelectList(itemLista,"Valor","Texto");
        }

        public static SelectList AsSelectList(this List<EntidadLista> itemLista)
        {
            return new SelectList(itemLista, "Valor", "Texto");
        }
    }
}
