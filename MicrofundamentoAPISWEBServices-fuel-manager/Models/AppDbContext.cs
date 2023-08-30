using MicrofundamentoAPISWEBServices_fuel_manager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MicrofundamentoAPISWEBServices_fuel_manager.Models
{
    public class AppDbContext : DbContext
    {
        //construtor
        public AppDbContext(DbContextOptions options) : base(options)
        {


        }
        public DbSet<Veiculo> Veiculos { get; set; }

        public DbSet<Consumo> Consumos { get; set; }


    }
}

//classe de contexto
//aqui vamos configurar o nosso entiframework
//A classe de AppDbcontext deve herdar de DBContext
//DBContext é uma classe que já vai receber as configurações/projeções de dependencias
//vamos configurar as proprieades e já vem configurado ou pré configurado, precisamos apenas
//passar essas configurações para o nosso sitema.
//bContextOptions options opções que recebe do sistema.
//AppDbContext(DbContextOptions options) - recebe a injenção de dependencia, configurações e opções de dependencia
// e já direciona para a classe pai que é dbcontext(pré configurado)
//também passo os objtos/tabelas que ele vai criar/entidades
//DbSet - conjunto de dados do banco de dados, DbSet<Veiculo> - tabela veiculo

// oque vem a ser injeção de dependencias:é quando eu não quero ficar criando instancias das minhas classes de forma
// programática. Eu não quero ficar toda hora criando uma dependencia entre os meus objetos. vou fazer isso de forma configuração
// Em que o meu programa em que meu programa vai carregar essa classe automaticamente por meio de configuração
//e não de instancia.
//se eu for usa uma instancia em dbcontext eu precisava dar um new, criar uma classe e gerenciar ela...
//a forma de fazer isso por meio de injeção de dependencia é fazer por meio de configuração
//e não por via de programação, criando uma nova instancia... e para configura essa configuração vamos usar a classe programa.

// código abaixo é injeção de dependencias
//public AppDbContext(DbContextOptions options) : base(options)


