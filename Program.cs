using Cafe_Colombiano.src.Shared.Context;


﻿using System.Threading.Tasks;


using Cafe_Colombiano.src.Modules.Variedad.Application.Services;


using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;



using Cafe_Colombiano.src.Shared.Helpers;

using Microsoft.EntityFrameworkCore;
using Cafe_Colombiano.src.Modules.Usuario.Ui;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var autenticador = new AutenticadorUsuario("admin", "12345");
        var menuUsuario = new DibujoMenusUsuario(autenticador);
        await menuUsuario.Iniciar();
    }

}