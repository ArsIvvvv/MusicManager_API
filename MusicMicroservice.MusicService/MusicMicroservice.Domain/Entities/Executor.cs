using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Domain.Common;
using MusicMicroservice.Domain.Exceptions.ExecutorExceptions;

namespace MusicMicroservice.Domain.Entities;

public class Executor: BaseEntity<Guid>
{ 
    public string FirstName {get; private set;} = string.Empty;
    public string LastName {get; private set;}= string.Empty;
    public string Nickname {get; private set;}= string.Empty;

    private readonly List<Music> _musics = new();

    public ICollection<Music> Musics
    {
        get => _musics;
        private set
        {
            if (value != null)
            {
                _musics.Clear();
                _musics.AddRange(value);
            }
        }
    }
    
    [NotMapped]
    public IReadOnlyCollection<Music> MusicsReadOnly => _musics.AsReadOnly(); 

    protected Executor() {}

    private Executor(Guid id,string firstName, string lastName, string nik)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Nickname = nik;
    }
    public static Executor Create(Guid id,string firstName, string lastName, string nik)
    {

         if(id == Guid.Empty)
        {
            throw new DomainExecutorException("Executor_ID_EMPTY","Id пустой");
        }
        if (string.IsNullOrWhiteSpace(firstName))
        {
             throw new DomainExecutorException("EXECUTOR_FIRSTNAME_NULL","Имя не может быть пустым");
        }
        if (string.IsNullOrWhiteSpace(lastName))
        {
             throw new DomainExecutorException("Executor_LASTNAME_NULL","Фамилия не может быть пустым");
        }
        if (string.IsNullOrWhiteSpace(nik))
        {
             throw new DomainExecutorException("EXECUTOR_NICKNAME_NULL","Псевдоним не может быть пустым");
        }

        return new Executor(id,firstName,lastName,nik);
    }

    public void ChangeNickname(string nickname)
    {
        if (string.IsNullOrWhiteSpace(nickname))
        {
             throw new DomainExecutorException("EXECUTOR_NICKNAME_NULL","Псевдоним не может быть пустым");
        }

        Nickname = nickname;
    }

    public void ChangeFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
             throw new DomainExecutorException("EXECUTOR_FIRSTNAME_NULL","Имя не может быть пустым");
        }

        FirstName = firstName;
    }
    
    public void ChangeLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
        {
             throw new DomainExecutorException("Executor_LASTNAME_NULL","Фамилия не может быть пустым");
        }

        LastName = lastName;
    }

    public void AddMusic(Music music)
    {
        if(music == null)
        {
            throw new DomainExecutorException("MUSIC_NULL","Музыка не может быть пустой");
        }

        _musics.Add(music);
    }

    public void AddRangeMusic(IEnumerable<Music> musics)
    {
        if(musics == null || !musics.Any())
        {
            throw new DomainExecutorException("MUSICS_NULL","Музыки не могут быть пустыми");
        }

        _musics.AddRange(musics);
    }

    public void RemoveMusic(Music music)
    {
        if(music == null)
        {
            throw new DomainExecutorException("MUSIC_NULL","Музыка не может быть пустой");
        }

        _musics.Remove(music);
    }



}