using Microsoft.Maui.Controls;
using System.Collections.Generic;
using UmaBoaComida.Models;
using UmaBoaComida.Models.UsersModels;
using UmaBoaComida.Views;


namespace UmaBoaComida.Views.Login
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            if (!Globais.Funcionarios.Any()) {
                Globais.Funcionarios.Add(//CCCCCCCCAAAAAAAAAAAAAAAIIIIIIIIIIIIIOOOOOOOOOOO LEIA O COMENTARIO ABAIXO
                    new Models.UsersModels.Administrador("Cleide", "0", "C"/*"CleideSedosa@gmail.com"*/, "2050", 10000)//caio utilize o construtor correto, assim vc consegue usar ele tanto como funcionario quanto o tipo. e pode converter varias vezes sem problema
                );
                Globais.Funcionarios.Add(
                    new Models.UsersModels.Atendente("Dalva", "1", "D"/*"DalvaSedosa@gmail.com"*/, "10050", 10000)
                );
                Globais.Funcionarios.Add(
                    new Models.UsersModels.Cozinheiro("Carlinhos", "2", "C"/*"CarlinhosSedosa@gmail.com"*/, "1050", 10000)
                );
            }

            LoginButton.Clicked += async (s, e) => {
                try {
                    string email = EmailEntry.Text?.Trim() ?? "";
                    string senha = PasswordEntry.Text?.Trim() ?? "";

                    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha)) {
                        await DisplayAlert("Erro", "Preencha email e senha", "OK");
                        return;
                    }

                    var usuario = Globais.Funcionarios.FirstOrDefault(f => f.Email.Equals(email, System.StringComparison.OrdinalIgnoreCase) && f.Password == senha);

                    if (usuario != null) {
                        await DisplayAlert("Sucesso", $"Bem-vindo(a) {usuario.Nome}", "OK");

                        switch (usuario.Tipo) {
                            case TipoFuncionario.Administrador://CCCCCCCCAAAAAAAAAAAAAAAIIIIIIIIIIIIIOOOOOOOOOOO LEIA O COMENTARIO ABAIXO
                                await Navigation.PushAsync(new Views.Admin.AdminMainPage(usuario as Models.UsersModels.Administrador));//Expecifique o caminho que vc quer pegar o objeto se nao ele vai pegar a view ou vice versa
                                break;
                            case TipoFuncionario.Cozinheiro:
                                await Navigation.PushAsync(new Views.Cozinheiro.CozinheiroMainPage(usuario as Models.UsersModels.Cozinheiro));
                                break;
                            case TipoFuncionario.Atendente:
                                await Navigation.PushAsync(new Views.Atendente.AtendenteMainPage(usuario as Models.UsersModels.Atendente));
                                break;
                            default:
                                await DisplayAlert("Aviso", "Tipo de usuário não reconhecido", "OK");
                                break;
                        }
                    } else {
                        await DisplayAlert("Erro", "Email ou senha incorretos", "OK");
                    }
                } catch (System.Exception ex) {
                    System.Diagnostics.Debug.WriteLine($"Erro no login: {ex}");
                    await DisplayAlert("Erro", "Ocorreu um erro no login", "OK");
                }
            };

            CadastrarClienteButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new UmaBoaComida.Views.Cadastro.CadastrarFuncionarioPage(new Administrador("admin","admin","admin@gmail.com","admin", 0.5)));
            };
        }

    }
}
