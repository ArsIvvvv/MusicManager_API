using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MusicMicroservice.Domain.Common;
using MusicMicroservice.Domain.Entities;
using MusicMicroservice.Domain.Exceptions.MusicExceptions;

namespace MusicMicroservice.Domain.Entities;

public class Music: BaseEntity<Guid>
{
    public string Name {get; private set;} = string.Empty;
    public int Year {get; private set;}
    public string Style {get; private set;} = string.Empty;

    private readonly List<Executor> _executor = new();
    public ICollection<Executor> Executors
    {
        get => _executor;
        private set
        {
            if (value != null)
            {
                _executor.Clear();
                _executor.AddRange(value);
            }
        }
    }
    
    [NotMapped]
    public IReadOnlyCollection<Executor> ExecutorsReadOnly => _executor.AsReadOnly(); 

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
            throw new DomainMusicException("ID_EMPTY","Id пустой");
        }

        if(string.IsNullOrWhiteSpace(name))
        {
            throw new DomainMusicException("NAME_NULL","Название не может быть пустым");
        }

        if(year < 0)
        {
            throw new DomainMusicException("YEAR_NEGATIVE","Год не может быть отриц");
        }

        if (string.IsNullOrWhiteSpace(style))
        {
             throw new DomainMusicException("STYLE_NULL","Жанр музыки не может быть пустым");
        }

        return new Music (id,name,year,style);
    }

    public void ChangeName(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new DomainMusicException("NAME_NULL","Название не может быть пустым");
        }

        Name = name;
    }

    public void ChangeYear(int year)
    {
        if(year < 0)
        {
            throw new DomainMusicException("YEAR_NEGATIVE","Год не может быть отриц");
        }

        Year = year;
    }

    public void ChangeStyle(string style)
    {
        if(string.IsNullOrWhiteSpace(style))
        {
            throw new DomainMusicException("STYLE_NULL","Жанр музыки не может быть пустым");
        }

        Style = style;
    }

    public void AddExecutor(Executor executor)
    {
        if(executor == null)
        {
            throw new DomainMusicException("EXECUTOR_NULL","Исполнитель не может быть пустым");
        }

        _executor.Add(executor);
    }

    public void AddRangeExecutors(IEnumerable<Executor> executors)
    {
        if(executors == null || !executors.Any())
        {
            throw new DomainMusicException("EXECUTORS_RANGE_NULL","Исполнители не могут быть пустыми");
        }

        _executor.AddRange(executors);
    }

    public void RemoveExecutor(Executor executor)
    {
        if(executor == null)
        {
            throw new DomainMusicException("EXECUTOR_NULL","Исполнитель не может быть пустым");
        }

        _executor.Remove(executor);
    }
    
}