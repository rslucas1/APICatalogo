﻿using APICatalogo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogo.Domain
{
    public class MeuServico : IMeuServico
    {
        public string Saudacao(string nome) 
        {
           return $"Bem-Vindo, {nome} \n\n {DateTime.Now}";
        }
    }
}
