﻿using System;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities
{
    public class Employee : NamedEntity
    {
        /// <summary>Фамилия</summary>
        public string Surname { get; set; }

        /// <summary>Отчество</summary>
        public string Patronymic { get; set; }

        /// <summary>Возраст</summary>
        public int Age { get; set; }

        /// <summary>Дата поступления на работу</summary>
        public DateTime EmployementDate { get; set; }
    }
}
