﻿namespace cChat.Data.Entities
{
    public interface IEntity<T> 
    {
        public T Id {get; set;}
    }
}