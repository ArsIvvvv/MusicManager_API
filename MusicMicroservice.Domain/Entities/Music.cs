using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MusicMicroservice.Domain.Common;

namespace MusicMicroservice.Domain
{
    public class Music: BaseEntity<Guid>
    {
        public string Name {get; private set;} = string.Empty;
        public int Year {get; private set;}
        public string Style {get; private set;} =string.Empty;


        protected Music() {}

        private Music(Guid id,string name, int year, string style)
        {
            Id = id;
            Name = name;
            Year = year;
            Style = style;
        }

        public static Music Create(Guid id, string name, int year, string style)
        {
            if(id == Guid.Empty)
            {
                throw new Exception ("Id пустой");
            }

            if(string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Название не может быть пустым");
            }

            if(year < 0)
            {
                throw new Exception("Год не может быть отриц");
            }

            if (string.IsNullOrWhiteSpace(style))
            {
                 throw new Exception("Жанр музыки не может быть пустым");
            }

            return new Music (id,name,year,style);
        }

        
    }
}